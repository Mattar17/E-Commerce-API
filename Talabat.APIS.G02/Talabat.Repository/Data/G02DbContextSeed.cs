using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data
{
    public class G02DbContextSeed
    {
        public static async Task SeedAsync(G02DbContext dbContext)
        {
            

            #region Product Brand

            if (!dbContext.ProductBrands.Any())
            {

                var BrandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

                if (Brands?.Count() > 0)
                {
                    foreach (var brand in Brands)
                    {
                        await dbContext.Set<ProductBrand>().AddAsync(brand);
                    }

                    await dbContext.SaveChangesAsync();
                }

            }

            #endregion

            #region Product Type

            if (!dbContext.ProductTypes.Any())
            {

                var TypesData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);

                if (Types?.Count() > 0)
                {
                    foreach (var type in Types)
                    {
                        await dbContext.Set<ProductType>().AddAsync(type);
                    }

                    await dbContext.SaveChangesAsync();
                }

            }

            #endregion

            #region Products
            if (!dbContext.Products.Any())
            {

                var ProductsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                if (Products?.Count() > 0)
                {
                    foreach (var product in Products)
                    {
                        await dbContext.Set<Product>().AddAsync(product);
                    }

                    await dbContext.SaveChangesAsync();
                }

            }
            #endregion
        }
    }
}
