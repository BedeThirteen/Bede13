namespace BedeThirteen.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BedeThirteen.Data.Models;

    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetLastNTransactions(int pageLength, int pagesToSkip = 0);

        Task<decimal> DepositAsync(string userId, decimal amount, Guid cardId);

        Task<decimal> WithdrawAsync(string userId, decimal amount, Guid cardId);

        Task<decimal> StakeAsync(string userId, decimal amount, string gameName);
        Task<decimal> WinAsync(string userId, decimal amount,  string gameName);

    }
}