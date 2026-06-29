using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
namespace PresentationLayer.ViewComponents.HomeViewComponents
{
    public class _HomeWelcomeAreaSectionComponentPartial : ViewComponent
    {
        private readonly ISliderService _sliderService;
        private readonly dal _dal;
        public _HomeWelcomeAreaSectionComponentPartial(ISliderService sliderService123, dal dal123)
        {
            _sliderService = sliderService123;
            _dal = dal123;
        }
        public IViewComponentResult Invoke()
        {
            string SelectSliderQuery = "Select SliderKucukTitle, SliderTitle, ImageUrl from EfSliders";
            ViewBag.SelectSliderViewBag = _dal.CommandExecuteReader(SelectSliderQuery, _dal.benimSqlBaglantim);

            var degerler = _sliderService.zzGetAll();
            return View(degerler);
        }
    }
}
