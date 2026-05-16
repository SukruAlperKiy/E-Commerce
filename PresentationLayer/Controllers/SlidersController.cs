using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class SlidersController : Controller
    {
        private readonly ISliderService _sliderService;
        public SlidersController(ISliderService sliderService123)
        {
            _sliderService = sliderService123;
        }

        public IActionResult SliderList()
        {
            var degerler = _sliderService.zzGetAll();
            return View(degerler);
        }
    }
}
