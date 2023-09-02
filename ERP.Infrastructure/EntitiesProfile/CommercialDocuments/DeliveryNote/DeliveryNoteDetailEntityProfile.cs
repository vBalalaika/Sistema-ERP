using ERP.Domain.Entities.CommercialDocuments.DeliveryNote;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.CommercialDocuments.DeliveryNote
{
    public class DeliveryNoteDetailEntityProfile : IEntityTypeConfiguration<DeliveryNoteDetail>
    {
        public void Configure(EntityTypeBuilder<DeliveryNoteDetail> builder)
        {
            builder.Property(dnd => dnd.ConcurrencyToken).IsConcurrencyToken();
        }
    }
}
