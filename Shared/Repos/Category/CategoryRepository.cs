using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shared.DbContexts;
using Shared.Entities;
using Shared.RabbitMQ.Events;
using Shared.RabbitMQ.Events.Category;

namespace Shared.Repos.Category
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryContext _categoryContext;
        private readonly IMapper _mapper;
        public CategoryRepository(CategoryContext productContext, IMapper mapper)
        {
            _categoryContext = productContext;
            _mapper = mapper;
        }

        public Task<bool> CheckAllCategoriesExist(List<Guid> categoryIds)
        {
            if (categoryIds != null && categoryIds.Count() > 0)
            {
                var result = categoryIds.All(x => _categoryContext.Categories.Any(y => x == y.Id));
                return Task.FromResult(result);
            }
            return Task.FromResult(true);
        }

        public async Task<Entities.Category> CreateCategory(CategoryCreated category)
        {
            Entities.Category product2Add = _mapper.Map<Entities.Category>(category);
            return await CreateCategory(product2Add);
        }

        public async Task<Entities.Category> CreateCategory(Entities.Category category)
        {
            _categoryContext.Categories.Add(category);
            await Save();
            return category;
        }
        public async Task<int> Save()
        {
            try
            {
                return await _categoryContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
