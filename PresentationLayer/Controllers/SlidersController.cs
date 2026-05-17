using BusinessLayer.Abstract;
using EntityLayer.Concrete;
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

        [HttpGet]
        public IActionResult CreateSlider()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateSlider(Slider varlik)
        {
            _sliderService.zzInsert(varlik);
            return RedirectToAction("SliderList");
        }

        public IActionResult DeleteSlider(int id)
        {
            _sliderService.zzDelete(id);
            return RedirectToAction("SliderList");
        }


        [HttpGet]
        public IActionResult UpdateSlider(int id)
        {
            var deger = _sliderService.zzGetById(id);
            return View(deger);
        }
        [HttpPost]
        public IActionResult UpdateSlider(Slider Varlik)
        {
            _sliderService.zzUpdate(Varlik);
            return RedirectToAction("SliderList");
        }
    }
}
