using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
namespace PresentationLayer.ViewComponents.HomeViewComponents
{
    public class _HomeWelcomeAreaSectionComponentPartial : ViewComponent
    {
        private readonly ISliderService _sliderService;
        public _HomeWelcomeAreaSectionComponentPartial(ISliderService sliderService123)
        {
            _sliderService = sliderService123;
        }
        public IViewComponentResult Invoke()
        {
            var degerler = _sliderService.zzGetAll();
            return View(degerler);
        }
    }
}
