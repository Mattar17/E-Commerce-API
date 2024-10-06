using AutoMapper;
using Talabat.APIS.G02.DTOS;
using Talabat.Core.Entities;

namespace Talabat.APIS.G02.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
           if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{_configuration["AppBaseUrl"]}{source.PictureUrl}";
            
            return string.Empty ;
        }
    }
}
