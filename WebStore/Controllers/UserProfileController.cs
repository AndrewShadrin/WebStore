using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
