using AutoMapper;
using prjLion.Repository.Interfaces;
using prjLion.Service.Interfaces;
using prjLion.Service.Models.Bo;

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
        /// 同時取得資料分頁與總筆數
        /// </summary>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<PaginationCountBo<MessageListBo>?> GetPaginationCountData(int pageNum)
        {
            var queryDataDto = await _lionGetRepositorys.GetPaginationCount(pageNum);

            if (queryDataDto.ItemData == null || !queryDataDto.ItemData.Any()) { throw new KeyNotFoundException($"第{pageNum}頁無資料"); }
            
            return _mapper.Map<PaginationCountBo<MessageListBo>?>(queryDataDto);
        }

        /// <summary>
        /// 同時取得資料分頁與總筆數、搜尋單一使用者留言
        /// 指定使用者姓名、指定頁數
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<PaginationCountBo<MessageListBo>?> GetMsgByUserNamePaginationCountData(string userName, int pageNum)
        {
            var queryDataDto = await _lionGetRepositorys.GetMsgByUserNamePaginationCount(userName, pageNum);

            if (queryDataDto.ItemData == null || !queryDataDto.ItemData.Any()) { throw new KeyNotFoundException($"查無{userName}使用者"); }

            return _mapper.Map<PaginationCountBo<MessageListBo>?>(queryDataDto);
        }
    }
}