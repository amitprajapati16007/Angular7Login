using AspCoreBl.Interfaces;
using AspCoreBl.Misc;
using AspCoreBl.Model;
using AspCoreBl.ModelDTO;
using AspCoreBl.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;


namespace AspCoreBl.Bl
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContext;
        private readonly Services.EmailService _emailService;


        public ApplicationUserRepository(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IHttpContextAccessor httpContext,
            IOptions<EmailSettings> emailSettings
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContext = httpContext;
            _emailService = new Services.EmailService(emailSettings);


        }

        public async Task<KeyValuePair<int, string>> PostApplicationUser(IdentityUserDTO dto)
        {
            dto.UserName = dto.Email;
            var isUserExixts = await UserExist(dto);
            if (isUserExixts)
                return new KeyValuePair<int, string>(-3, "User already exists for " + dto.UserName + " email.");

            var user = new ApplicationUser()
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                return new KeyValuePair<int, string>(-2, result.Errors.ToString());
            }
            var roleRes = await _userManager.AddToRoleAsync(user, Role.WebUser.ToString());
            if (!roleRes.Succeeded)
            {

                var resDeleteUser = await _userManager.DeleteAsync(user);
                if (!resDeleteUser.Succeeded)
                {
                    return new KeyValuePair<int, string>(-7, "User successfully created but failed to set role, tried to remove user but error occured.");
                }

                return new KeyValuePair<int, string>(-8, "User successfully created but failed to set role, removed user.");
            }
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var fullname = dto.LastName + " " + dto.FirstName;
            var mailContent = await EmailBodyCreator.CreateConfirmEmailBody(Utilities.GetCurrHost(_httpContext), fullname, dto.UserName, code);
            try
            {
                await _emailService.SendMailAsync(new List<MailAddress>() { new MailAddress(user.Email) }, null, null, AppCommon.AppName + " - Verify Email", mailContent, null);
                return new KeyValuePair<int, string>(1, "User successfully created, email sent.");
            }
            catch (Exception ex)
            {
                var resDeleteUser = await _userManager.DeleteAsync(user);
                if (!resDeleteUser.Succeeded)
                {
                    return new KeyValuePair<int, string>(-6, "User successfully created but failed to sent email, tried to remove user but error occured.");
                }
                return new KeyValuePair<int, string>(-4, "User successfully created but failed to sent email, deleted user.");
            }
        }

        public async Task<bool> ConfirmEmailAsync(string email, string code)
        {
            var user = await _userManager.FindByEmailAsync(email);

            code = WebUtility.UrlDecode(code);
            code = code.Replace(' ', '+');

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<KeyValuePair<string, LoginSuccessViewModel>> LoginAsync(IdentityUserDTO dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.UserName,
                  dto.Password, false, false);

            if (!result.Succeeded)
            {
                return new KeyValuePair<string, LoginSuccessViewModel>("username or password is incorrect please try again", null);
            }

            var user = await _userManager.FindByNameAsync(dto.UserName);

            if (!user.EmailConfirmed)
            {
                return new KeyValuePair<string, LoginSuccessViewModel>("Invalid login attempt. You must have a confirmed email account.", null);
            }
            var roles = await _userManager.GetRolesAsync(user);
            var role = (Role)Enum.Parse(typeof(Role), roles[0]);

            if (role == Role.System) return new KeyValuePair<string, LoginSuccessViewModel>("Role not found.", null);
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                     new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role, role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(AppCommon.SymmetricSecurityKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var loginSuccessViewModel = new LoginSuccessViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Token = tokenHandler.WriteToken(token)
            };
            return new KeyValuePair<string, LoginSuccessViewModel>("", loginSuccessViewModel);

        }

        public async Task<bool> ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return false;

            var resetCode = await _userManager.GeneratePasswordResetTokenAsync(user);
            var fullname = user.FirstName + " " + user.LastName;
            var mailContent = await EmailBodyCreator.CreateResetPasswordEmailBody(Utilities.GetCurrHost(_httpContext), fullname, user.Email, resetCode);
            var fullName = user.FirstName + " " + user.LastName;
            await _emailService.SendMailAsync(new List<MailAddress>() { new MailAddress(user.Email, fullName) }, null, null, AppCommon.AppName + " - Reset Password", mailContent, null);

            return true;
        }
        public async Task<KeyValuePair<int, LoginSuccessViewModel>> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return new KeyValuePair<int, LoginSuccessViewModel>(0, null);

            model.Code = WebUtility.UrlDecode(model.Code);
            model.Code = model.Code.Replace(' ', '+');

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.NewPassword);
            if (result.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(user);
                KeyValuePair<string, LoginSuccessViewModel> loginRes = await LoginAsync(new IdentityUserDTO() { UserName = user.UserName, Password = model.NewPassword });
                if (loginRes.Key != null)
                    return new KeyValuePair<int, LoginSuccessViewModel>(1, loginRes.Value);
            }
            return new KeyValuePair<int, LoginSuccessViewModel>(2, null);
        }

        public async Task<bool> UserExist(IdentityUserDTO dto)
        {

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return false;
            }
            return true;

        }
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }


    }
}
