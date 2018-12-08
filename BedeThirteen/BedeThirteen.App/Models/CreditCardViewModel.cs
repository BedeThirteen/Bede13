using System.ComponentModel.DataAnnotations;

namespace BedeThirteen.App.Models
{
    public class CreditCardViewModel
    {
        [Required(ErrorMessage = "Required")]
        [CreditCard]
        public string CardNumber { get; set; }

        [Range(0, 12)]
        public int Month { get; set; }
        [Range(2018, 2025)]
        public int Year { get; set; }

        [MaxLength(4)]
        [MinLength(3)]
        public string Cvv { get; set; }
    }
}
