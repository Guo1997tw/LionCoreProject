using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjLion.Repository.Models
{
    public class MessageBoardTable
    {
        public int MessageBoardId { get; set; }
        public int MemberId { get; set; }
        public string MessageText { get; set; } = null!;
        public DateTime MessageTime { get; set; }
    }
}