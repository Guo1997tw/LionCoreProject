using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjLion.Service.Interfaces;
using prjLion.Service.Models.Bo;
using prjLion.WebAPI.Models;
using prjLion.WebAPI.Models.HttpClients.Inp;
using prjLion.WebAPI.Models.HttpClients.Out;

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
        /// 分頁功能
        /// 輸入第幾頁
        /// </summary>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<MessageListViewModel>> GetMsgPageAll(int pageNum)
        {
            var queryBo = await _lionGetServices.GetMsgPage(pageNum);

            return _mapper.Map<IEnumerable<MessageListViewModel>>(queryBo);
        }

        /// <summary>
        /// 取的留言版總筆數
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<int> GetDataCount()
        {
            var queryBo = await _lionGetServices.GetMsgCount();

            return _mapper.Map<int>(queryBo);
        }

        /// <summary>
        /// 搜尋單一使用者留言
		/// 指定使用者姓名
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpPost("{userName}")]
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

        /// <summary>
        /// 登入帳號
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<LoginInfoViewModel> LoginMember([FromBody] LoginMemberViewModel loginMemberHttpViewModel)
        {
            var result = await _lionPostServices.CheckMember(loginMemberHttpViewModel.account, loginMemberHttpViewModel.hashPassword);

            return _mapper.Map<LoginInfoViewModel>(result);
        }

        /// <summary>
        /// 新增留言
        /// </summary>
        /// <param name="createMsgViewModel"></param>
        /// <returns></returns>
        [HttpPost]
		public async Task<bool> CreateUserMsg(CreateMsgViewModel createMsgViewModel)
		{
			var mapper = _mapper.Map<CreateMsgViewModel, CreateMsgBo>(createMsgViewModel);

			await _lionPostServices.CreateMsg(mapper);

			return true;
		}

        /// <summary>
        /// 修改留言
		/// 指定留言編號 (流水號)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editMsgViewModel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<bool> UpdateUserMsg(int id, [FromBody] EditMsgViewModel editMsgViewModel)
        {
            var mapper = _mapper.Map<EditMsgViewModel, EditMsgBo>(editMsgViewModel);

            await _lionPostServices.EditMsg(id, mapper);

            return true;
        }

		/// <summary>
		/// 刪除留言
		/// 指定留言編號 (流水號)
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete("{id}")]
        public async Task<bool> RemoveMemberMsg(int id)
        {
            return await _lionPostServices.DeleteMemberMsg(id);
        }
	}
}