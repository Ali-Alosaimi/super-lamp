using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Database.Entities
{
    public class ProductCart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long cartID { get; set; }
        public long productID { get; set; }
        public long UserID { get; set; }
        public int qty { get; set; }
        [NotMapped]
        public string ProductName { get; set; }
        [NotMapped]
        public decimal ProductPrice { get; set; }
    }
}
