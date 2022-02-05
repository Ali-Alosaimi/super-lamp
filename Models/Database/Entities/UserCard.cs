using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Database.Entities
{
    public class UserCard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public long userId { get; set; }
        public string cardNumber { get; set; }
        public string cvc { get; set; }
        public long expMonth { get; set; }
        public long expYear { get; set; }
    }
}
