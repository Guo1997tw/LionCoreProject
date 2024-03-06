﻿namespace prjLion.WebAPI.Models
{
    public class MemberAccountInfoViewModel
    {
        /// <summary>
        /// 會員編號
        /// </summary>
        public int MemberId { get; set; }

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