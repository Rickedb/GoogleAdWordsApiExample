using AdWordsApiExample.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AdWordsApiExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly GoogleAdWordsService _service;

        public ReportController(GoogleAdWordsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var report = await _service.GetReport("XXX-XXX-XXXX"); //your account id
            return Ok(report);
        }
    }
}
