﻿using AspCoreBl.ModelDTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspCoreBl.Bl
{
    public class ApplicationUserBl
    {
        private static UserManager<IdentityUser> _userManager;
        private static SignInManager<IdentityUser> _signInManager;

        public ApplicationUserBl(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

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
    }
}
