namespace BedeThirteen.Services.Contracts
{
    using System;
    using System.Threading.Tasks;
    using BedeThirteen.Data.Models;

    public interface IBalanceService
    {
        Task<decimal> Deposit(string userId, decimal amount, Guid cardId);

        Task<string> GetUserBalance(string userId);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <param name="amount"></param>
        ///// <returns>Remaining balance.</returns>
        //Task<decimal> DecreaseBalance(string userId, decimal amount);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <param name="amount"></param>
        ///// <returns>Current balance.</returns>
        //Task<decimal> IncreaseBalance(string userId, decimal amount);
    }
}