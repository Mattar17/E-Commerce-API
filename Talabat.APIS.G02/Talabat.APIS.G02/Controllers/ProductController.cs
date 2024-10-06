using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Talabat.APIS.G02.DTOS;
using Talabat.APIS.G02.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repository;
using Talabat.Core.Specifications;

namespace Talabat.APIS.G02.Controllers
{

    public class ProductController : APIBaseController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<ProductBrand> _brandRep;
        private readonly IGenericRepository<ProductType> _typeRepo;

        public ProductController(IGenericRepository<Product> ProductRepo, IMapper mapper, IGenericRepository<ProductBrand> BrandRep, IGenericRepository<ProductType> TypeRepo)
        {
            _productRepo = ProductRepo;
            _mapper = mapper;
            _brandRep = BrandRep;
            _typeRepo = TypeRepo;
        }

        #region GetAllProducts
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetAllProducts([FromQuery]ProductSpecsParams Params)
        {
            var Specs = new ProductWithBrandAndTypeSpecs(Params);
            var Products = await _productRepo.GetAllWithSpecs(Specs);
            var MappedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(Products);
            return Ok(MappedProducts);
        }
        #endregion

        #region GetProductById
        [HttpGet("{id}")]

        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
        {
            var Specs = new ProductWithBrandAndTypeSpecs(id);
            var Product = await _productRepo.GetByIdWithSpecs(Specs);
            if (Product == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var MappedProduct = _mapper.Map<Product, ProductToReturnDto>(Product);
            return Ok(MappedProduct);
        }
        #endregion

        #region GetBrands

        [HttpGet("Brands")]

        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetBrands()
        {
            var Brands = await _brandRep.GetAllAsync();
            return Ok(Brands);

        }


        #endregion

        #region GetTypes

        [HttpGet("Types")]

        public async Task<ActionResult<IEnumerable<ProductType>>> GetTypes()
        {
            var Types = await _typeRepo.GetAllAsync();
            return Ok(Types);

        }


        #endregion
    }
}
