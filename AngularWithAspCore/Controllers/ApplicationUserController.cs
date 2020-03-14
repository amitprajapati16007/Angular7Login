using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularWithAspCore.Misc;
using AspCoreBl.Bl;
using AspCoreBl.Interfaces;
using AspCoreBl.ModelDTO;
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
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _aApplicationUserRepository.LogoutAsync();
            return OKResult(1, "Logout successful.");
        }
    }
}