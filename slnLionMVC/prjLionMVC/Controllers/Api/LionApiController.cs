using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjLionMVC.Interfaces;
using prjLionMVC.Models;
using prjLionMVC.Models.Entity;

namespace prjLionMVC.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LionApiController : ControllerBase
    {
        private readonly ILion _lion;

        public LionApiController(ILion lion)
        {
            _lion = lion;
        }

        [HttpGet]
        public IEnumerable<MsgListDto> GetMsg()
        {
            return _lion.GetAllMsg().Select(x => new MsgListDto
            {
                MessageBoardId = x.MessageBoardId,
                Account = x.Account,
                MemberName = x.MemberName,
                MessageText = x.MessageText,
                MessageTime = x.MessageTime,
            }).ToList();
        }
    }
}