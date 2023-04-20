using MassTransit;
using Shared.RabbitMQ.Events.Product;
using Shared.Repos.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RabbitMQ.Consumers.Category
{
    public class ProductCreateCategoryConsumer : IConsumer<ProductCreated>
    {
        private readonly ICategoryRepository _categoryRepository;
        public ProductCreateCategoryConsumer(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task Consume(ConsumeContext<ProductCreated> context)
        {
            var result = await _categoryRepository.CheckAllCategoriesExist(context.Message.CategoryIds);
            if (!result)
            {
                throw new Exception("Category Ids of new product is not valid. Product Id : " + context.Message.Id + "and Product Name : " + context.Message.Name);
            }
        }
    }
}
