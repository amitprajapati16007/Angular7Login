using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspCoreBl.Bl;
using AspCoreBl.ModelDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AngularWithAspCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        
        private static UserManager<IdentityUser> _userManager;
        private static SignInManager<IdentityUser> _signInManager;

        public ApplicationUserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("PostApplicationUser")]
        public static async Task<Object> PostApplicationUser(IdentityUserDTO dto)
        {
            var identityUser = new IdentityUser()
            {
                UserName = dto.UserName,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(identityUser, dto.Password);
            return result;

        }
        //[HttpPost]
        //[Route("PostApplicationUser")]
        //public async Task<Object> PostApplicationUser(IdentityUserDTO dto)
        //{
        //    try
        //    {
        //        var result= await ApplicationUserBl.PostApplicationUser(dto);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
    }
}