using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApp.Model
{
    [Table("Orders")]
    public class Order
    {
        [Key, Column("id_order")]
        public int IdOrder { get; set; }

        //[Column("serial_number")]
        //public int SerialNumber { get; set; } //fk



        public int SingularObjectId { get; set; }
        public virtual SingularObject SingularObject { get; set; }
    }
}
