﻿namespace prjLionMVC.Models.HttpClients.Out
{
    public class ResultImageViewModel
    {
        /// <summary>
        /// 會員編號
        /// </summary>
        public int MemberId { get; set; }

        /// <summary>
        /// 圖片名稱
        /// </summary>
        public string? PictureName { get; set; }

        /// <summary>
        /// 實體圖片
        /// </summary>
        public IFormFile? formFile { get; set; }
    }
}