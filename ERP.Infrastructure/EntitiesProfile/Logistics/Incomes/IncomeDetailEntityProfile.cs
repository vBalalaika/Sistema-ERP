using ERP.Domain.Entities.Logistics.Incomes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.Logistics.Incomes
{
    public class IncomeDetailEntityProfile : IEntityTypeConfiguration<IncomeDetail>
    {
        public void Configure(EntityTypeBuilder<IncomeDetail> builder)
        {
            builder.Property(id => id.ConcurrencyToken).IsConcurrencyToken();

            builder.Property(id => id.IncomeProductId).IsRequired();
            builder.Property(id => id.IncomeStateId).IsRequired();
            builder.Property(id => id.UnitId).IsRequired();

            builder.Property(id => id.BatchNumber).HasMaxLength(150);
            builder.Property(id => id.ProductNumber).HasMaxLength(20);

            builder.Property(id => id.Reception).HasMaxLength(250);
            builder.Property(id => id.NextStation).HasMaxLength(250);
        }
    }
}
