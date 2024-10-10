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
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        #region GetAllProducts
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetAllProducts([FromQuery]ProductSpecsParams Params)
        {
            var Specs = new ProductWithBrandAndTypeSpecs(Params);
            var Products = await _unitOfWork.Repository<Product>().GetAllWithSpecs(Specs);
            var MappedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(Products);
            return Ok(MappedProducts);
        }
        #endregion

        #region GetProductById
        [HttpGet("{id}")]

        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
        {
            var Specs = new ProductWithBrandAndTypeSpecs(id);
            var Product = await _unitOfWork.Repository<Product>().GetByIdWithSpecs(Specs);
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
            var Brands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();
            return Ok(Brands);

        }


        #endregion

        #region GetTypes

        [HttpGet("Types")]

        public async Task<ActionResult<IEnumerable<ProductType>>> GetTypes()
        {
            var Types = await _unitOfWork.Repository<ProductType>().GetAllAsync();
            return Ok(Types);

        }


        #endregion
    }
}
