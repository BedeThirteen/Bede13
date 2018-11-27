using System.Collections.Generic;
using System.Threading.Tasks;
using BedeThirteen.Data.Models;

namespace BedeThirteen.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetLastNTransactions(int pageLength, int pagesToSkip = 0);
    }
}