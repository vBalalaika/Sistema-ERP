using ERP.Domain.Entities.Production;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.Production
{
    public class WorkActivityEntityProfile : IEntityTypeConfiguration<WorkActivity>
    {
        public void Configure(EntityTypeBuilder<WorkActivity> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();
        }
    }
}
