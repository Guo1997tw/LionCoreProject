using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjLion.Repository.Models.Dto
{
	public class CreateMsgDto
	{
		/// <summary>
		/// 會員編號
		/// 流水號
		/// </summary>
		public int MemberId { get; set; }

		/// <summary>
		/// 留言內容
		/// </summary>
		public string MessageText { get; set; } = null!;

		/// <summary>
		/// 留言時間
		/// 時間戳
		/// </summary>
		public DateTime MessageTime { get; set; }
	}
}