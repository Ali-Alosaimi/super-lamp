using System;
namespace Models.Response.Products
{
    public class ProductsResponse
    {
        public long productId { get; set; }
        public string productName { get; set; }
        public decimal price { get; set; }
        public string image { get; set; }
        public long catId { get; set; }
        public string catName { get; set; }
    }
}
