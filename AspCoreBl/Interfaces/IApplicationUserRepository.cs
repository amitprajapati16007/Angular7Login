using AspCoreBl.ModelDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspCoreBl.Interfaces
{
    public interface IApplicationUserRepository 
    {
        Task<Object> PostApplicationUser(IdentityUserDTO dto);
        Task<bool> UserExist(IdentityUserDTO dto);
        Task<LoginSuccessViewModel> LoginAsync(IdentityUserDTO dto);
    }
}
