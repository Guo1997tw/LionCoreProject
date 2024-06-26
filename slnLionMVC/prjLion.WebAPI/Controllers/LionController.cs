﻿using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
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
        private string _rootPath;

        public LionController(ILionGetServices lionGetServices, ILionPostServices lionPostServices, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _lionGetServices = lionGetServices;
            _lionPostServices = lionPostServices;
            _mapper = mapper;
            _rootPath = $@"{webHostEnvironment.WebRootPath}\Images\";
        }

        /// <summary>
        /// 同時取得資料分頁與總筆數
        /// </summary>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [HttpPost("{pageNum}")]
        public async Task<ActionResult<ResultTViewModel<PaginationCountViewModel>>> GetPaginationCountDataAll(int pageNum)
        {
            var queryBo = await _lionGetServices.GetPaginationCountData(pageNum);

            var mapper = _mapper.Map<PaginationCountViewModel>(queryBo);

            return Ok(new ResultTViewModel<PaginationCountViewModel>
            {
                Success = true,
                Message = "資料加載成功",
                Data = new PaginationCountViewModel
                {
                    ItemData = mapper.ItemData,
                    CountData = mapper.CountData,
                }
            });
        }

        /// <summary>
        /// 同時取得資料分頁與總筆數、搜尋單一使用者留言
        /// 指定使用者姓名、指定頁數
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [HttpPost("{userName}/{pageNum}")]
        public async Task<ActionResult<ResultTViewModel<PaginationCountViewModel>>> GetMsgByUserNamePaginationCountDataAll(string userName, int pageNum = 1)
        {
            var queryBo = await _lionGetServices.GetMsgByUserNamePaginationCountData(userName, pageNum);

            var mapper = _mapper.Map<PaginationCountViewModel>(queryBo);

            return Ok(new ResultTViewModel<PaginationCountViewModel>
            {
                Success = true,
                Message = "資料加載成功",
                Data = new PaginationCountViewModel
                {
                    ItemData = mapper.ItemData,
                    CountData = mapper.CountData,
                }
            });
        }

        /// <summary>
        /// 註冊帳號
        /// </summary>
        /// <param name="memberAccountViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<object>> RegisterMember(MemberAccountViewModel memberAccountViewModel)
        {
            var mapper = _mapper.Map<MemberAccountViewModel, MemberAccountBo>(memberAccountViewModel);

            await _lionPostServices.CreateAccount(mapper);

            return Ok(new ResultTViewModel<object>
            {
                Success = true,
                Message = "註冊成功",
            });
        }

        /// <summary>
        /// 登入帳號
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ResultTViewModel<ResultLoginViewModel>>> LoginMember([FromBody] LoginMemberViewModel loginMemberHttpViewModel)
        {
            var result = await _lionPostServices.CheckMember(loginMemberHttpViewModel.account, loginMemberHttpViewModel.hashPassword);

            if (result == null) { return NotFound($"無此帳號: {loginMemberHttpViewModel.account}"); }

            return Ok(new ResultTViewModel<ResultLoginViewModel>
            {
                Success = true,
                Message = "登入成功",
                Data = new ResultLoginViewModel
                {
                    memberId = result.MemberId,
                    account = result.Account,
               }
            });
        }

        /// <summary>
        /// 新增留言
        /// </summary>
        /// <param name="createMsgViewModel"></param>
        /// <returns></returns>
        [HttpPost]
		public async Task<ActionResult<ResultTViewModel<CreateMsgBo>>> CreateUserMsg(CreateMsgViewModel createMsgViewModel)
		{
			var mapper = _mapper.Map<CreateMsgViewModel, CreateMsgBo>(createMsgViewModel);

			await _lionPostServices.CreateMsg(mapper);

            return Ok(new ResultTViewModel<CreateMsgBo>
            {
                Success = true,
                Message = "新增成功",
                Data = mapper
            });
		}

        /// <summary>
        /// 修改留言
		/// 指定留言編號 (流水號)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editMsgViewModel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ResultTViewModel<EditMsgBo>>> UpdateUserMsg(int id, [FromBody] EditMsgViewModel editMsgViewModel)
        {
            var mapper = _mapper.Map<EditMsgViewModel, EditMsgBo>(editMsgViewModel);

            await _lionPostServices.EditMsg(id, mapper);

            return Ok(new ResultTViewModel<EditMsgBo>
            {
                Success = true,
                Message = "修改成功",
                Data = mapper
            });
        }

		/// <summary>
		/// 刪除留言
		/// 指定留言編號 (流水號)
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete("{id}")]
        public async Task<ActionResult<ResultTViewModel<int>>> RemoveMemberMsg(int id)
        {
            var result = await _lionPostServices.DeleteMemberMsg(id);

            if (result == false) return NotFound($"無此資料: {id}");

			return Ok(new ResultTViewModel<int>
            {
                Success = true,
                Message = "刪除成功",
                Data = id
            });
        }

        /// <summary>
        /// 上傳圖片
        /// </summary>
        /// <param name="createImgViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ResultTViewModel<CreateImgBo>>> UploadPicture([FromBody] CreateImgViewModel createImgViewModel)
        {
            if (createImgViewModel.formFile == null) return BadRequest("圖片未上傳");

            if (createImgViewModel.formFile.Length > 0)
            {
                string fileNameTemp = $"{createImgViewModel.formFile.FileName}";
                string savePath = $@"{_rootPath}{fileNameTemp}";

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    createImgViewModel.formFile.CopyTo(stream);
                }
            }

            var mapper = _mapper.Map<CreateImgBo>(createImgViewModel);

            await _lionPostServices.CreatePicture(mapper);

            return Ok(new ResultTViewModel<CreateImgBo>
            {
                Success = true,
                Message = "上傳成功",
                Data = mapper
            });
        }
    }
}