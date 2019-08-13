using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201809;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdWordsApiExample.Controllers
{
    public class ReportController : Controller
    {
        public ReportController()
        {

        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            
            return View();
        }
    }
}
