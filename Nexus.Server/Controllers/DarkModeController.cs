using Microsoft.AspNetCore.Mvc;
using Nexus.Server.ThemeController;

namespace Nexus.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DarkModeController : ControllerBase
    {
        private readonly ThemeControllerX controller;

        public DarkModeController(ThemeControllerX controller) { 
            this.controller = controller;
        }

        [Route("get")]
        [HttpGet]
        public JsonResult GetTheme()
        {
            return new JsonResult(controller.GetTheme());
        }

        [Route("toggle/{mode}")]
        [HttpPost]
        public JsonResult SwitchMode(bool mode)
        {
            return new JsonResult(controller.ToggleThemeMode(mode));
        }
    }
}
