using ERP.Domain.Entities.Production;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.Production
{
    public class GroupedWorkActionEntityProfile : IEntityTypeConfiguration<GroupedWorkAction>
    {

        public void Configure(EntityTypeBuilder<GroupedWorkAction> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();
        }
    }
}
