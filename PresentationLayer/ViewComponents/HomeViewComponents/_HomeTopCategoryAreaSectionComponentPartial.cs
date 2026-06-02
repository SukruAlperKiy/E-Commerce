using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
namespace PresentationLayer.ViewComponents.HomeViewComponents
{
    public class _HomeTopCategoryAreaSectionComponentPartial:ViewComponent
    {
        private readonly ITopCategoryAreaService _topCategoryAreaService;
        public _HomeTopCategoryAreaSectionComponentPartial(ITopCategoryAreaService topCategoryAreaService222)
        {
            _topCategoryAreaService = topCategoryAreaService222;
        }
        public IViewComponentResult Invoke()
        {
            var degerler123 = _topCategoryAreaService.aaGetAll();
            return View(degerler123);
        }
    }
}
