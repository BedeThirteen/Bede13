using System;
using System.ComponentModel.DataAnnotations;

namespace BedeThirteen.App.Controllers
{
    public class DepositViewModel
    {
        [Required]
        public Guid CardId { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Amount { get; set; }
    }
}