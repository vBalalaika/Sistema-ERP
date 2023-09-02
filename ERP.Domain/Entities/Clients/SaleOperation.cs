using ERP.Domain.Common;
using ERP.Domain.Entities.Sales;
using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities.Clients
{
    public class SaleOperation : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }
        public string Operation { get; set; }
        public string State { get; set; }
        public string Comments { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndingDate { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int OperationNumber { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public ICollection<Communication> Communications { get; set; } = new List<Communication>();

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }

    }
}
