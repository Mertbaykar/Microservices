using MassTransit;
using Shared.RabbitMQ.Events.Product;
using Shared.Repos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RabbitMQ.Consumers.Product
{
    public class ProductCreateConsumer : IConsumer<ProductCreated>
    {
        private readonly IProductRepository _productRepository;
        public ProductCreateConsumer(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task Consume(ConsumeContext<ProductCreated> context)
        {
            var product = await _productRepository.CreateProduct(context.Message);
        }
    }
}
