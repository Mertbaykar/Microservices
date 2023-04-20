using Shared.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    [Table("Products")]
    public class Product : EntityBase
    {
        public string Name { get; set; }
        [NotMapped]
        public List<Guid> CategoryIds { get; set; } = new List<Guid>();
    }
}
