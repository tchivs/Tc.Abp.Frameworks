using System;
using System.Collections.Generic;
using System.Text;

namespace Tchivs.Abp.IdentityServer.Permissions
{
    public  class IdentityServerPermissions
    {
        public const string GroupName = "IdentityServer";
        public const string Default = $"{GroupName}.{nameof(Default)}";


        public static class Client
        {
            public const string Default = GroupName + ".Client";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string Enable = Default + ".Enable";
        }


        public static class ApiResource
        {
            public const string Default = GroupName + ".ApiResource";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }

        public static class ApiScope
        {
            public const string Default = GroupName + ".ApiScope";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }

        public static class IdentityResources
        {
            public const string Default = GroupName + ".IdentityResources";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }

    }
}

