using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog1.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }
}