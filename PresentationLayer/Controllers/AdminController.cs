using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers
{
    public class AdminController : Controller
    {
        private readonly dal _dal;
        private readonly IWebHostEnvironment _webHostiongEnviroment;
        public AdminController(dal dal123, IWebHostEnvironment webHostEnvironment)
        {
            _dal = dal123;
            _webHostiongEnviroment = webHostEnvironment;
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

        public IActionResult Footer_Logo_Updates(int FooterLogoId, IFormFile resimFile, string FooterLogoAltBaslik, string mevcutFotoUrl)
        {
            if (string.IsNullOrEmpty(FooterLogoAltBaslik))
            {
                //  bu kodun amaci eger kullanici "FooterLogoAltBaslik" kismina birsey girmezse kod hataya gireceginden kullanicinin eskinden girdigi veriler kaybolmasin diye bir onceki verilerini yeniden viewbaga gonderiyoruz.
                string FooterLogoSql2 = $"Select FooterLogoId, FooterLogoFotoUrl, FooterLogoAltBaslik from FooterLogo where FooterLogoId = {FooterLogoId}";

                ViewBag.Hata = "Doldurulmasi Zorunlu Alanlar Bos Birakilamaz";
                return RedirectToAction("Footer_Logo_Update", new { id = FooterLogoId} );
            }

            string fotoUrl = mevcutFotoUrl;

            if (resimFile != null && resimFile.Length > 0)
            {
                // bu kod satiri eger resim uzantisi buyuk harflerle gelirse onu kucuk harflere ceviriyor. (.PNG, .JPEG) => (.png, .jpeg)
                string uzantiExtension = Path.GetExtension(resimFile.FileName).ToLower();

                // bu kod sadece ".jpg", ".jpeg", ".png", ".gif", ".webp" bu uzantilara izin veriyor. hackerler .exe, .php ile injection yapamasin diye.
                string[] izinVerilenUzantilar = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

                // yani izin verilmeyen bir uzanti gelirse,
                if (izinVerilenUzantilar.Contains(uzantiExtension) == false)
                {
                    //  bu kodun amaci eger kullanici "FooterLogoAltBaslik" kismina birsey girmezse kod hataya gireceginden kullanicinin eskinden girdigi veriler kaybolmasin diye bir onceki verilerini yeniden viewbaga gonderiyoruz.
                    string FooterLogoSql2 = $"Select FooterLogoId, FooterLogoFotoUrl, FooterLogoAltBaslik from FooterLogo where FooterLogoId = {FooterLogoId}";

                    ViewBag.Hata = "Geçersiz dosya formatı! Sadece JPG, PNG, WEBP yükleyebilirsiniz.";
                    return RedirectToAction("Footer_Logo_Update", new { id = FooterLogoId });
                }

                string dosyaIsmi = Guid.NewGuid().ToString() + uzantiExtension;

            //  bu kodda fotografin gidecegi dosya yolunu verdik
                string dosyaYolu = Path.Combine(_webHostiongEnviroment.WebRootPath, "wwwroot", "Photos", "FooterLogo");

            //  bu Kod, eger dosya yolu yoksa onu olustur diyor. (olmasa da olur)
                if (Directory.Exists(dosyaYolu) == false)
                {
                    Directory.CreateDirectory(dosyaYolu);
                }

                string tamYol = Path.Combine(dosyaYolu, dosyaIsmi);


                resimFile.CopyTo(tamYol);

                fotoUrl = "/wwwroot/Photos/FooterLogo/" + dosyaIsmi;
            }

            string sqlUpdateGuncelle = @" 
                   Update FooterLogo 
                   set 
                   FooterLogoAltBaslik = @FooterLogoAltBaslik,
                   FooterLogoFotoUrl = @FooterLogoFotoUrl 
                   Where FooterLogoId = @FooterLogoId";

            using (SqlCommand emir = new SqlCommand(sqlUpdateGuncelle, _dal.benimSqlBaglantim))
            {
                emir.Parameters.AddWithValue("@FooterLogoId", FooterLogoId);
                emir.Parameters.AddWithValue("@FooterLogoAltBaslik", FooterLogoAltBaslik);
                emir.Parameters.AddWithValue("@FooterLogoFotoUrl", resimFile);



                emir.ExecuteNonQuery();
            }


            return RedirectToAction("Footer_Logo");
        }


        #endregion
    }
}
