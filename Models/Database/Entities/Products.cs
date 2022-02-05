using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Database.Entities
{
    public class Products
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long productId { get; set; }
        public string productName { get; set; }
        public long catId { get; set; }
        public decimal price { get; set; }
        public string image { get; set; }
    }
}
