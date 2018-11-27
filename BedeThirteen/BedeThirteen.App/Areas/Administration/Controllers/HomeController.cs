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

        public async Task<IActionResult> Transactions()
        {

            var results = await this.transactionService.GetLastNTransactions(10);

            if (results == null)
            {
                return View(new List<TransactionViewModel>());
               
            }
            else
            {
                var viewModel = new List<TransactionViewModel>();
                foreach(var transaction in results)
                {
                    viewModel.Add(new TransactionViewModel()
                    {
                        Amount = transaction.Amount,
                        Date = transaction.Date,
                        Id = transaction.Id.ToString(),
                        User = transaction.User.UserName,
                        Type = transaction.TransactionType.Name,
                        Description = transaction.Description

                    });

                };
                return View(viewModel);
            }
        }
    }
}