using System.ComponentModel.DataAnnotations;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Reflection;

namespace Tc.Abp.AspNetCore;

public static class BlazoriseUiObjectExtensionPropertyInfoExtensions
{
    private static readonly HashSet<Type> NumberTypes = new HashSet<Type> {
            typeof(int),
            typeof(long),
            typeof(byte),
            typeof(sbyte),
            typeof(short),
            typeof(ushort),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(float),
            typeof(double),
            typeof(decimal),
            typeof(int?),
            typeof(long?),
            typeof(byte?),
            typeof(sbyte?),
            typeof(short?),
            typeof(ushort?),
            typeof(uint?),
            typeof(long?),
            typeof(ulong?),
            typeof(float?),
            typeof(double?),
            typeof(decimal?)
        };

    private static readonly HashSet<Type> TextEditSupportedAttributeTypes = new HashSet<Type> {
            typeof(EmailAddressAttribute),
            typeof(UrlAttribute),
            typeof(PhoneAttribute)
        };

    public static string GetDateEditInputFormatOrNull(this IBasicObjectExtensionPropertyInfo property)
    {
        if (property.IsDate())
        {
            return "{0:yyyy-MM-dd}";
        }

        if (property.IsDateTime())
        {
            return "{0:yyyy-MM-ddTHH:mm}";
        }

        return null;
    }

    public static string GetTextInputValueOrNull(this IBasicObjectExtensionPropertyInfo property, object value)
    {
        if (value == null)
        {
            return null;
        }

        if (TypeHelper.IsFloatingType(property.Type))
        {
            return value.ToString()?.Replace(',', '.');
        }

        return value.ToString();
    }

    public static T GetInputValueOrDefault<T>(this IBasicObjectExtensionPropertyInfo property, object value)
    {
        if (value == null)
        {
            return default;
        }

        return (T)value;
    }

    
}

