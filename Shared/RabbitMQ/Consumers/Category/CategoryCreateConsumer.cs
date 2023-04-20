using MassTransit;
using Shared.RabbitMQ.Events.Category;
using Shared.RabbitMQ.Events.Product;
using Shared.Repos.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RabbitMQ.Consumers.Category
{
    public class CategoryCreateConsumer : IConsumer<CategoryCreated>
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryCreateConsumer(ICategoryRepository productRepository)
        {
            _categoryRepository = productRepository;
        }
        public async Task Consume(ConsumeContext<CategoryCreated> context)
        {
            var product = await _categoryRepository.CreateCategory(context.Message);
        }
    }
}
