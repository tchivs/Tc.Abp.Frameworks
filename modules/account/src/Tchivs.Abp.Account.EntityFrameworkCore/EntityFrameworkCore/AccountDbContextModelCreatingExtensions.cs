using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Tchivs.Abp.Account.EntityFrameworkCore
{
    public static class AccountDbContextModelCreatingExtensions
    {
        public static void ConfigureAccount(
            this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure all entities here. Example:

            builder.Entity<Question>(b =>
            {
                //Configure table & schema name
                b.ToTable(AccountDbProperties.DbTablePrefix + "Questions", AccountDbProperties.DbSchema);

                b.ConfigureByConvention();

                //Properties
                b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

                //Relations
                b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

                //Indexes
                b.HasIndex(q => q.CreationTime);
            });
            */
        }
    }
}
