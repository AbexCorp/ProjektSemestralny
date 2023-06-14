using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace StoreApp.Model
{
    [Table("Products")]
    [Index(nameof(Product.Name), IsUnique = true)]
    public class Product
    {
        [Key, Column("id_product")]
        public int IdProduct { get; set; }

        //[Column("id_category")]
        //public int IdCategory { get; set; } //fk

        [Column("name"), Required, NotNull, MaxLength(60)]
        public string? Name { get; set; }




        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual List<SingularObject> SingularObject { get; set; }
    }
}
