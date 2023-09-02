using ERP.Domain.Common;
using System;

namespace ERP.Domain.Entities.Clients
{
    public class Reminder : IAuditableBaseEntity, IConcurrencyToken
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public DateTime ContactDate { get; set; }
        public bool ReminderCheck { get; set; }
        public DateTime? ReminderDate { get; set; }
        public string Comment { get; set; }

        public int ConcurrencyToken { get; set; }
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}
