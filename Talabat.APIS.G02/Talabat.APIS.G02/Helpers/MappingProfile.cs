using AutoMapper;
using Talabat.APIS.G02.DTOS;
using Talabat.Core.Entities;

namespace Talabat.APIS.G02.Helpers
{
    public class MappingProfile : Profile {

        public MappingProfile() {


            CreateMap<Product, ProductToReturnDto>()
                .ForMember(m => m.ProductBrand, s => s.MapFrom(m => m.ProductBrand.Name))
                .ForMember(m => m.ProductType, s => s.MapFrom(m => m.ProductType.Name))
                .ForMember(m => m.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());

            CreateMap<Address,AddressDto>().ReverseMap();
            CreateMap<Talabat.Core.Order_Aggregate.Address,AddressDto>().ReverseMap();


        }

    }
}
