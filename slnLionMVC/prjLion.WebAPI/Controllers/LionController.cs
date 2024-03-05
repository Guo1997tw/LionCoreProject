using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjLion.Service.Interfaces;
using prjLion.Service.Models.Bo;
using prjLion.WebAPI.Models;

namespace prjLion.WebAPI.Controllers
{
    [Route("api/Lion/[action]")]
    [ApiController]
    public class LionController : ControllerBase
    {
        private readonly ILionGetServices _lionGetServices;
        private readonly ILionPostServices _lionPostServices;
        private readonly IMapper _mapper;

        public LionController(ILionGetServices lionGetServices, ILionPostServices lionPostServices, IMapper mapper)
        {
            _lionGetServices = lionGetServices;
            _lionPostServices = lionPostServices;
            _mapper = mapper;
        }

        /// <summary>
        /// 搜尋單一使用者留言
		/// 指定使用者姓名
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet("{userName}")]
        public async Task<ActionResult<IEnumerable<MessageListViewModel>?>> SearchMsgUserName(string userName)
        {
            var queryBo = await _lionGetServices.GetMsgByUserName(userName);

            if(queryBo == null || !queryBo.Any()) { return NotFound("無法搜尋到該筆使用者資料"); }

            return Ok(_mapper.Map<IEnumerable<MessageListViewModel>?>(queryBo));
        }

        /// <summary>
        /// 註冊帳號
        /// </summary>
        /// <param name="memberAccountViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> RegisterMember(MemberAccountViewModel memberAccountViewModel)
        {
            var mapper = _mapper.Map<MemberAccountViewModel, MemberAccountBo>(memberAccountViewModel);

            return Ok(await _lionPostServices.CreateAccount(mapper));
        }
    }
}