using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Blog1.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Post content")]
        public string Text { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Tags")]
        public string Tags { get; set; }
        [DataType(DataType.Html)]
        [Display(Name = "Comments")]
        public string Comments { get; set; }
    }
}