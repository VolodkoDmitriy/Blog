using System;
using System.ComponentModel.DataAnnotations;

namespace Blog1.Models
{
    public class PostNewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Post")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "The field can not be empty!")]                
        public string Text { get; set; }
        
    }
}