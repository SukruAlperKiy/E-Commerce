using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using System.Data;
namespace PresentationLayer.ViewComponents.HomeViewComponents
{
    public class _HomeNewArrivalsAreaSectionComponentPartial:ViewComponent
    {
        private readonly dal _dal;
        public _HomeNewArrivalsAreaSectionComponentPartial(dal dal123)
        {
            _dal = dal123;
        }
        public IViewComponentResult Invoke()
        {
            string SelectUrunlerQuery = "Select UrunFiyat, UrunIsim, UrunlerKapakFoto1 from Urunler";
            DataSet veriKumesi = _dal.CommandExecuteReader(SelectUrunlerQuery, _dal.benimSqlBaglantim);


            var urunlerListesi = new List<Urunler1Model>();

            if (veriKumesi.Tables.Count > 0 && veriKumesi.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow sira in veriKumesi.Tables[0].Rows)
                {
                    var urun = new Urunler1Model()
                    {
                        urunlerIsim = sira["UrunIsim"].ToString(),
                        urunlerFiyat = Convert.ToDecimal(sira["UrunFiyat"]),
                        urunlerKapakFoto = sira["UrunlerKapakFoto1"].ToString()
                    };
                    urunlerListesi.Add(urun);
                }
            }

            return View(urunlerListesi);
        }
    }
}
