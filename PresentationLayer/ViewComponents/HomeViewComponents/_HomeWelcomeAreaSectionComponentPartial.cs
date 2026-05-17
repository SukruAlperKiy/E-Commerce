using Microsoft.AspNetCore.Mvc;
namespace PresentationLayer.ViewComponents.HomeViewComponents
{
    public class _HomeWelcomeAreaSectionComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
