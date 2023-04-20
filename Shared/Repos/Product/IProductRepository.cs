using Shared.Entities;
using Shared.RabbitMQ.Events.Product;

namespace Shared.Repos.Product
{
    public interface IProductRepository
    {
        public Task<Entities.Product> CreateProduct(ProductCreated product);
        public Task<Entities.Product> CreateProduct(Entities.Product product);
    }
}
