using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using PresentationLayer.Models;
using System.Data;
namespace PresentationLayer.ViewComponents.HomeViewComponents
{
    public class _HomeNavbarSectionComponentPartial:ViewComponent 
    {
        private readonly dal _dal;
        public _HomeNavbarSectionComponentPartial(dal dal123)
        {
            _dal = dal123;
        }

        public IViewComponentResult Invoke()
        {
            string navbarKategorileriQuery = "Select kategoriIsim, kategoriStatus from Kategoriler";
            ViewBag.verilerViewBag = _dal.CommandExecuteReader(navbarKategorileriQuery, _dal.benimSqlBaglantim);

            return View();
        }
    }
}
