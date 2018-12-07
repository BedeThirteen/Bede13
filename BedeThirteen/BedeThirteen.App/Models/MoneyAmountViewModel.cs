using System;
using System.ComponentModel.DataAnnotations;

namespace BedeThirteen.App.Models
{
    public class MoneyAmountViewModel
    {
        [Required]
        public Guid CardId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage= "Amount must be a positive number.")]         
        public decimal Amount { get; set; }
    }
}