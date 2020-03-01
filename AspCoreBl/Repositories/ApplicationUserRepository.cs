using AspCoreBl.Interfaces;
using AspCoreBl.ModelDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        
        public ApplicationUserRepository(
            UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager,
            IHttpContextAccessor httpContext
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContext = httpContext;
            
        }

        public async Task<Object> PostApplicationUser(IdentityUserDTO dto)
        {
            var user = new IdentityUser()
            {
                UserName = dto.UserName,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var callbackUrl = (
                //   "ConfirmEmail", "Account",
                //   new { userId = user.Email, code = code },
                //   protocol: _httpContext.HttpContext.Request.Host);


                var callbackUrl = _httpContext.HttpContext.Request.Host;
                var body= "Confirm your account"+
                   "Please confirm your account by clicking this link: <a href=\""
                                                   + callbackUrl + "\">link</a>";
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                //client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("amit.prajapati16007@gmail.com", "007Password7@@");

                try
                {
                    client.SendMailAsync("amit.prajapati16007@gmail.com", "amit.prajapati16007@gmail.com", "Confirm your account", body);
                }
                catch (Exception ex)
                {

                    throw;
                } 
                

            }
            return result;

        }

        public async Task<bool> ConfirmEmailAsync(string email, string code)
        {
            var user = await _userManager.FindByIdAsync(email);

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
