using prjLion.Repository.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjLion.Repository.Interfaces
{
    public interface ILionPostRepositorys
    {
        /// <summary>
        /// 註冊帳號
        /// </summary>
        /// <param name="memberAccountDto"></param>
        /// <returns></returns>
        public Task<bool> InsertAccount(MemberAccountDto memberAccountDto);

        /// <summary>
        /// 新增留言
        /// </summary>
        /// <param name="createMsgDto"></param>
        /// <returns></returns>
		public Task<bool> InsertMsg(CreateMsgDto createMsgDto);

        /// <summary>
        /// 修改留言
		/// 指定留言編號 (流水號)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editMsgDto"></param>
        /// <returns></returns>
        public Task<bool> UpdateMsg(int id, EditMsgDto editMsgDto);

        /// <summary>
        /// 刪除留言
        /// 指定留言編號 (流水號)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> DeleteMsg(int id);

        /// <summary>
        /// 上傳圖片
        /// </summary>
        /// <param name="createImgDto"></param>
        /// <returns></returns>
        public Task<bool> InsertPicture(CreateImgDto createImgDto);
    }
}