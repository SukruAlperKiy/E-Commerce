using Microsoft.AspNetCore.Mvc;
namespace PresentationLayer.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminCustomTemplateLayoutComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
