using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
namespace PresentationLayer.ViewComponents.HomeViewComponents
{
    public class _HomeHeaderAreaSectionComponentPartial:ViewComponent
    {
        private readonly dal _dal;
        public _HomeHeaderAreaSectionComponentPartial(dal dal123)
        {
            _dal = dal123;
        }
        public IViewComponentResult Invoke()
        {
            string kategorilerQuery = "Select kategoriIsim, kategoriStatus from Kategoriler";
            ViewBag.kategorilerViewBag = _dal.CommandExecuteReader(kategorilerQuery, _dal.benimSqlBaglantim);

            return View();
        }
    }
}
