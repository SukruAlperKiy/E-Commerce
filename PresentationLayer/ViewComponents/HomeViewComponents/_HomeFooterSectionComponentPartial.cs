using BusinessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
namespace PresentationLayer.ViewComponents.HomeViewComponents
{
    public class _HomeFooterSectionComponentPartial:ViewComponent
    {
        private readonly IFooterService _footerService;
        private readonly IFooterSolService _footerSolService;
        public _HomeFooterSectionComponentPartial(IFooterService footerService, IFooterSolService footerSolService)
        {
            _footerService = footerService;
            _footerSolService = footerSolService;
        }


        public IViewComponentResult Invoke()
        {
            var degerler = _footerService.ifooterserviceGetAll();
            var degerler2 = _footerSolService.IFooterSolServiceGetAll();

            var heheha = new FooterAllViewModel
            {
                FooterListModel = degerler,
                FooterSolListModel = degerler2,
            };

            return View(heheha);
        }
    }
}
