using System;
using System.Collections.Generic;
using System.Text;

namespace AspCoreBl.ModelDTO
{
    public class LoginSuccessViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }

        public int RoleId { get; set; }
    }
}
