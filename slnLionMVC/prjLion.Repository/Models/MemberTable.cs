using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjLion.Repository.Models
{
    public class MemberTable
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; } = null!;
        public string Account { get; set; } = null!;
        public string HashPassword { get; set; } = null!;
        public string SaltPassword { get; set; } = null!;
    }
}