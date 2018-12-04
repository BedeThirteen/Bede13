using Microsoft.AspNetCore.Mvc;

namespace BedeThirteen.App.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}