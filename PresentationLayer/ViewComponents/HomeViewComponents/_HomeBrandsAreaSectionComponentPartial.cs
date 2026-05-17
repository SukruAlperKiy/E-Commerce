using Microsoft.AspNetCore.Mvc;
namespace PresentationLayer.ViewComponents.HomeViewComponents
{
    public class _HomeBrandsAreaSectionComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
