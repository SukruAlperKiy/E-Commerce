using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class FooterSolController : Controller
    {
        private readonly IFooterSolService _footerSolService;
        public FooterSolController(IFooterSolService footerSolService)
        {
            _footerSolService = footerSolService;
        }
        public IActionResult FooterSolList()
        {
            return View();
        }
    }
}
