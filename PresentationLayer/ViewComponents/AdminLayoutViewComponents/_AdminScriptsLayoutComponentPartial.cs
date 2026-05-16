using Microsoft.AspNetCore.Mvc;
namespace PresentationLayer.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminScriptsLayoutComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
