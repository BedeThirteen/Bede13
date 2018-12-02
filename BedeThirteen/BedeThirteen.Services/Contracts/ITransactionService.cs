namespace BedeThirteen.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BedeThirteen.Data.Models;

    public interface ITransactionService
    {
        // Task<IEnumerable<Transaction>> GetLastNTransactionsAsync(int pageLength, int pagesToSkip = 0);
        Task<IEnumerable<Transaction>> GetTransactionsAsync(string sortOrder);

        Task<decimal> DepositAsync(string userId, decimal amount, Guid cardId);

        Task<decimal> WithdrawAsync(string userId, decimal amount, Guid cardId);
    }
}