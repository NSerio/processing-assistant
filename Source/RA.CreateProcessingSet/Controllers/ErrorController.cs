using System.Web.Mvc;

namespace RA.CreateProcessingSet.Controllers
{
	[HandleError]
	public class ErrorController : Controller
	{
		[AllowAnonymous]
		public ViewResult Index()
		{
			return View("Error");
		}

		[AllowAnonymous]
		public ActionResult AccessDenied()
		{
			return View();
		}
	}
}