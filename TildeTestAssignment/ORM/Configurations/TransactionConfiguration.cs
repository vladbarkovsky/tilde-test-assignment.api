using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TildeTestAssignment.ORM.Entities;

namespace TildeTestAssignment.ORM.Configurations
{
    public class TransactionConfiguration : BaseEntityConfiguration<Transaction>
    {
        public override void Configure(EntityTypeBuilder<Transaction> builder)
        {
            base.Configure(builder);
        }
    }
}