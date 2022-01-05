using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Volo.Abp.Account.Localization;
using Volo.Abp.Identity;
using Volo.Abp.Users;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Tchivs.Abp.Account.Blazor.Components;
using Volo.Abp;

namespace Tchivs.Abp.Account.Blazor.ProfileManagement
{
    public class ProfileManagementPageGroup
    {
        public string Id
        {
            get => _id;
            set => _id = Check.NotNullOrWhiteSpace(value, nameof(Id));
        }
        private string _id;

        public string DisplayName
        {
            get => _displayName;
            set => _displayName = Check.NotNullOrWhiteSpace(value, nameof(DisplayName));
        }
        private string _displayName;

        public Type ComponentType
        {
            get => _componentType;
            set => _componentType = Check.NotNull(value, nameof(ComponentType));
        }
        private Type _componentType;

        public object Parameter { get; set; }

        public ProfileManagementPageGroup([NotNull] string id, [NotNull] string displayName, [NotNull] Type componentType, object parameter = null)
        {
            Id = id;
            DisplayName = displayName;
            ComponentType = componentType;
            Parameter = parameter;
        }
    }
    public class ProfileManagementPageCreationContext
    {
        public IServiceProvider ServiceProvider { get; }

        public List<ProfileManagementPageGroup> Groups { get; }

        public ProfileManagementPageCreationContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;

            Groups = new List<ProfileManagementPageGroup>();
        }
    }
    public interface IProfileManagementPageContributor
    {
        Task ConfigureAsync(ProfileManagementPageCreationContext context);
    }
    public class ProfileManagementPageOptions
    {
        public List<IProfileManagementPageContributor> Contributors { get; }

        public ProfileManagementPageOptions()
        {
            Contributors = new List<IProfileManagementPageContributor>();
        }
    }
    public class AccountProfileManagementPageContributor : IProfileManagementPageContributor
    {
        public async Task ConfigureAsync(ProfileManagementPageCreationContext context)
        {
            var l = context.ServiceProvider.GetRequiredService<IStringLocalizer<AccountResource>>();

            if (await IsPasswordChangeEnabled(context))
            {
                context.Groups.Add(
                    new ProfileManagementPageGroup(
                        "Volo.Abp.Account.Password",
                        l["ProfileTab:Password"],
                        typeof(AccountProfilePasswordManagementGroupViewComponent)
                    )
                );
            }

            context.Groups.Add(
                new ProfileManagementPageGroup(
                    "Volo.Abp.Account.PersonalInfo",
                    l["ProfileTab:PersonalInfo"],
                    typeof(AccountProfilePersonalInfoManagementGroupViewComponent)
                )
            );
        }

        protected virtual async Task<bool> IsPasswordChangeEnabled(ProfileManagementPageCreationContext context)
        {
            var userManager = context.ServiceProvider.GetRequiredService<IdentityUserManager>();
            var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();

            var user = await userManager.GetByIdAsync(currentUser.GetId());

            return !user.IsExternal;
        }
    }

}
