using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RabbitMQ.Events.Product
{
    public class ProductCreated
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public List<Guid> CategoryIds { get; set; } = new List<Guid>();
    }
}
