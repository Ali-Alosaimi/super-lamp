using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Request
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email required")]
        [Display(Name ="Email")]
        public string email { get; set; }

        [Required(ErrorMessage = "Password required")]
        [Display(Name = "Password")]
        public string password { get; set; }
    }
}
