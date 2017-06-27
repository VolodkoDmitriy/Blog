using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Blog1.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "User")]
        public string User { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "comments")]
        public string Text { get; set; }
    }
}