﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class DalComment : IEntity
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Text { get; set; }
        
    }
}
