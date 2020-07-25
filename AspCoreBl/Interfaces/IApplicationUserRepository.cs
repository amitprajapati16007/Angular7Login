using AspCoreBl.Model;
using AspCoreBl.ModelDTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AspCoreBl.Interfaces
{
    public interface IApplicationUserRepository 
    {
        Task<KeyValuePair<int, string>> PostApplicationUser(IdentityUserDTO dto);
        Task<bool> UserExist(IdentityUserDTO dto);
        Task<KeyValuePair<string, LoginSuccessViewModel>> LoginAsync(IdentityUserDTO dto);
        Task<bool> ConfirmEmailAsync(string email, string code);
        Task<bool> ForgotPasswordAsync(string email);
        Task<KeyValuePair<int, LoginSuccessViewModel>> ResetPasswordAsync(ResetPasswordViewModel model);
        Task<KeyValuePair<int, object>> ChangePasswordAsync(ChangePasswordViewModel model, ApplicationUser user);
        Task<ApplicationUser> GetSingleAsyncs(Expression<Func<ApplicationUser, bool>> predicate, params Expression<Func<ApplicationUser, object>>[] includeProperties);
        Task LogoutAsync();

        Task<KeyValuePair<string, LoginSuccessViewModel>> ExternalLoginAsync(SocialUser dto);
    }
}
