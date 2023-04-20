using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RabbitMQ
{
    public class RabbitMQConstants
    {
        public const string ProductExchange = "product";
        public const string CategoryExchange = "category";

        public static readonly RabbitMQConstants ProductCreated = new RabbitMQConstants(ProductExchange, "product-created");
        public static readonly RabbitMQConstants ProductUpdated = new RabbitMQConstants(ProductExchange, "product-updated");

        public static readonly RabbitMQConstants CategoryCreated = new RabbitMQConstants(CategoryExchange, "category-created");
        public static readonly RabbitMQConstants CategoryUpdated = new RabbitMQConstants(CategoryExchange, "category-updated");
        public RabbitMQConstants(string exchangeName, string queueName)
        {
            ExchangeName = exchangeName;
            QueueName = queueName;
        }
        public string ExchangeName { get; set; }
        public string QueueName { get; set; }
    }
}
