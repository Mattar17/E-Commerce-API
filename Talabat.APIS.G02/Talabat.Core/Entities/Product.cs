using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        #region Brand Relation
        public int ProductBrandId { get; set; }
        public ProductBrand ProductBrand { get; set; }

        #endregion

        #region Type Relation
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
        #endregion
    }
}
