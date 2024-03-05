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
        }
    }
}