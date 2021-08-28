using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailTime.Controllers.MvcControllers
{
    [Controller]
    public class HomeController: Controller
    {
        [Route("Home/index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
