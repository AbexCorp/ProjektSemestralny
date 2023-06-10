using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Model
{
    public class Warehouse
    {
        public int SerialNumber { get; set; } //pk
        public int IdProduct { get; set; } //fk
        public bool IsNotSold { get; set; }
    }
}
