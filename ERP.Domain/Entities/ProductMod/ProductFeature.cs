using ERP.Domain.Common;
using System;

namespace ERP.Domain.Entities.ProductMod
{
    public class ProductFeature : IAuditableBaseEntity, IConcurrencyToken
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public int ConcurrencyToken { get; set; }

        //public int IdProduct { get; set; }
        //public Product Product { get; set; }
        public string RawMaterialCode { get; set; }
        public string RawMaterialDescription { get; set; }
        public bool Standar { get; set; }
        public decimal? Appreciation { get; set; }
        public int? PieceXCut { get; set; }
        public int? ColorsXPass { get; set; }
        public string PressingCapacity { get; set; }
        public string Reduction { get; set; }
        public string DimensionPlates { get; set; }
        public string OpeningMax { get; set; }
        public string HeightPiece { get; set; }
        public string WidthMaxPiece { get; set; }
        public string WidthMinPiece { get; set; }
        public string LenghtMaxPiece { get; set; }
        public string LenghtMinPiece { get; set; }
        public string Diameter { get; set; }
        public decimal? Density { get; set; }
        public string Bagel { get; set; }
        public decimal? PowerHP { get; set; }
        public string Legs { get; set; }
        public string ProductionSpeed { get; set; }
        public string BagQuantityMax { get; set; }
        public string RollQuantityMax { get; set; }
        public string PassageWidthMax { get; set; }
        public string PrintWidthMax { get; set; }
        public string PrintHeightMax { get; set; }
        public string PressedCenter { get; set; }
        public string PressedSpeed { get; set; }
        public string OpeningSpeed { get; set; }
        public string LineFrequency { get; set; }
        public string MachineWeight { get; set; }
        public string MachineLenght { get; set; }
        public string MachineWidth { get; set; }
        public string MachineHeight { get; set; }
        public string PackagingDimension { get; set; }
        public string WorkPressure { get; set; }
        public bool InHouseManufacturing { get; set; }
        public bool Bought { get; set; }
        public bool Component { get; set; }
        public bool Finished { get; set; }
        public string ComponentHighPiece { get; set; }
        public string ComponentWidhtPiece { get; set; }
        public string ComponentLongPiece { get; set; }
        public bool StoredStock { get; set; }
        public string ManufacturingTime { get; set; }


    }
}
