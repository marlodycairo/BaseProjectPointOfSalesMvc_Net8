using Microsoft.AspNetCore.Mvc;

namespace BaseProjectMvc_Net8.Controllers
{
	public class ItemController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
