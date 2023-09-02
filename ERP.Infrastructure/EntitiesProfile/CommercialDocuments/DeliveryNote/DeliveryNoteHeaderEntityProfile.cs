using ERP.Domain.Entities.CommercialDocuments.DeliveryNote;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.CommercialDocuments.DeliveryNote
{
    public class DeliveryNoteHeaderEntityProfile : IEntityTypeConfiguration<DeliveryNoteHeader>
    {
        public void Configure(EntityTypeBuilder<DeliveryNoteHeader> builder)
        {
            builder.Property(dnh => dnh.ConcurrencyToken).IsConcurrencyToken();
            builder.Property(dnh => dnh.Number).HasMaxLength(250);
            builder.Property(dnh => dnh.Comments).HasMaxLength(500);
        }
    }
}
