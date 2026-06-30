using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PresentationLayer.Models;
namespace PresentationLayer.ViewComponents.HomeViewComponents
{
    public class _HomeCTAAreaSectionComponentPartial:ViewComponent
    {
        private readonly dal _dal;

        public _HomeCTAAreaSectionComponentPartial(dal dal123)
        {
            _dal = dal123;
        }
        public IViewComponentResult Invoke()
        {
            string KategoriUrunSelectQuery = $@"SELECT u.UrunId, u.UrunFiyat, u.UrunAciklama, u.UrunIsim, u.UrunStatus, u.UrunlerKapakFoto1, k.kategoriId
                                                FROM UrunKategori uk
                                                INNER JOIN Kategoriler k ON uk.kategoriId = k.kategoriId 
                                                Inner Join Urunler u On uk.UrunId = u.UrunId
                                                WHERE k.kategoriStatus = 1 order by k.kategoriId";
            ViewBag.KategoriUrunSelectViewBag = _dal.CommandExecuteReader(KategoriUrunSelectQuery, _dal.benimSqlBaglantim);

            string KategoriSelectQuery = $@"SELECT k.kategoriIsim, k.kategoriId
                                                FROM UrunKategori uk
                                                INNER JOIN Kategoriler k ON uk.kategoriId = k.kategoriId 
                                                Inner Join Urunler u On uk.UrunId = u.UrunId
                                                WHERE k.kategoriStatus = 1 
                                                group by k.kategoriId, k.kategoriIsim 
                                                order by k.kategoriId";
            ViewBag.KategoriSelectViewBag = _dal.CommandExecuteReader(KategoriSelectQuery, _dal.benimSqlBaglantim);

            return View();
        }
    }
}
