namespace BedeThirteen.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using BedeThirteen.Data.Models.Abstract;

    public class CreditCard : GUIDEntity
    {
        [Required]

        // Commented during development
        // [MaxLength(16)]
        // [MinLength(16)]
        public string Number { get; set; }

        [MaxLength(4)]
        [MinLength(3)]
        public string Cvv { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Expiry { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
