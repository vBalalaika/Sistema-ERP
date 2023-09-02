using ERP.Domain.Entities.Purchases.QuoteRequests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.Purchases.QuoteRequests
{
    public class QuoteRequestHeaderEntityProfile : IEntityTypeConfiguration<QuoteRequestHeader>
    {
        public void Configure(EntityTypeBuilder<QuoteRequestHeader> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();

            builder
                .HasMany(x => x.QuoteRequestDetails)
                .WithOne(x => x.QuoteRequestHeader)
                .HasForeignKey(x => x.QuoteRequestHeaderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
