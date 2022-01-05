using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Tchivs.Abp.Account.MongoDB
{
    public static class AccountMongoDbContextExtensions
    {
        public static void ConfigureAccount(
            this IMongoModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
        }
    }
}
