
using WebApi.Entities;

namespace WebApi.Services.Interfaces
{
    public interface IProductModifyService <IdType, ProductType>
    {
        void Create(ProductType product, Account user);
        void Delete(IdType id);
        void Update(IdType id, ProductType product, Account user);
    }
}
