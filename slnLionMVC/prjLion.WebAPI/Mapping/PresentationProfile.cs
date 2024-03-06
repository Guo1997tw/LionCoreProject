using AutoMapper;
using prjLion.Service.Models.Bo;
using prjLion.WebAPI.Models;

namespace prjLion.WebAPI.Mapping
{
    public class PresentationProfile : Profile
    {
        public PresentationProfile()
        {
            // 搜尋單一使用者留言
            // 指定使用者姓名
            CreateMap<MessageListBo, MessageListViewModel>();

            // 註冊帳號
            CreateMap<MemberAccountViewModel, MemberAccountBo>();

            // 帳號資訊
            CreateMap<MemberAccountBo, MemberAccountInfoViewModel>();

            // 新增留言
			CreateMap<CreateMsgViewModel, CreateMsgBo>();
		}
    }
}