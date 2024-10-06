using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Order_Aggregate
{
    public class ProductItemOrdered
    {
        public int ProudctId { get; set; }
        public string ProudctName { get; set; }
        public string PictureUrl { get; set; }

        public ProductItemOrdered(int productId , string productName , string pictureUrl)
        {
            ProudctId = productId;
            ProudctName = productName;
            PictureUrl = pictureUrl;
        }

        public ProductItemOrdered()
        {
            
        }
    }
}