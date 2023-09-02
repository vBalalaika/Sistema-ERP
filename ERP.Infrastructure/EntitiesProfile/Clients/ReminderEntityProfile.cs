using ERP.Domain.Entities.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.Clients
{
    public class ReminderEntityProfile : IEntityTypeConfiguration<Reminder>
    {
        public void Configure(EntityTypeBuilder<Reminder> builder)
        {
            // Reglas para cada campo en particular
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();
        }
    }
}
