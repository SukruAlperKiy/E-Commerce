using Microsoft.AspNetCore.Mvc;
namespace PresentationLayer.ViewComponents.HomeViewComponents
{
    public class _HomeHeaderAreaSectionComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
