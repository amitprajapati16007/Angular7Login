using AspCoreBl.ModelDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspCoreBl.Interfaces
{
    public interface IApplicationUserRepository 
    {
        Task<KeyValuePair<int, string>> PostApplicationUser(IdentityUserDTO dto);
        Task<bool> UserExist(IdentityUserDTO dto);
        Task<KeyValuePair<string, LoginSuccessViewModel>> LoginAsync(IdentityUserDTO dto);
        Task<bool> ConfirmEmailAsync(string UserName, string code);
    }
}
