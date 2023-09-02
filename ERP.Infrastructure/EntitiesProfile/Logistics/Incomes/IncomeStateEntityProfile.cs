using ERP.Domain.Entities.Logistics.Incomes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.Logistics.Incomes
{
    public class IncomeStateEntityProfile : IEntityTypeConfiguration<IncomeState>
    {
        public void Configure(EntityTypeBuilder<IncomeState> builder)
        {
            builder.Property(ist => ist.ConcurrencyToken).IsConcurrencyToken();

            builder.Property(ist => ist.Description).HasMaxLength(50);
            builder.Property(ist => ist.Comments).HasMaxLength(200);
        }
    }
}
