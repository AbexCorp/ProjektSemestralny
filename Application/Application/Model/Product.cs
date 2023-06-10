using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Model
{
    public class Product
    {
        public int IdProduct { get; set; } //pk
        public int IdCategory { get; set; } //fk
        public string? Name { get; set; }
    }
}
