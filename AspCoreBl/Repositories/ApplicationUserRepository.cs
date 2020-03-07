using AspCoreBl.Interfaces;
using AspCoreBl.Misc;
using AspCoreBl.ModelDTO;
using AspCoreBl.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AspCoreBl.Bl
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private  UserManager<IdentityUser> _userManager;
        private  SignInManager<IdentityUser> _signInManager;
        private readonly IHttpContextAccessor _httpContext;
        private readonly Services.EmailService _emailService;


        public ApplicationUserRepository(
            UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager,
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

            var isUserExixts = await UserExist(dto);
            if (isUserExixts)
                return new KeyValuePair<int, string>(-3, "User already exists for " + dto.UserName + " email.");

            var user = new IdentityUser()
            {
                UserName = dto.UserName,
                Email = dto.Email,               
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {   
                return new KeyValuePair<int, string>(-2, result.Errors.ToString());
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var fullname = dto.UserName;
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

        public async Task<bool> ConfirmEmailAsync(string UserName, string code)
        {
            var user = await _userManager.FindByNameAsync(UserName);

            code = WebUtility.UrlDecode(code);
            code = code.Replace(' ', '+');

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {             
                return true;
            }
            return false;
        }

        public async Task<LoginSuccessViewModel> LoginAsync(IdentityUserDTO dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.UserName,
                  dto.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(dto.UserName);
                var loginSuccessViewModel = new LoginSuccessViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email
                };
                return loginSuccessViewModel;
            }
            return null;
        }

        public async Task<bool> UserExist(IdentityUserDTO dto)
        {
            
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user==null)
            {
                return false;
            }
            return true;

        }



    }
}
