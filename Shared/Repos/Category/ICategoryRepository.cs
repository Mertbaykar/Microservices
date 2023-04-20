using Shared.Entities;
using Shared.RabbitMQ.Events;
using Shared.RabbitMQ.Events.Category;

namespace Shared.Repos.Category
{
    public interface ICategoryRepository
    {
        public Task<Entities.Category> CreateCategory(CategoryCreated category);
        public Task<Entities.Category> CreateCategory(Entities.Category category);
        public Task<bool> CheckAllCategoriesExist(List<Guid> categoryIds);
    }
}
