﻿namespace prjLionMVC.Models.ViewModels
{
    public class CreateAccountViewModel
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
    }
}