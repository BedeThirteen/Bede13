namespace BedeThirteen.Services.CompositeModels
{
    using System.Collections.Generic;
    using BedeThirteen.Data.Models;

    public class TransactionsResult
    {
        public IEnumerable<Transaction> Transactions { get; set; }

        public int TotalCount { get; set; }
    }
}
