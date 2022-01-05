using AutoMapper;
using Volo.Abp.Account;

namespace Tchivs.Abp.Account.Blazor;

public class AbpAccountBlazorAutoMapperProfile : Profile
{
    public AbpAccountBlazorAutoMapperProfile()
    {
        CreateMap<ProfileDto, PersonalInfoModel>();
    }
}