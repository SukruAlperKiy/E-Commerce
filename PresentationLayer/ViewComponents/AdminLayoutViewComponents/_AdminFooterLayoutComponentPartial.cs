using Microsoft.AspNetCore.Mvc;
namespace PresentationLayer.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminFooterLayoutComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
