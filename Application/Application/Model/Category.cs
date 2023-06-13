using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace StoreApp.Model
{
    [Table("Categories")]
    public class Category
    {
        [Key, Column("id_category")]
        public int IdCategory { get; set; }

        [Required, MaxLength(40), Column("name"), NotNull]
        public string? Name { get; set; }




        public virtual List<Product> Products { get; set; }
    }
}
