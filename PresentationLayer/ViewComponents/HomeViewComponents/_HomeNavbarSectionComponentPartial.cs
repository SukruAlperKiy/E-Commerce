using Microsoft.AspNetCore.Mvc;
namespace PresentationLayer.ViewComponents.HomeViewComponents
{
    public class _HomeNavbarSectionComponentPartial:ViewComponent 
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
