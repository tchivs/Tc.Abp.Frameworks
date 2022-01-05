using Volo.Abp.Reflection;

namespace Tchivs.Abp.Account.Permissions
{
    public class AccountPermissions
    {
        public const string GroupName = "Account";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(AccountPermissions));
        }
    }
}