using Microsoft.AspNetCore.Mvc;
namespace PresentationLayer.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminHeadLayoutComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
