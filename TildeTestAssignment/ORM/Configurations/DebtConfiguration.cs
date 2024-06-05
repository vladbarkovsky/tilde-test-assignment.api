using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TildeTestAssignment.ORM.Entities;

namespace TildeTestAssignment.ORM.Configurations
{
    public class DebtConfiguration : BaseEntityConfiguration<Debt>
    {
        public override void Configure(EntityTypeBuilder<Debt> builder)
        {
            base.Configure(builder);
        }
    }
}