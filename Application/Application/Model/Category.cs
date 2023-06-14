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
    [Table("Categories")]
    [Index(nameof(Category.Name), IsUnique = true)]
    public class Category
    {
        [Key, Column("id_category")]
        public int IdCategory { get; set; }

        [Required, MaxLength(40), Column("name"), NotNull]
        public string? Name { get; set; }




        public virtual List<Product> Products { get; set; }
    }
}
