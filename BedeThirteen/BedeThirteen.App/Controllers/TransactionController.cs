using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BedeThirteen.App.Models;
using BedeThirteen.Services.Contracts;
using BedeThirteen.Services.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BedeThirteen.App.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly ITransactionService transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        public async Task<IActionResult> Search()
        {
            var model = await this.GetResultModelAsync(
                new FilterTransactionsViewModel()
                {
                    FilterBy = "all",
                    PageNumber = 0,
                    PageSize = 15,
                    SortBy = "date_desc",
                    FilterCriteria = "",
                    AditionalCriteria = ""
                }
                );
            return View(model);
        }

        public async Task<IActionResult> GetSearchResultsAsync(FilterTransactionsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                TransactionsResultViewModel resultModel = await GetResultModelAsync(model);

                return PartialView("_SearchResultPartial", resultModel);
            }
            catch (ServiceException)
            {
                return BadRequest();
            }
        }

        private async Task<TransactionsResultViewModel> GetResultModelAsync(FilterTransactionsViewModel model)
        {
            var userId = "";
            if (this.User.IsInRole("User")) { userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier); }

            var transactionsResult = await this.transactionService
              .GetTransactionsAsync(model.FilterBy, model.FilterCriteria, model.AditionalCriteria,
                                    model.PageSize, model.PageNumber, model.SortBy, userId);

            var resultModel = new TransactionsResultViewModel()
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

                NumberOfPages = transactionsResult.TotalCount % model.PageSize != 0
                ? (transactionsResult.TotalCount / model.PageSize) + 1
                : transactionsResult.TotalCount / model.PageSize,
                CurrentPage = model.PageNumber
            };

            return resultModel;
        }
    }
}