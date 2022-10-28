
using WebApi.Helpers;

namespace WebApi.Services.Interfaces
{
    public interface IProductReadWithPaginationService<IdType, ProductType>
    {
        ProductType GetProduct(IdType id);
        PagedList<ProductType> GetAll(PaginationParametrs paginationParametrs);
    }
}
