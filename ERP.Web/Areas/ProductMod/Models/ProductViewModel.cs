using ERP.Web.Areas.Commons.Models;
using ERP.Web.Areas.Purchases.Models.Providers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP.Web.Areas.ProductMod.Models
{
    public class ProductViewModel
    {
        #region SimpleProperties
        public int Id { get; set; }
        public int ConcurrencyToken { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string Version { get; set; }
        public string Observation { get; set; }

        public int QuantityOrderDetail { get; set; }
        public string ProductNumber { get; set; }
        public string StructureDescription { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? Existence { get; set; } = 0;
        public int? ExistenceUnitMeasureId { get; set; }
        public UnitMeasureViewModel ExistenceUnitMeasure { get; set; }
        public SelectList ExistenceUnitMeasures { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? Minimum { get; set; } = 0;
        public int? MinimumUnitMeasureId { get; set; }
        public UnitMeasureViewModel MinimumUnitMeasure { get; set; }
        public SelectList MinimumUnitMeasures { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? QuantityToOrder { get; set; } = 0;
        public int? QuantityToOrderUnitMeasureId { get; set; }
        public UnitMeasureViewModel QuantityToOrderUnitMeasure { get; set; }
        public SelectList QuantityToOrderUnitMeasures { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? Cost { get; set; }

        public string Location { get; set; }
        public string Roadmap
        {
            get
            {
                var roadmap = "";
                bool first = true;
                foreach (var item in this.RelProductStations)
                {
                    if (first)
                    {
                        roadmap = String.Join("", new String[] { roadmap, item.Station.Abbreviation });
                        first = false;
                    }
                    else
                    {
                        roadmap = String.Join("->", new String[] { roadmap, item.Station.Abbreviation });
                    }
                }
                return roadmap;
            }
        }
        public string PackingVolume { get; set; }
        public string WeightEspecification { get; set; }
        public string ProductionSpeed { get; set; }
        public string WorkPressure { get; set; }

        public int? CategoryIdFilter { get; set; }
        public string CategoryDescriptionFilter { get; set; }
        public bool? ProducedBySmak { get; set; }

        #endregion

        //Para combo de estructuras
        public string CodeAndDescription
        {
            get
            {
                return this.Code + " - " + this.Description;
            }
        }
        //ProductFeature
        public int ProductFeatureId { get; set; }
        public ProductFeatureViewModel ProductFeature { get; set; }

        //LocationStationStock
        public int? LocationStationId { get; set; }
        public StationViewModel LocationStation { get; set; }

        //PieceTypes
        public SelectList PieceTypesList { get; set; }
        public IList<ProductPieceTypeViewModel> ProductPieceTypes { get; set; } = new List<ProductPieceTypeViewModel>();

        //CutTypes
        public IList<ProductCutTypeViewModel> ProductCutTypes { get; set; } = new List<ProductCutTypeViewModel>();
        public SelectList CutTypesList { get; set; }

        //PieceFormats
        public IList<ProductPieceFormatViewModel> ProductPieceFormats { get; set; } = new List<ProductPieceFormatViewModel>();
        public SelectList PieceFormatsList { get; set; }

        //SupplyVoltages
        public IList<ProductSupplyVoltageViewModel> ProductSupplyVoltages { get; set; } = new List<ProductSupplyVoltageViewModel>();
        public SelectList SupplyVoltagesList { get; set; }

        //Station y RelProductStation
        public IList<StationViewModel> StationsList { get; set; }
        public IList<RelProductStationViewModel> RelProductStations { get; set; } = new List<RelProductStationViewModel>();

        //RelProviderProduct
        public IList<RelProviderProductViewModel> RelProviderProducts { get; set; } = new List<RelProviderProductViewModel>();

        //AccesoryProducts
        public IList<AccessoryProductViewModel> AccessoryProducts { get; set; } = new List<AccessoryProductViewModel>();
        public IList<ProductViewModel> AccessoryProductsList { get; set; }

        //Archive
        public IList<ArchiveViewModel> Archives { get; set; } = new List<ArchiveViewModel>();
        public IList<ArchiveTypeViewModel> ArchiveTypesList { get; set; } = new List<ArchiveTypeViewModel>();

        //Unit Measure
        public IList<UnitMeasureViewModel> UnitsMeasureList { get; set; } = new List<UnitMeasureViewModel>();
        public SelectList UnitsMeasure { get; set; }
        public UnitMeasureViewModel? UnitMeasure { get; set; }
        public int? UnitMeasureId { get; set; }

        //Category
        public SelectList Categories { get; set; }

        //SubCategory
        public SubCategoryViewModel SubCategory { get; set; }
        public int SubCategoryId { get; set; }
        public SelectList SubCategories { get; set; }

        //Structures
        public IList<StructureViewModel> Structures { get; set; } = new List<StructureViewModel>();

        //Raw Material
        public SelectList RawMaterials { get; set; }

        //IVACondition
        public SelectList IVATypes { get; set; }
        public IVAConditionViewModel? IVAType { get; set; }
        public int? IVATypeId { get; set; }

        //Operation
        public int? OperationId { get; set; }
        public OperationViewModel? Operation { get; set; }
        public SelectList Operations { get; set; }

        #region Stock

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal StorageQuantity { get; set; } = 0;
        public int? StorageUnitMeasureId { get; set; }
        public UnitMeasureViewModel StorageUnitMeasure { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal PhysicalSpaceWidth { get; set; } = 0;

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal PhysicalSpaceLength { get; set; } = 0;

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal PhysicalSpaceHeight { get; set; } = 0;

        public int Shelves_Rack { get; set; } = 0;
        public int Shelf { get; set; } = 0;
        public int Position { get; set; } = 0;

        /* FRAN: Renombre los campos Width, Length y Height que no se utilizaban*/
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? StockWidth { get; set; } = 0;
        public int? StockWidthUnitMeasureId { get; set; }
        public UnitMeasureViewModel StockWidthUnitMeasure { get; set; }
        public SelectList StockWidthUnitMeasures { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? StockLength { get; set; } = 0;
        public int? StockLengthUnitMeasureId { get; set; }
        public UnitMeasureViewModel StockLengthUnitMeasure { get; set; }
        public SelectList StockLengthUnitMeasures { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? StockHeight { get; set; } = 0;
        public int? StockHeightUnitMeasureId { get; set; }
        public UnitMeasureViewModel StockHeightUnitMeasure { get; set; }
        public SelectList StockHeightUnitMeasures { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal StockQuantity { get; set; } = 0;
        public int? StockQuantityUnitMeasureId { get; set; }
        public UnitMeasureViewModel StockQuantityUnitMeasure { get; set; }
        public SelectList StockQuantityUnitMeasures { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? StockWeight { get; set; } = 0;
        public int? StockWeightUnitMeasureId { get; set; }
        public UnitMeasureViewModel StockWeightUnitMeasure { get; set; }
        public SelectList StockWeightUnitMeasures { get; set; }

        public string StockObservations { get; set; }

        /* FB: 14/9/22 - No se utiliza */
        public int? StockUnitMeasureId { get; set; }
        public UnitMeasureViewModel? StockUnitMeasure { get; set; }
        public SelectList StockUnitMeasures { get; set; }

        #endregion

    }
}