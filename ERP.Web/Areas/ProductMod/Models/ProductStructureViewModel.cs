using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ERP.Web.Areas.ProductMod.Models
{
    public class ProductStructureViewModel
    {
        public ProductViewModel Product { get; set; }
        public IList<StructureViewModel> Structures { get; set; } = new List<StructureViewModel>();
        public StructureViewModel SelectedStructure { get; set; }

        public string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder(capacity: normalizedString.Length);

            for (int i = 0; i < normalizedString.Length; i++)
            {
                char c = normalizedString[i];
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder
                .ToString()
                .Normalize(NormalizationForm.FormC);
        }

    }
}
