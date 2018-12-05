using System;
using System.Threading.Tasks;

namespace BedeThirteen.Services.Contracts
{
    public interface IDataAggregationService
    {
        Task<decimal> StakesSum(DateTime startOfRange, DateTime endOfRange, string userId = null);

        Task<decimal> DepositSum(DateTime startOfRange, DateTime endOfRange, string userId = null);

        Task<decimal> WinsSum(DateTime startOfRange, DateTime endOfRange, string userId = null);

        Task<decimal> WithdrawSum(DateTime startOfRange, DateTime endOfRange, string userId = null);
    }
}