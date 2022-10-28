using WebApi.Entities;

namespace WebApi.Models.Products
{
    public abstract class BaseProduct<IdType>
    {
        public IdType Id { get; set; }
        public int CreatedId { get; set; }
        public Account CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpdatedId { get; set; }
        public Account UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
