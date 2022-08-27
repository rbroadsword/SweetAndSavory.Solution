using Microsoft.AspNetCore.Mvc; 

namespace SweetAndSavory.Controllers
{
    public class HomeController : Controllers
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
            return View(); 
        }
    }
}