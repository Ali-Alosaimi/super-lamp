using System;
namespace Models.Response.Auth
{
    public class LoginResponse
    {
        public long userId { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string roleName { get; set; }
        public long roleId { get; set; }
    }
}
