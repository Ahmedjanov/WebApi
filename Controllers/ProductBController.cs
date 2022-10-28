using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;
using WebApi.Models.Products;
using WebApi.Services;
using WebApi.Authorization;
using WebApi.Helpers;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ProductBController : BaseController
    {
        private readonly ProductBWithPaginationService _productService;
        public ProductBController(ProductBWithPaginationService productService)
        {
            _productService = productService;
        }

        [HttpPost("create")]
        public void Create(ProductBDTO product)
        {
            _productService.Create(product, Account);
        }

        [HttpDelete("delete")]
        public void Delete(int id)
        {
            _productService.Delete(id);
        }

        [HttpPut("edit")]
        public void Edit(int id, ProductBDTO productB)
        {
            _productService.Update(id, productB, Account);
        }

        [HttpGet("{id}")]
        public ProductBDTO GetProductA(int id)
        {
            return _productService.GetProduct(id); ;
        }

        [HttpGet]
        public PagedList<ProductBDTO> GetAll(PaginationParametrs paginationParametrs)
        {
            var products = _productService.GetAll(paginationParametrs);

            var metadata = new
            {
                products.TotalCount,
                products.PageSize,
                products.CurrentPage,
                products.TotalPages,
                products.HasNext,
                products.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return products;
        }
    }
}
