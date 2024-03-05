using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjLion.Repository.Models
{
    public class ErrorLogTable
    {
        public int ErrorId { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; } = null!;
        public DateTime? DateCreated { get; set; }
    }
}