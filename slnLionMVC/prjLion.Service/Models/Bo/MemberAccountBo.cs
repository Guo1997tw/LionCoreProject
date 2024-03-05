using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjLion.Service.Models.Bo
{
    public class MemberAccountBo
    {
        /// <summary>
        /// 會員姓名
        /// </summary>
        public string MemberName { get; set; } = null!;

        /// <summary>
        /// 登入帳號
        /// </summary>
        public string Account { get; set; } = null!;

        /// <summary>
        /// 登入密碼 (雜湊)
        /// </summary>
        public string HashPassword { get; set; } = null!;

        /// <summary>
        /// 鹽值
        /// 亂數產生
        /// </summary>
        public string SaltPassword { get; set; } = null!;
    }
}