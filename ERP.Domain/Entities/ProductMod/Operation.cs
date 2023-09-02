﻿using ERP.Domain.Common;
using System;

namespace ERP.Domain.Entities.ProductMod
{
    public class Operation : IAuditableBaseEntity, IConcurrencyToken
    {
        public string Description { get; set; }
        public Boolean IsActive { get; set; }

        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }

    }
}
