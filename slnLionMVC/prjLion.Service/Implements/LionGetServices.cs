using AutoMapper;
using prjLion.Repository.Interfaces;
using prjLion.Repository.Models.Dto;
using prjLion.Service.Interfaces;
using prjLion.Service.Models.Bo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjLion.Service.Implements
{
    public class LionGetServices : ILionGetServices
    {
        private readonly ILionGetRepositorys _lionGetRepositorys;
        private readonly IMapper _mapper;

        public LionGetServices(ILionGetRepositorys lionGetRepositorys, IMapper mapper)
        {
            _lionGetRepositorys = lionGetRepositorys;
            _mapper = mapper;
        }

        /// <summary>
        /// 搜尋單一使用者留言
		/// 指定使用者姓名
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<MessageListBo>?> GetMsgByUserName(string userName)
        {
            var queryDto = await _lionGetRepositorys.GetMsgByUserName(userName);

            if (queryDto == null) { return null; }

            return _mapper.Map<IEnumerable<MessageListBo>?>(queryDto);
        }

        /// <summary>
        /// 分頁功能
        /// 輸入第幾頁
        /// </summary>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<MessageListBo>> GetMsgPage(int pageNum)
        {
            var queryDto = await _lionGetRepositorys.GetMsgPageNum(pageNum);

            return _mapper.Map<IEnumerable<MessageListBo>>(queryDto);
        }

        /// <summary>
        /// 取的留言版總筆數
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> GetMsgCount()
        {
            var queryDto = await _lionGetRepositorys.GetMsgPageCount();

            return _mapper.Map<int>(queryDto);
        }

        /// <summary>
        /// 同時取得資料分頁與總筆數
        /// </summary>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<PaginationCountBo<MessageListBo>> GetPaginationCountData(int pageNum)
        {
            var queryDataDto = await _lionGetRepositorys.GetPaginationCount(pageNum);
            
            return _mapper.Map<PaginationCountBo<MessageListBo>>(queryDataDto);
        }

        /// <summary>
        /// 同時取得資料分頁與總筆數、搜尋單一使用者留言
        /// 指定使用者姓名、指定頁數
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<PaginationCountBo<MessageListBo>> GetMsgByUserNamePaginationCountData(string userName, int pageNum)
        {
            var queryDataDto = await _lionGetRepositorys.GetMsgByUserNamePaginationCount(userName, pageNum);

            return _mapper.Map<PaginationCountBo<MessageListBo>>(queryDataDto);
        }
    }
}