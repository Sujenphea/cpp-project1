using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace A2.Models
{
    public class User
    {
        [Key] public string UserName { get; set; }
        [Required] public string Password { get; set; }
        public string Address { get; set; }
    }
}