namespace WebApi.Services.Interfaces
{
    public interface IProductReadService <IdType, ProductType>
    {
        ProductType GetProduct(IdType id);
        IEnumerable<ProductType> GetAll();
    }
}
