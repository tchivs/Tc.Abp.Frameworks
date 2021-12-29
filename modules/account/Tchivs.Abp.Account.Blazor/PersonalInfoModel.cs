using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Identity;
using Volo.Abp.Validation;

namespace Tchivs.Abp.Account.Blazor;

public class PersonalInfoModel : IHasConcurrencyStamp
{
    [Required]
    [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxUserNameLength))]
    [Display(Name = "DisplayName:UserName")]
    public string UserName { get; set; }

    [Required]
    [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxEmailLength))]
    [Display(Name = "DisplayName:Email")]
    public string Email { get; set; }

    [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxNameLength))]
    [Display(Name = "DisplayName:Name")]
    public string Name { get; set; }

    [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxSurnameLength))]
    [Display(Name = "DisplayName:Surname")]
    public string Surname { get; set; }

    [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPhoneNumberLength))]
    [Display(Name = "DisplayName:PhoneNumber")]
    public string PhoneNumber { get; set; }

    [HiddenInput]
    public string ConcurrencyStamp { get; set; }
}