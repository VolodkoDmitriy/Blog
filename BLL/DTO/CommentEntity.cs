using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class CommentEntity : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Text { get; set; }
    }
}
