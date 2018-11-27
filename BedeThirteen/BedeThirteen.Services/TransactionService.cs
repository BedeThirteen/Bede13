using BedeThirteen.Data.Context;
using BedeThirteen.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedeThirteen.Services
{
    public class TransactionService : ITransactionService
    {
        private BedeThirteenContext context;

        public TransactionService(BedeThirteenContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Transaction>> GetLastNTransactions(int pageLength, int pagesToSkip = 0)
        {
           return await this.context.Transactions.Include(t => t.TransactionType).OrderByDescending(t => t.Date).Skip(pagesToSkip * pageLength).Take(pageLength).ToListAsync();
        }
    }
}
