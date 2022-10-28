using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DTOs;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Products;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class ProductAService : IProductModifyService<int, ProductADTO>, IProductReadService<int, ProductADTO>
    {
        private readonly DataContext _context;

        public ProductAService(DataContext context)
        {
            _context = context;
        }

        public void Create(ProductADTO productA, Account user)
        {
            try
            {
                var product = new ProductA()
                {
                    Name = productA.ProductName,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = user,
                };

                _context.ProductAs.Add(product);
                _context.SaveChanges();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var product = _context.ProductAs.Find(id);
                if (product == null)
                {
                    throw new KeyNotFoundException($"По Id = {id} ПродуктА не найден");
                }

                _context.ProductAs.Remove(product);
                _context.SaveChanges();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public IEnumerable<ProductADTO> GetAll()
        {
            var products = _context.ProductAs;
            var productsDTO = new List<ProductADTO>();

            foreach(var product in products)
            {
                productsDTO.Add(new ProductADTO()
                {
                    ProductId = product.Id,
                    ProductName = product.Name
                });
            }

            return productsDTO;
        }

        public ProductADTO GetProduct(int id)
        {
            var product = _context.ProductAs.Find(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"По Id = {id} ПродуктА не найден");
            }

            var productDTO = new ProductADTO()
            {
                ProductId = product.Id,
                ProductName = product.Name,
            };

            return productDTO;
        }

        public void Update(int id, ProductADTO productA, Account user)
        {
            try
            {
                if (productA.ProductId != id)
                {
                    throw new AppException($"Указанный Id - {id} не совпадает с Id продукта - {productA.ProductId}");
                }
                
                var productInDataBase = _context.ProductAs.AsNoTracking().FirstOrDefault(a => a.Id == id);

                if (productA == null)
                {
                    throw new KeyNotFoundException($"По Id = {id} ПродуктА не найден");
                }
                
                var product = new ProductA()
                {
                    Id = productA.ProductId,
                    Name = productA.ProductName,
                    CreatedAt = productInDataBase.CreatedAt,
                    CreatedBy = productInDataBase.CreatedBy,
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = user,
                };

                _context.ProductAs.Update(product);
                _context.SaveChanges();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
