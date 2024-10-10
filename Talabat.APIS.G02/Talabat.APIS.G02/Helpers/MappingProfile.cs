using AutoMapper;
using Talabat.APIS.G02.DTOS;
using Talabat.Core.Entities;
using Talabat.Core.Order_Aggregate;

namespace Talabat.APIS.G02.Helpers
{
    public class MappingProfile : Profile {

        public MappingProfile() {


            CreateMap<Product, ProductToReturnDto>()
                .ForMember(m => m.ProductBrand, s => s.MapFrom(m => m.ProductBrand.Name))
                .ForMember(m => m.ProductType, s => s.MapFrom(m => m.ProductType.Name))
                .ForMember(m => m.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());

            CreateMap<Talabat.Core.Entities.Address,AddressDto>().ReverseMap();
            CreateMap<Talabat.Core.Order_Aggregate.Address,AddressDto>().ReverseMap();

            CreateMap<Orders , OrderToReturnDTO>()
                .ForMember(delivery => delivery.DeliveryMethod , order => order.MapFrom(order => order.DeliveryMethod.ShortName))
                .ForMember(delivery => delivery.DeliveryMethodCost , order => order.MapFrom(order => order.DeliveryMethod.Cost));



        }

    }
}
