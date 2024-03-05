using AutoMapper;
using prjLion.Repository.Interfaces;
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
    }
}