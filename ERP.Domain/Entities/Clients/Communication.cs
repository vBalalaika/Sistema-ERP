using ERP.Domain.Common;
using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities.Clients
{
    public class Communication : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
        public DateTime ComunicationDate { get; set; }
        public string ContactSource { get; set; }
        public string Comments { get; set; }
        public bool Incoming { get; set; }
        public bool Outgoing { get; set; }
        public bool ConsultedProduct { get; set; }
        public bool AnotherReason { get; set; }
        public bool PostSale { get; set; }
        public int? SaleOperationId { get; set; }
        public SaleOperation SaleOperation { get; set; }
        public ICollection<ConsultedProduct> ConsultedProducts { get; set; } = new List<ConsultedProduct>();
        public int? CommunicationNumber { get; set; }
        public string CommunicationName { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }

    }
}
