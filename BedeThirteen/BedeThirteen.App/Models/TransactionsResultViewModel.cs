using System.Collections.Generic;
namespace BedeThirteen.App.Models
{
    public class TransactionsResultViewModel
    {
        public IEnumerable<TransactionViewModel> Transactions { get; set; }
        public int NumberOfPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
