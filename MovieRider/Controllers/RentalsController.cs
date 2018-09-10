using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieRider.Controllers
{
    public class RentalsController : Controller
    {
        public IActionResult New()
        {
            return View();
        }
    }
}