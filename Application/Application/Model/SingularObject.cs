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
    [Table("Warehouse")]
    public class SingularObject
    {
        [Key, Column("serial_number")]
        public int SerialNumber { get; set; }

        //[Column("id_product")]
        //public int IdProduct { get; set; } //fk

        //[Column("state"), Required, NotNull]
        //public bool IsNotSold { get; set; }



        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        /*
        public int OrderId { get; set; }
        public virtual Order? Order { get; set; }
        */
    }
}
