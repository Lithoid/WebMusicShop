using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("Statuses")]
    public class Status : DbEntity
    {
        [Column("Name")]
        [MaxLength(32)]
        public string Name { get; set; }

        public List<Order> Orders { get; set; }
    }
}
