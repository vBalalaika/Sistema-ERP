using ERP.Domain.Entities.Logistics.Incomes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.Logistics.Incomes
{
    public class IncomeHeaderEntityProfile : IEntityTypeConfiguration<IncomeHeader>
    {
        public void Configure(EntityTypeBuilder<IncomeHeader> builder)
        {
            builder.Property(ih => ih.ConcurrencyToken).IsConcurrencyToken();

            builder.Property(ih => ih.ProviderId).IsRequired();
            builder.Property(ih => ih.TransportProviderId).IsRequired();

            builder.Property(ih => ih.DeliveryNoteNumber).HasMaxLength(50);
            builder.Property(ih => ih.InvoiceNumber).HasMaxLength(50);
            builder.Property(ih => ih.Comments).HasMaxLength(255);
        }
    }
}
