using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class FooterController : Controller
    {
        private readonly IFooterService _footerService;
        public FooterController(IFooterService footerService)
        {
            _footerService = footerService;
        }

        public IActionResult FooterList()
        {
            var degerler = _footerService.ifooterserviceGetAll();
            return View(degerler);
        }
        public IActionResult FooterDelete(int id)
        {
            _footerService.ifooterserviceDelete(id);
            return RedirectToAction("FooterList");
        }

        [HttpGet]
        public IActionResult CreateFooter()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateFooter(Footer varlik)
        {
            _footerService.ifooterserviceInsert(varlik);
            return RedirectToAction("FooterList");
        }

        [HttpGet]
        public IActionResult FooterUpdate(int id)
        {
            var degerler = _footerService.ifooterserviceGetById(id);
            return View(degerler);
        }

        [HttpPost]
        public IActionResult FooterUpdate(Footer varlik)
        {
            _footerService.ifooterserviceUpdate(varlik);
            return RedirectToAction("FooterList");
        }

    }
}
