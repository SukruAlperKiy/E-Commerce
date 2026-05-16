using Microsoft.AspNetCore.Mvc;
namespace PresentationLayer.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminMainPanelLayoutComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
