using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AngularWithAspCore.Misc;
using AspCoreBl.Bl;
using AspCoreBl.Interfaces;
using AspCoreBl.Misc;
using AspCoreBl.ModelDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AngularWithAspCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : BaseController
    {
        private IApplicationUserRepository _aApplicationUserRepository;

        public ApplicationUserController(IApplicationUserRepository ApplicationUserRepository)
        {
            _aApplicationUserRepository = ApplicationUserRepository;
        }

        [HttpPost]
        [Route("PostApplicationUser")]
        public async Task<IActionResult> PostApplicationUser(IdentityUserDTO dto)
        {
            try
            {
                var result = await _aApplicationUserRepository.PostApplicationUser(dto);
                return OKResult(result.Key, result.Value);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [Route("ConfirmEmailAsync")]
        public async Task<IActionResult> ConfirmEmailAsync(string email, string code)
        {
            try
            {   
                var result = await _aApplicationUserRepository.ConfirmEmailAsync(email, code);
                if (result)
                    return OKResult(1, "Email confirmed");

                return OKResult(0, "Link expired.");

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        

        [HttpGet]
        [Route("UserExist")]
        public async Task<bool> UserExist(IdentityUserDTO dto)
        {
            try
            {
                return await _aApplicationUserRepository.UserExist(dto);                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        [Route("LoginAsync")]
        public async Task<IActionResult> LoginAsync(IdentityUserDTO dto)
        {
            try
            {
                var result = await _aApplicationUserRepository.LoginAsync(dto);
                return OKResult(result.Key, result.Value);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("forgotpassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
                return OtherResult(HttpStatusCode.BadRequest, "Email is required.");

            var result = await _aApplicationUserRepository.ForgotPasswordAsync(email);

            if (result)
                return OKResult(1, "Email sent for resetting password.");

            return OtherResult(HttpStatusCode.BadRequest, "User not found for provided email.");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return InvalidModelStateResult(ModelState);

            var result = await _aApplicationUserRepository.ResetPasswordAsync(model);
            switch (result.Key)
            {
                case 0:
                    return OtherResult(HttpStatusCode.BadRequest, "User not found for provided email.");
                case 1:
                    return OKResult(1, "Password successfully reset. Login successful.", result.Value);
                case 2:
                    return OKResult(2, "Link expired.");
            }

            //Will never come to this
            throw new AppException("Something went wrong.");
        }
        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _aApplicationUserRepository.LogoutAsync();
            return OKResult(1, "Logout successful.");
        }
    }
}