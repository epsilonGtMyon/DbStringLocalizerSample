using Microsoft.AspNetCore.Mvc;

namespace DbStringLocalizerSample.App.Endpoints
{
    [Route("/")]
    public class IndexController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return Redirect("/index.html");
        }
    }
}
