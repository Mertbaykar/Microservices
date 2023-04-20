using MassTransit;
using MassTransit.Transports.Fabric;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using Shared.DbContexts;
using Shared.MapperConfigurations;
using Shared.RabbitMQ;
using Shared.RabbitMQ.Events.Product;
using Shared.Repos.Category;
using Shared.Repos.Product;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MapperConfigurations));

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(config =>
{
    config.JsonSerializerOptions.WriteIndented = true;
    config.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});


builder.Services.AddDbContext<ProductContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("productSqlConnection"));
});

builder.Services.AddDbContext<CategoryContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("categorySqlConnection"));
});


builder.Services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
// Consumer'lardan birinde ICategoryRepository gerektiði için resolve edilmeli
builder.Services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));


#region MassTransit - RabbitMQ

IConfigurationSection rabbitmqConfigs = builder.Configuration.GetSection("RabbitMqSettings");

builder.Services.AddMassTransit(busRegistrationConfig =>
{

    Type[] productCreateConsumers = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                        .Where(x => x.IsAssignableTo(typeof(IConsumer<ProductCreated>))).ToArray();

    busRegistrationConfig.AddConsumers(productCreateConsumers);


    busRegistrationConfig.UsingRabbitMq((busRegistrator, busFactoryConfigurator) =>
    {
        busFactoryConfigurator.Host(rabbitmqConfigs.GetValue<string>("Uri"), configurator =>
        {
            configurator.Username(rabbitmqConfigs.GetValue<string>("UserName"));
            configurator.Password(rabbitmqConfigs.GetValue<string>("Password"));
        });


        #region Produce Stages

        // exchangename
        busFactoryConfigurator.Message<ProductCreated>(x => x.SetEntityName(RabbitMQConstants.ProductExchange));

        #endregion


        #region Consumer Stages

        // queuename
        busFactoryConfigurator.ReceiveEndpoint(RabbitMQConstants.ProductCreated.QueueName, config =>
        {
            config.ConfigureConsumeTopology = false;
            // exchangename
            config.Bind(RabbitMQConstants.ProductExchange, x =>
            {
            });

            productCreateConsumers.ToList().ForEach(x =>
            {
                config.ConfigureConsumer(busRegistrator, x);
            });

        });
       
        #endregion

        busFactoryConfigurator.ConfigureEndpoints(busRegistrator);
    });
});

#endregion
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
