using ERP.Domain.Entities.ProductMod;
using System;

namespace ERP.Web.Areas.ProductMod.Models
{
    public class StructureItemViewModel
    {

        public string Order { get; set; }
        public string Level { get; set; }
        public int Quantity { get; set; }
        public int SpacingPixels
        {
            get
            {
                if (this.Level != null)
                {
                    return (this.Level.Split('.').Length - 1) * 8;
                }
                else
                {
                    return 0;
                }
            }
        }
        public string RoadMap
        {
            get
            {
                var roadmap = "";
                if (this.SonProduct != null)
                {
                    bool first = true;
                    foreach (var item in this.SonProduct.RelProductStations)
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
                }
                else if (this.SonStructure != null)
                {
                    if (this.SonStructure.Product.RelProductStations != null)
                    {
                        bool first = true;
                        foreach (var item in this.SonStructure.Product.RelProductStations)
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
                    }
                }
                return roadmap;
            }
        }
        public StructureVersionViewModel Version { get; set; }
        public int VersionId { get; set; }
        public ProductViewModel SonProduct { get; set; }
        public int? SonProductId { get; set; }
        public StructureViewModel SonStructure { get; set; }
        public int? SonStructureId { get; set; }
        public Structure Structure { get; set; }
        public int StructureId { get; set; }
        public int Id { get; set; }
        public int ConcurrencyToken { get; set; }
    }
}
