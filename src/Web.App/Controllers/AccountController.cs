using System.Web.Mvc;

namespace Web.App.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        // GET: /Account/LoginMenu
        [AllowAnonymous]
        public PartialViewResult LoginMenu()
        {
            return PartialView("_LoginPartial");
        }
    }
}