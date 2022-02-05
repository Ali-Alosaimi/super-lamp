using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Database.Entities
{
    public class OrderDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long orderDetailID { get; set; }
        public long orderID { get; set; }
        public long productID { get; set; }
        public long qty { get; set; }
        [NotMapped]
        public string ProductName { get; set; }
        [NotMapped]
        public decimal ProductPrice { get; set; }
    }
}
