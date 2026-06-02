using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class TopCategoryAreaController : Controller
    {
        private readonly ITopCategoryAreaService _topCategoryAreaService;
        public TopCategoryAreaController(ITopCategoryAreaService topCategoryAreaService123)
        {
            _topCategoryAreaService = topCategoryAreaService123;
        }

        public IActionResult TopCategoryAreaList()
        {
            var degerler = _topCategoryAreaService.aaGetAll();
            return View(degerler);
        }

        [HttpGet]
        public IActionResult CreateTopCategoryArea()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTopCategoryArea(TopCategoryArea varlik123)
        {
            _topCategoryAreaService.aaInsert(varlik123);
            return RedirectToAction("TopCategoryAreaList");
        }

        public IActionResult DeleteTopCategoryArea(int id)
        {
            _topCategoryAreaService.aaDelete(id);
            return RedirectToAction("TopCategoryAreaList");
        }

        [HttpGet]
        public IActionResult UpdateTopCategoryArea(int id)
        {
            var deger = _topCategoryAreaService.aaGetById(id);
            return View(deger);
        }

        [HttpPost]
        public IActionResult UpdateTopCategoryArea(TopCategoryArea varlik777)
        {
            _topCategoryAreaService.aaUpdate(varlik777);
            return RedirectToAction("TopCategoryAreaList");
        }
    }
}
