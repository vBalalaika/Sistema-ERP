using ERP.Domain.Common;
using System;

namespace ERP.Domain.Entities.Commons
{
    public class UnitMeasure : IAuditableBaseEntity, IConcurrencyToken
    {
        public int? Code { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        // UnitType 1 = UNIDAD, 2 = Unidades de longitud, 3 = Unidades de peso, 4 = Unidades de capacidad, 5 = HOJA, 6 = BOBINA, 7 = ROLLO, 8 = BARRA, 9 = CAJA
        public int UnitType { get; set; }
        public string Abbreviation { get; set; }

        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
