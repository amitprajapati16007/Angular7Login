using System;
using System.Collections.Generic;
using System.Text;

namespace AspCoreBl.ModelDTO
{
    public class IdentityUserDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class SocialUser
    {
        public string provider { get; set; }
        public string id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string photoUrl { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string authToken { get; set; }
        public string idToken { get; set; }
    }
}
