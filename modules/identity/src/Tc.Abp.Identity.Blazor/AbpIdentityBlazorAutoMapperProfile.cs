using AutoMapper;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;

namespace Tc.Abp.Identity.Blazor;

public class AbpIdentityBlazorAutoMapperProfile : Profile
{
    public AbpIdentityBlazorAutoMapperProfile()
    {
        CreateMap<IdentityUserDto, IdentityUserUpdateDto>()
            .MapExtraProperties()
            .Ignore(x => x.Password)
            .Ignore(x => x.RoleNames);

        CreateMap<IdentityRoleDto, IdentityRoleUpdateDto>()
            .MapExtraProperties();
    }
}
