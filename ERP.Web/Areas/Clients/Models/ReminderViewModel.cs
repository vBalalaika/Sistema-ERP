using ERP.Domain.Entities.Clients;
using System;

namespace ERP.Web.Areas.Clients.Models
{
    public class ReminderViewModel
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }

        public DateTime ContactDate { get; set; }

        public bool ReminderCheck { get; set; } = true;

        public DateTime? ReminderDate { get; set; }

        public string Comment { get; set; }

        public string CountryTime { get; set; }

        public int Id { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
