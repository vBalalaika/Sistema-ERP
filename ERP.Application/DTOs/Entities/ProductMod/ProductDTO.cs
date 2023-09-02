using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.DTOs.Entities.ProductMod
{
    public class ProductDTO : Product
    {
        //public List<int> PieceTypeIds { get; set; }
        //public List<int> CutTypeIds { get; set; }
        //public List<int> PieceFormatIds { get; set; }
        //public List<int> SupplyVoltagesIds { get; set; }
        //public List<int> AccessoryProductsIds { get; set; }
        public string CodeAndDescription
        {
            get
            {
                return this.Code + "-" + this.Description;
            }
        }
        public int? CodeWithOutPrefix
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.Code))
                {
                    int codeInt = 0;
                    var code = this.Code.Substring(2);
                    if (int.TryParse(code, out codeInt))
                    {
                        return codeInt;
                    }
                    else
                        return null;


                }
                else
                    return null;

            }

        }
    }
}
