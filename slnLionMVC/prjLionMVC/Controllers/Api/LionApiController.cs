using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prjLionMVC.Interfaces;
using prjLionMVC.Models;
using prjLionMVC.Models.Entity;
using prjLionMVC.Models.ViewModels;

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
        public IEnumerable<MsgListViewModel> GetMsg()
        {
            return _lion.GetAllMsg().Select(x => new MsgListViewModel
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