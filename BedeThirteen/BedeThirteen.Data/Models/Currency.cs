namespace BedeThirteen.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BedeThirteen.Data.Models.Abstract;

    public class Currency : Entity
    {
        public Currency()
        {
            this.Users = new HashSet<User>();
        }

        [Key]
        [MaxLength(5)]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        public decimal ConversionRateToUSD { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
