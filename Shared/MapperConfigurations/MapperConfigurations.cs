using AutoMapper;
using Shared.Entities;
using Shared.RabbitMQ.Events.Category;
using Shared.RabbitMQ.Events.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.MapperConfigurations
{
    public class MapperConfigurations : Profile
    {
        public MapperConfigurations()
        {
            CreateMap<ProductCreated, Product>();
            CreateMap<CategoryCreated, Category>();

        }
    }
}
