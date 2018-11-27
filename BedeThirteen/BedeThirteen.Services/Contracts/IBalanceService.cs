namespace BedeThirteen.Services.Contracts
{
    using System;
    using System.Threading.Tasks;

    public interface IBalanceService
    {
        Task<decimal> DepositAsync(string userId, decimal amount, Guid cardId);

        Task<decimal> WithdrawAsync(string userId, decimal amount, Guid cardId);

        Task<string> GetUserBalanceAsync(string userId);
    }
}