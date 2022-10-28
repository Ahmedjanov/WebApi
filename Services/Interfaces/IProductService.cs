
using WebApi.Entities;

namespace WebApi.Services.Interfaces
{
    public interface IProductService <IdType, ProductType>
    {
        void Create(ProductType product, Account user);
        void Delete(IdType id);
        void Update(IdType id, ProductType product, Account user);
        ProductType GetProduct(IdType id);
        IEnumerable<ProductType> GetAll();
    }
}
