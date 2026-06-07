using BusinessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using System.Data;
namespace PresentationLayer.ViewComponents.HomeViewComponents
{
    public class _HomeFooterSectionComponentPartial:ViewComponent
    {
        private readonly IFooterService _footerService;
        private readonly IFooterSolService _footerSolService;
        private readonly dal _dal;
        public _HomeFooterSectionComponentPartial(IFooterService footerService, IFooterSolService footerSolService, dal dal123)
        {
            _footerService = footerService;
            _footerSolService = footerSolService;
            _dal = dal123;
        }


        public IViewComponentResult Invoke()
        {
            var degerler = _footerService.ifooterserviceGetAll();
            var degerler2 = _footerSolService.IFooterSolServiceGetAll();

            string logoSql = "Select Top 1 FooterLogoFotoUrl, FooterLogoAltBaslik from FooterLogo";
            DataSet veriKumesi = _dal.CommandExecuteReader(logoSql, _dal.benimSqlBaglantim);

            
                string sqlBossaDefaultLogo= "photos/footerlogo/Tucan2.png";
                string sqlBossaDefaultText = "Default Text";

                if (veriKumesi.Tables.Count > 0 && veriKumesi.Tables[0].Rows.Count > 0)
                {
                    sqlBossaDefaultLogo = veriKumesi.Tables[0].Rows[0]["FooterLogoFotoUrl"].ToString();
                    sqlBossaDefaultText = veriKumesi.Tables[0].Rows[0]["FooterLogoAltBaslik"].ToString();
                }
            

            var heheha = new FooterAllViewModel
            {
                FooterListModel = degerler,
                FooterSolListModel = degerler2,
                FooterLogoFotoUrl = sqlBossaDefaultLogo,
                FooterLogoAltBaslik = sqlBossaDefaultText
            };

            return View(heheha);
        }
    }
}
