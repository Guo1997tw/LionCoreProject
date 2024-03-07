using AutoMapper;
using prjLion.Repository.Models.Dto;
using prjLion.Service.Models.Bo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjLion.Service.Mapping
{
    public class ServicesProfile : Profile
    {
        public ServicesProfile()
        {
            // 搜尋單一使用者留言
            // 指定使用者姓名
            CreateMap<MessageListDto, MessageListBo>();

            // 註冊帳號
            // 登入帳號
            CreateMap<MemberAccountBo, MemberAccountDto>().ReverseMap();

			// 新增留言
			CreateMap<CreateMsgBo, CreateMsgDto>();

            // 修改留言
            CreateMap<EditMsgBo, EditMsgDto>();
        }
    }
}