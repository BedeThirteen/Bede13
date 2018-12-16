using BedeThirteen.Data.Context;
using BedeThirteen.Data.Models;
using BedeThirteen.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedeThirteen.Services
{
    public class DataAggregationService : IDataAggregationService
    {

        private readonly BedeThirteenContext context;

        public DataAggregationService(BedeThirteenContext context)
        {
            this.context = context;
        }

        public async Task<decimal> StakesSum(DateTime startOfRange, DateTime endOfRange, string userId = null)
        {
            var transactioType = this.context.TransactionTypes.First(t => t.Name == "Stake");
            IQueryable<Transaction> aggregationQuery;
            if (userId != null)
            {
                aggregationQuery = this.context.Transactions.Where(transaction => transaction.UserId == userId);
            }
            else
            {
                aggregationQuery = this.context.Transactions;
            }

            return await aggregationQuery.Where(transaction => transaction.TransactionType == transactioType).
                Where(transaction => transaction.CreatedOn > startOfRange && transaction.CreatedOn < endOfRange)
                .Select(transaction => transaction.Amount).SumAsync();
        }

        public async Task<decimal> WinsSum(DateTime startOfRange, DateTime endOfRange, string userId = null)
        {
            var transactioType = this.context.TransactionTypes.First(t => t.Name == "Win");
            IQueryable<Transaction> aggregationQuery;
            if (userId != null)
            {
                aggregationQuery = this.context.Transactions.Where(transaction => transaction.UserId == userId);
            }
            else
            {
                aggregationQuery = this.context.Transactions;
            }

            return await aggregationQuery.Where(transaction => transaction.TransactionType == transactioType).
                Where(transaction => transaction.CreatedOn > startOfRange && transaction.CreatedOn < endOfRange)
                .Select(transaction => transaction.Amount).SumAsync();
        }
        public async Task<decimal> DepositSum(DateTime startOfRange, DateTime endOfRange, string userId = null)
        {
            var transactioType = this.context.TransactionTypes.First(t => t.Name == "Deposit");
            IQueryable<Transaction> aggregationQuery;
            if (userId != null)
            {
                aggregationQuery = this.context.Transactions.Where(transaction => transaction.UserId == userId);
            }
            else
            {
                aggregationQuery = this.context.Transactions;
            }

            return await aggregationQuery.Where(transaction => transaction.TransactionType == transactioType).
                Where(transaction => transaction.CreatedOn > startOfRange && transaction.CreatedOn < endOfRange)
                .Select(transaction => transaction.Amount).SumAsync();
        }
        public async Task<decimal> WithdrawSum(DateTime startOfRange, DateTime endOfRange, string userId = null)
        {
            var transactioType = this.context.TransactionTypes.First(t => t.Name == "Withdraw");
            IQueryable<Transaction> aggregationQuery;
            if (userId != null)
            {
                aggregationQuery = this.context.Transactions.Where(transaction => transaction.UserId == userId);
            }
            else
            {
                aggregationQuery = this.context.Transactions;
            }

            return await aggregationQuery.Where(transaction => transaction.TransactionType == transactioType).
                Where(transaction => transaction.CreatedOn > startOfRange && transaction.CreatedOn < endOfRange)
                .Select(transaction => transaction.Amount).SumAsync();
        }
    }
}
