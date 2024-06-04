using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TildeTestAssignment.ORM.Entities;

namespace TildeTestAssignment.ORM.Configurations
{
    public class PersonConfiguration : BaseEntityConfiguration<Person>
    {
        public override void Configure(EntityTypeBuilder<Person> builder)
        {
            base.Configure(builder);

            builder
                .HasMany(x => x.Loans)
                .WithOne(x => x.Debtor)
                .HasForeignKey(x => x.DebtorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasMany(x => x.Refunds)
                .WithOne(x => x.Creditor)
                .HasForeignKey(x => x.CreditorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}