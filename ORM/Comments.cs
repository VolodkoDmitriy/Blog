namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comments
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public string commentText { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }

        public virtual Posts Posts { get; set; }

        public virtual Users Users { get; set; }
    }
}
