using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjLion.Service.Interfaces;
using prjLion.WebAPI.Models;

namespace prjLion.WebAPI.Controllers
{
    [Route("api/LionGet/[action]")]
    [ApiController]
    public class LionGetController : ControllerBase
    {
        private readonly ILionGetServices _lionGetServices;
        private readonly IMapper _mapper;

        public LionGetController(ILionGetServices lionGetServices, IMapper mapper)
        {
            _lionGetServices = lionGetServices;
            _mapper = mapper;
        }

        /// <summary>
        /// 搜尋單一使用者留言
		/// 指定使用者姓名
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet("{userName}")] public async Task<ActionResult<IEnumerable<MessageListViewModel>?>> SearchMsgUserName(string userName)
        {
            var queryBo = await _lionGetServices.GetMsgByUserName(userName);

            if(queryBo == null || !queryBo.Any()) { return NotFound("無法搜尋到該筆使用者資料"); }

            return Ok(_mapper.Map<IEnumerable<MessageListViewModel>?>(queryBo));
        }
    }
}