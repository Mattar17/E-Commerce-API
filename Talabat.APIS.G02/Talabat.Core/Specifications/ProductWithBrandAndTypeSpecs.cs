using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecs : BaseSpecification<Product>
    {
        public ProductWithBrandAndTypeSpecs(ProductSpecsParams Params) : base( P=>

                (!Params.BrandId.HasValue||P.ProductBrandId==Params.BrandId)
                &&
                (!Params.TypeId.HasValue || P.ProductTypeId == Params.TypeId)
            )
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);

            if (!string.IsNullOrEmpty(Params.Sort))
            {
                switch (Params.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(P => P.Price);
                        break;

                    case "PriceDesc":
                        AddOrderByDescending(P => P.Price);
                        break;

                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }

            ApplyPagination(Params.PageSize, Params.PageSize*(Params.PageIndex-1));
        }

        public ProductWithBrandAndTypeSpecs(int id) : base(p=>p.Id == id)
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }
    }
}
