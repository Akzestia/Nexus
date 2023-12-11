using Microsoft.AspNetCore.Mvc;

namespace Nexus.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
