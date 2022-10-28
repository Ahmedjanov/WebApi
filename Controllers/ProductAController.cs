using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;
using WebApi.Models.Products;
using WebApi.Services;
using WebApi.Authorization;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ProductAController : BaseController
    {
        private readonly ProductAService _productService;
        public ProductAController(ProductAService productService)
        {
            _productService = productService;
        }

        [HttpPost("create")]
        public void Create(ProductADTO product)
        {
            _productService.Create(product, Account);
        }

        [HttpDelete("delete")]
        public void Delete(int id)
        {
            _productService.Delete(id);
        }

        [HttpPut("edit")]
        public void Edit(int id, ProductADTO productA)
        {
            _productService.Update(id, productA, Account);
        }

        [HttpGet("{id}")]
        public ProductADTO GetProductA(int id)
        {
            return _productService.GetProduct(id); ;
        }

        [HttpGet]
        public IEnumerable<ProductADTO> GetAll()
        {
            return _productService.GetAll(); ;
        }
    }
}
