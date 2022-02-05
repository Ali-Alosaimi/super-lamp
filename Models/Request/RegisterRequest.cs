using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Request
{
    public class RegisterRequest
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string password { get; set; }
    }
}
