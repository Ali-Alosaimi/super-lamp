using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Models.Database.Entities;

namespace Models.Request
{
    public class AddProductRequest
    {
        public string productName { get; set; }
        public decimal price { get; set; }
        public IFormFile image { get; set; }
        public long catId { get; set; }
        public long productId { get; set; }

        public IEnumerable<Categories> categories { get; set; }
    }
}
