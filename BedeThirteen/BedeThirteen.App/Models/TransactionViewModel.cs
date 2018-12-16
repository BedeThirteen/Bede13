using System;
namespace BedeThirteen.App.Models
{
    public class TransactionViewModel
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }

        public string Type { get; set; }
        public decimal Amount { get; set; }

        public string Description { get; set; }
        public string User { get; set; }

    }
}
