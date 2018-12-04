namespace BedeThirteen.Services.Contracts
{
    using System;
    using System.Threading.Tasks;
    using BedeThirteen.Services.CompositeModels;

    public interface ITransactionService
    {

        Task<decimal> DepositAsync(string userId, decimal amount, Guid cardId);

        Task<decimal> WithdrawAsync(string userId, decimal amount, Guid cardId);

        Task<decimal> StakeAsync(string userId, decimal amount, string gameName);

        Task<decimal> WinAsync(string userId, decimal amount, string gameName);

        Task<TransactionsResult> GetTransactionsAsync(string filterBy, string filterCriteria, string aditionalCriteria, int pageSize, int pageNumber, string sortBy);
    }
}