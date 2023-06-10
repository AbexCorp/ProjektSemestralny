using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Model
{
    public class Order
    {
        public int IdOrder { get; set; } //pk
        public int SerialNumber { get; set; } //fk
    }
}
