using Microsoft.AspNetCore.Mvc;
namespace PresentationLayer.ViewComponents.HomeViewComponents
{
    public class _HomeFooterSectionComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
