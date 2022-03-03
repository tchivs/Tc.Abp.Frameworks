using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Localization;

namespace Tchivs.Abp.UI.Components
{

    public abstract class BaseLanguageSwitch : AbpBlazorComponent
    {
        [NotNull, Inject] protected ILanguageProvider LanguageProvider { get; set; }
        protected IReadOnlyList<LanguageInfo> Languages { get; set; }
        protected LanguageInfo Language { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Languages = await LanguageProvider.GetLanguagesAsync();
            Language = await this.GetCurrentLanguage(Languages);
        }
        protected virtual Task<LanguageInfo> GetCurrentLanguage(IReadOnlyList<LanguageInfo> languages)
        {
            var current = languages.FindByCulture(
             CultureInfo.CurrentCulture.Name,
             CultureInfo.CurrentUICulture.Name
             );
            return Task.FromResult(current);
        }
        protected virtual Task ChangeLanguage(LanguageInfo language)
        {
            var relativeUrl = Navigation.Uri.RemovePreFix(Navigation.BaseUri).EnsureStartsWith('/');
            Navigation.NavigateTo(
                $"/Abp/Languages/Switch?culture={language.CultureName}&uiCulture={language.UiCultureName}&returnUrl={relativeUrl}",
                forceLoad: true
            );
            return Task.CompletedTask;
        }
    }
}
