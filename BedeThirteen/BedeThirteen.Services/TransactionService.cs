namespace BedeThirteen.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BedeThirteen.Data.Context;
    using BedeThirteen.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class TransactionService : ITransactionService
    {
        private readonly BedeThirteenContext context;

        public TransactionService(BedeThirteenContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Transaction>> GetLastNTransactions(int pageLength, int pagesToSkip = 0)
        {
            return await this.context.Transactions.Include(t => t.TransactionType)
                 .Include(t => t.User).OrderByDescending(t => t.Date)
                 .Skip(pagesToSkip * pageLength).Take(pageLength)
                 .ToListAsync();
        }
    }
}
