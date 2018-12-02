using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BedeThirteen.App.Areas.Administration.Models;
using BedeThirteen.Data.Models;
using BedeThirteen.Services;
using Microsoft.AspNetCore.Mvc;

namespace BedeThirteen.App.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class HomeController : Controller
    {
        private readonly ITransactionService transactionService;
        public HomeController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetTransactions(string sortOrder)
        {
            var result = await GetTransactionListAsync(sortOrder);

            return PartialView("_TransactionsResultPartial", result);
        }



        public async Task<IActionResult> Transactions(string sortOrder = "")
        {
            //ViewData["AmountSortParm"] = string.IsNullOrEmpty(sortOrder) ? "amount_desc" : "";
            //ViewData["DateSortParm"] = sortOrder == "date" ? "date_desc" : "date";

            var result = await GetTransactionListAsync(sortOrder);

            return View(result);
        }
        private async Task<List<TransactionViewModel>> GetTransactionListAsync(string sortOrder)
        {
            var transactions = await this.transactionService.GetTransactionsAsync(sortOrder);

            var result = transactions
              .Select(t => new TransactionViewModel
              {
                  Id = t.Id.ToString(),
                  Amount = t.Amount,
                  Date = t.Date,
                  Description = t.Description,
                  Type = t.TransactionType.Name,
                  User = t.User.UserName
              })
              .ToList();
            return result;
        }
    }
}