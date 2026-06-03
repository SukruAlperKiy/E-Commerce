using BusinessLayer.Abstract;
using EntityLayer.Concrete;
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
            var degerler = _footerSolService.IFooterSolServiceGetAll();
            return View(degerler);
        }

        [HttpGet]
        public IActionResult CreateSolFooter()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateSolFooter(FooterSol varlik)
        {
            _footerSolService.IFooterSolServiceInsert(varlik);
            return RedirectToAction("FooterSolList");
        }
        [HttpGet]
        public IActionResult UpdateSolFooter(int id)
        {
            var deger = _footerSolService.IFooterSolServiceGetById(id);
            return View(deger);
        }
        [HttpPost]
        public IActionResult UpdateSolFooter(FooterSol varlik)
        {
            _footerSolService.IFooterSolServiceUpdate(varlik);
            return RedirectToAction("FooterSolList");
        }

        public IActionResult DeleteSolFooter(int id)
        {
            _footerSolService.IFooterSolServiceDelete(id);
            return RedirectToAction("FooterSolList");
        }

            
    }
}
