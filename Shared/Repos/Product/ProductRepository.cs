using AutoMapper;
using Shared.DbContexts;
using Shared.Entities;
using Shared.RabbitMQ.Events.Product;

namespace Shared.Repos.Product
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _productContext;
        private readonly IMapper _mapper;
        public ProductRepository(ProductContext productContext, IMapper mapper)
        {
            _productContext = productContext;
            _mapper = mapper;
        }
        public async Task<Entities.Product> CreateProduct(ProductCreated product)
        {
            Entities.Product product2Add = _mapper.Map<Entities.Product>(product);
            return await CreateProduct(product2Add);
        }

        public async Task<Entities.Product> CreateProduct(Entities.Product product)
        {
            _productContext.Products.Add(product);
            var categories = product.CategoryIds;
            if (categories != null && categories.Count() > 0)
            {
                var productCategories = categories.Select(x => new ProductCategory()
                {
                    CategoryId = x,
                    ProductId = product.Id
                });

                _productContext.ProductCategories.AddRange(productCategories);
            }
            await Save();
            return product;
        }

        public async Task<int> Save()
        {
            try
            {
                return await _productContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
