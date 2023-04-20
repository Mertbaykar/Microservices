using Shared.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    [Table("Categories")]
    public class Category : EntityBase
    {
        public string Name { get; set; }
    }
}
