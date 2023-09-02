using ERP.Domain.Entities.ProductMod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Specification.ProductMod.StructureSpecifications
{
    public class StructureById : BaseSpecification<Structure>
    {
        public StructureById(int id)
            : base(x => x.Id == id)
        {

        }
    }
}
