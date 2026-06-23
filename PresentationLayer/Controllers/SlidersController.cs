using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers
{
    public class SlidersController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly dal _dal;
        public SlidersController(ISliderService sliderService123, dal dal123)
        {
            _sliderService = sliderService123;
            _dal = dal123;
        }


        #region Entity Framework Slider
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
        #endregion

        #region Yeni Slider

        public IActionResult SliderSelect()
        {
            string SlidersSelectQuery = "Select SliderId, SliderKucukTitle, SliderTitle, ImageUrl from EfSliders";
            ViewBag.SlidersSelectViewBag = _dal.CommandExecuteReader(SlidersSelectQuery, _dal.benimSqlBaglantim);
            return View();
        }

        [HttpGet]
        public IActionResult SliderUpdate(int id)
        {
            string SliderGetQuery = $"Select SliderId, SliderKucukTitle, SliderTitle, ImageUrl from EfSliders where SliderId = {id}";
            ViewBag.SliderInsertViewBag = _dal.CommandExecuteReader(SliderGetQuery, _dal.benimSqlBaglantim);

            return View();
        }


        [HttpGet]
        public IActionResult SliderInsert()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SliderInsert(string SliderKucukTitleParametre, string SliderTitleParametre, string ImageUrlParametre)
        {
            string CreateSliderQuery = @$"
            Insert Into EfSliders
            (SliderKucukTitle, SliderTitle, ImageUrl)
            Values
            (@SliderKucukTitleTutucu, @SliderTitleTutucu, @ImageUrlTutucu)";

            using (SqlConnection baglanti = _dal.benimSqlBaglantim)
            {
                using (SqlCommand emir = new SqlCommand(CreateSliderQuery, baglanti))
                {
                    emir.Parameters.AddWithValue("@SliderKucukTitleTutucu", SliderKucukTitleParametre);
                    emir.Parameters.AddWithValue("@SliderTitleTutucu", SliderTitleParametre);
                    emir.Parameters.AddWithValue("@ImageUrlTutucu", ImageUrlParametre);

                    baglanti.Open();

                    emir.ExecuteNonQuery();
                }
            }

                return RedirectToAction("SliderSelect");
        }

        #endregion

    }
}
