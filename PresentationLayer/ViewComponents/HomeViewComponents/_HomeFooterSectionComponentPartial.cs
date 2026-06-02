using BusinessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
namespace PresentationLayer.ViewComponents.HomeViewComponents
{
    public class _HomeFooterSectionComponentPartial:ViewComponent
    {
        private readonly IFooterService _footerService;
        public _HomeFooterSectionComponentPartial(IFooterService footerService)
        {
            _footerService = footerService;
        }


        public IViewComponentResult Invoke()
        {
            var degerler = _footerService.ifooterserviceGetAll();
            return View(degerler);
        }
    }
}
