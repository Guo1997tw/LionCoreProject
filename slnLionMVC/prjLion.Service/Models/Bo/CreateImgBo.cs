using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjLion.Service.Models.Bo
{
    public class CreateImgBo
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
        /// 建立時間
        /// </summary>
        public DateTime CreateTime { get; set; } = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time"));
    }
}