using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DTOs;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Products;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class ProductBWithPaginationService : IProductModifyService<int, ProductBDTO>, IProductReadWithPaginationService<int, ProductBDTO>
    {
        private readonly DataContext _context;

        public ProductBWithPaginationService(DataContext context)
        {
            _context = context;
        }

        public void Create(ProductBDTO productA, Account user)
        {
            try
            {
                var product = new ProductB()
                {
                    Name = productA.ProductName,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = user,
                };

                _context.ProductBs.Add(product);
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
                var product = _context.ProductBs.Find(id);
                if (product == null)
                {
                    throw new KeyNotFoundException($"По Id = {id} ПродуктB не найден");
                }

                _context.ProductBs.Remove(product);
                _context.SaveChanges();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public PagedList<ProductBDTO> GetAll(PaginationParametrs paginationParametrs)
        {
            var products = _context.ProductBs;
            var productsDTO = new List<ProductBDTO>();

            foreach(var product in products)
            {
                productsDTO.Add(new ProductBDTO()
                {
                    ProductId = product.Id,
                    ProductName = product.Name
                });
            }

            return PagedList<ProductBDTO>.ToPagedList(productsDTO, paginationParametrs.PageNumber, paginationParametrs.PageSize);
        }

        public ProductBDTO GetProduct(int id)
        {
            var product = _context.ProductBs.Find(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"По Id = {id} ПродуктB не найден");
            }

            var productDTO = new ProductBDTO()
            {
                ProductId = product.Id,
                ProductName = product.Name,
            };

            return productDTO;
        }

        public void Update(int id, ProductBDTO productB, Account user)
        {
            try
            {
                if (productB.ProductId != id)
                {
                    throw new AppException($"Указанный Id - {id} не совпадает с Id продукта - {productB.ProductId}");
                }
                
                var productInDataBase = _context.ProductBs.AsNoTracking().FirstOrDefault(a => a.Id == id);

                if (productB == null)
                {
                    throw new KeyNotFoundException($"По Id = {id} ПродуктB не найден");
                }
                
                var product = new ProductB()
                {
                    Id = productB.ProductId,
                    Name = productB.ProductName,
                    CreatedAt = productInDataBase.CreatedAt,
                    CreatedBy = productInDataBase.CreatedBy,
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = user,
                };

                _context.ProductBs.Update(product);
                _context.SaveChanges();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
