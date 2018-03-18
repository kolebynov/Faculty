using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faculty.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Faculty.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IMapper mapper)
        { }

        public IActionResult Index()
        {
            return View();
        }
    }
}