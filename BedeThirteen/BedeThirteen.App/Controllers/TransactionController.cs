using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BedeThirteen.App.Models;
using BedeThirteen.Services.CompositeModels;
using BedeThirteen.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BedeThirteen.App.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }


        public IActionResult Search()
        {
            // var model = await GetTransactionListAsync(filterBy, filterCriteria, pageSize, pageNumber, sortBy);
            return View();
        }
        public async Task<IActionResult> GetSearchResultsAsync(
            string filterBy,
            string filterCriteria,
            string aditionalCriteria,
            int pageSize,
            int pageNumber,
            string sortBy)
        {

            var userId = "";
            if (this.User.IsInRole("User"))
            { userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier); }

            var transactionsResult = await this.transactionService
              .GetTransactionsAsync(filterBy, filterCriteria, aditionalCriteria, pageSize, pageNumber, sortBy, userId);

            var model = new TransactionsResultViewModel()
            {
                Transactions = transactionsResult.Transactions
                          .Select(t => new TransactionViewModel
                          {
                              Id = t.Id.ToString(),
                              Amount = t.Amount,
                              Date = t.Date,
                              Description = t.Description,
                              Type = t.TransactionType.Name,
                              User = userId == "" ? t.User.UserName : ""
                          }).ToList(),

                NumberOfPages = transactionsResult.TotalCount % pageSize != 0
                ? (transactionsResult.TotalCount / pageSize) + 1
                : transactionsResult.TotalCount / pageSize,
                CurrentPage = pageNumber,
            };

            return PartialView("_SearchResultPartial", model);
        }
    }
}