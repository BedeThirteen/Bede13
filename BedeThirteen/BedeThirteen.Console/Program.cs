using BedeThirteen.Data.Context;
using BedeThirteen.Data.Models;
using BedeThirteen.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BedeThirteen.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
           // Run().Wait();

        }

        //private static async Task Run()
        //{

        //var context = new BedeThirteenContext();

        //var cardService = new CreditCardService(context);
        //var user = context.Users.FirstOrDefault();

        //var card = await cardService.AddCreditCardAsync("1234567890123456", "333", DateTime.Now.AddDays(1), user.Id);
        //var card2 = await cardService.AddCreditCardAsync("6543210987654321", "111", DateTime.Now.AddDays(3), user.Id);

        //  Console.WriteLine($"card {card.Id} {card.Number} {card.Cvv}");



        //var cards = await cardService.GetUserCardsAsync(user.Id);

        //Console.WriteLine($"Casrds: \r\n {string.Join(", ", cards.Select(c=>c.Number))}");
        //  }
    }
}
