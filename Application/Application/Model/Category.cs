using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Application.Model
{
    [Table("Categories")]
    public class Category
    {
        [Key, Column("id_product")]
        public int IdProduct { get; set; }


        [Required, MaxLength(40), Column("name"), NotNull]
        public string? Name { get; set; }
    }
}
