using ERP.Domain.Common;
using ERP.Domain.Entities.Commons;
using ERP.Domain.Entities.Purchases.Providers;
using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities.ProductMod
{
    public class Product : IAuditableBaseEntity, IConcurrencyToken
    {
        #region Property
        public string Code { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public decimal? Existence { get; set; } = 0;
        public int? ExistenceUnitMeasureId { get; set; }
        public UnitMeasure ExistenceUnitMeasure { get; set; }

        public decimal? Minimum { get; set; } = 0;
        public int? MinimumUnitMeasureId { get; set; }
        public UnitMeasure MinimumUnitMeasure { get; set; }

        public decimal? QuantityToOrder { get; set; } = 0;
        public int? QuantityToOrderUnitMeasureId { get; set; }
        public UnitMeasure QuantityToOrderUnitMeasure { get; set; }

        public decimal? Cost { get; set; }
        public string Location { get; set; }

        public string Observation { get; set; }
        public int? ProfitID { get; set; }

        #region Stock

        /* FRAN: Campos para stock */
        public decimal StorageQuantity { get; set; } = 0;
        public int? StorageUnitMeasureId { get; set; }
        public UnitMeasure StorageUnitMeasure { get; set; }

        public decimal PhysicalSpaceWidth { get; set; } = 0;
        public decimal PhysicalSpaceLength { get; set; } = 0;
        public decimal PhysicalSpaceHeight { get; set; } = 0;

        public int Shelves_Rack { get; set; } = 0;
        public int Shelf { get; set; } = 0;
        public int Position { get; set; } = 0;

        public decimal? StockWidth { get; set; } = 0;
        public int? StockWidthUnitMeasureId { get; set; }
        public UnitMeasure StockWidthUnitMeasure { get; set; }

        public decimal? StockLength { get; set; } = 0;
        public int? StockLengthUnitMeasureId { get; set; }
        public UnitMeasure StockLengthUnitMeasure { get; set; }

        public decimal? StockHeight { get; set; } = 0;
        public int? StockHeightUnitMeasureId { get; set; }
        public UnitMeasure StockHeightUnitMeasure { get; set; }

        public decimal StockQuantity { get; set; } = 0;
        public int? StockQuantityUnitMeasureId { get; set; }
        public UnitMeasure StockQuantityUnitMeasure { get; set; }

        public decimal? StockWeight { get; set; } = 0;
        public int? StockWeightUnitMeasureId { get; set; }
        public UnitMeasure StockWeightUnitMeasure { get; set; }

        public string StockObservations { get; set; }

        /* VER SI SE BORRA */
        public int? StockUnitMeasureId { get; set; }
        public UnitMeasure? StockUnitMeasure { get; set; }

        #endregion

        public SubCategory SubCategory { get; set; }
        public int SubCategoryId { get; set; }

        public UnitMeasure? UnitMeasure { get; set; }
        public int? UnitMeasureId { get; set; }

        public IVACondition? IVAType { get; set; }
        public int? IVATypeId { get; set; }

        public int? OperationId { get; set; }
        public Operation? Operation { get; set; }

        public int? ProductFeatureId { get; set; }
        public ProductFeature? ProductFeature { get; set; }

        public int? LocationStationId { get; set; }
        public Station? LocationStation { get; set; }

        public string IsOnColppy { get; set; }

        public int ConcurrencyToken { get; set; }
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        #endregion

        public ICollection<ProductCutType> ProductCutTypes { get; set; } = new List<ProductCutType>();
        public ICollection<AccessoryProduct> AccessoryProducts { get; set; } = new List<AccessoryProduct>();
        public ICollection<AccessoryProduct> AccessoryProductsOf { get; set; } = new List<AccessoryProduct>();
        public ICollection<ProductPieceFormat> ProductPieceFormats { get; set; } = new List<ProductPieceFormat>();
        public ICollection<ProductPieceType> ProductPieceTypes { get; set; } = new List<ProductPieceType>();
        public ICollection<ProductSupplyVoltage> ProductSupplyVoltages { get; set; } = new List<ProductSupplyVoltage>();
        public ICollection<RelProductStation> RelProductStations { get; set; } = new List<RelProductStation>();
        public ICollection<Structure> Structures { get; set; } = new List<Structure>();
        public ICollection<Archive> Archives { get; set; } = new List<Archive>();

        public ICollection<RelProviderProduct> RelProviderProducts { get; set; } = new List<RelProviderProduct>();

    }
}