using System.ComponentModel.DataAnnotations;


namespace Blog1.Models
{
    public class NewCommentModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [ScaffoldColumn(false)]
        public int UserId { get; set; }
        [ScaffoldColumn(false)]
        public int PostId { get; set; }

        [Display(Name = "comment")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "The field can not be empty!")]
        public string Text { get; set; }
    }
}