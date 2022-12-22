using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Localization;

namespace Volo.Abp.ObjectExtending;
public static class EnumHelper
{
    public static string GetLocalizedMemberName(Type enumType, object value, IStringLocalizerFactory stringLocalizerFactory)
    {
        var memberName = enumType.GetEnumName(value);
        var localizedMemberName = AbpInternalLocalizationHelper.LocalizeWithFallback(
            new[]
            {
                        stringLocalizerFactory.CreateDefaultOrNull()
            },
            new[]
            {
                        $"Enum:{enumType.Name}.{memberName}",
                        $"{enumType.Name}.{memberName}",
                        memberName
            },
            memberName
        );

        return localizedMemberName;
    }
}

public static class ObjectExtensionPropertyInfoBlazorExtensions
{
    private static readonly Type[] DateTimeTypes =
    {
            typeof(DateTime),
            typeof(DateTime?),
            typeof(DateTimeOffset),
            typeof(DateTimeOffset?)
        };

    public static bool IsDate(this IBasicObjectExtensionPropertyInfo property)
    {
        return DateTimeTypes.Contains(property.Type) &&
               property.GetDataTypeOrNull() == DataType.Date;
    }

    public static bool IsDateTime(this IBasicObjectExtensionPropertyInfo property)
    {
        return DateTimeTypes.Contains(property.Type) &&
               !property.IsDate();
    }

    public static DataType? GetDataTypeOrNull(this IBasicObjectExtensionPropertyInfo property)
    {
        return property
            .Attributes
            .OfType<DataTypeAttribute>()
            .FirstOrDefault()?.DataType;
    }
}
