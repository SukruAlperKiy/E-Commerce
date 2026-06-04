using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers
{
    public class AdminController : Controller
    {
        private readonly dal _dal;
        public AdminController(dal dal123)
        {
            _dal = dal123;
        }

        public IActionResult Index()
        {
            return View();
        }


        #region footerLogo
        public IActionResult Footer_Logo()
        {
            string FooterLogoSql = "Select FooterLogoId, FooterLogoFotoUrl, FooterLogoAltBaslik from FooterLogo";
            ViewBag.FooterLogoKismi = _dal.CommandExecuteReader(FooterLogoSql, _dal.benimSqlBaglantim);
            return View();
        }

        public IActionResult Footer_Logo_Update(int id)
        {
            string FooterLogoSql2 = $"Select FooterLogoId, FooterLogoFotoUrl, FooterLogoAltBaslik from FooterLogo where FooterLogoId = {id}";
            ViewBag.FooterLogoKismiUpdate = _dal.CommandExecuteReader(FooterLogoSql2, _dal.benimSqlBaglantim);
            return View();
        }


        #endregion
    }
}
