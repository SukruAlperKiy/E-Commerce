using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
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

        [HttpPost]
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
            //  Bu Kod eger 2 adet 'logo.png' isimli dosya yuklenirse biri digerini ezmesin diye 'Guid.NewGuid().ToString()' bu kodla dosyaya benzersiz isimler olusturur. Örnek: Kullanıcı bilgisayarından benim-profil-resmim.jpg dosyasını seçti. Kod çalışınca uzantiExtension değeri .jpg olur. dosyaIsmi değişkeninin son hali şu şekle dönüşür: 4a8b9c1d - 2e3f - 4a5b - 6c7d - 8e9f0a1b2c3d.jpg
                string dosyaIsmi = Guid.NewGuid().ToString() + uzantiExtension;

            //  bu kodda fotografin gidecegi dosya yolunu verdik
                string dosyaYolu = Path.Combine(_webHostiongEnviroment.WebRootPath, "Photos", "FooterLogo");

            //  bu Kod, eger dosya yolu yoksa onu olustur diyor. (olmasa da olur)
                if (Directory.Exists(dosyaYolu) == false)
                {
                    Directory.CreateDirectory(dosyaYolu);
                }
            //  bu kod dosyanin yolunu ve dosyanin tam ismini birlestirir. Ornek: dosyaYolu = C:\Projem\wwwroot\Photos\FooterLogo , dosyaIsmi = 4a8b9c1d...jpg tamYol = C:\Projem\wwwroot\Photos\FooterLogo\4a8b9c1d-2e3f-4a5b-6c7d-8e9f0a1b2c3d.jpg
                string tamYol = Path.Combine(dosyaYolu, dosyaIsmi);

            //  'FileStream' ve 'FileMode.Create' kodlari hard diskte (yukaridaki string tamYol) adresine ici bos bir dosya olusturur.
                using (var dosyaKaydetmeAkisi = new FileStream(tamYol, FileMode.Create))
                {
                //  Bu kod ise kullanicinin tarayicisindan gelen resim verilerini yukarida olusturdugumuz bos dosyanin icine yazar. Yani resmi kaydetme isini bu kod yapar.
                    resimFile.CopyTo(dosyaKaydetmeAkisi);
                }

            //  fotoUrl artik kullanicinin ekledigi resmin geldigi son nokta.
                fotoUrl = "/Photos/FooterLogo/" + dosyaIsmi;
            }

            string sqlUpdateGuncelle = @" 
                   Update FooterLogo 
                   set 
                   FooterLogoAltBaslik = @FooterLogoAltBaslik,
                   FooterLogoFotoUrl = @FooterLogoFotoUrl 
                   Where FooterLogoId = @FooterLogoId";

            using (SqlConnection baglanti = _dal.benimSqlBaglantim)
            using (SqlCommand emir = new SqlCommand(sqlUpdateGuncelle, baglanti))
            {
                emir.Parameters.AddWithValue("@FooterLogoId", FooterLogoId);
                emir.Parameters.AddWithValue("@FooterLogoAltBaslik", FooterLogoAltBaslik);
                emir.Parameters.AddWithValue("@FooterLogoFotoUrl", fotoUrl);

                baglanti.Open();
                emir.ExecuteNonQuery();

            }


            return RedirectToAction("Footer_Logo");
        }


        #endregion
    }
}
