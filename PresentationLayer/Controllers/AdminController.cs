using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using PresentationLayer.Models;
using System.Data;

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

        [HttpPost]  // buradaki parametreler "FooterLogoIdParametre vb." Footer_Logo_Update controllerinin viewinden geliyor.
        public IActionResult Footer_Logo_Updates(int FooterLogoIdParametre, IFormFile resimFileParametre, string FooterLogoAltBaslikParametre, string mevcutFotoUrlParametre)
        {
            if (string.IsNullOrEmpty(FooterLogoAltBaslikParametre))
            {
                //  bu kodun amaci eger kullanici "FooterLogoAltBaslik" kismina birsey girmezse kod hataya gireceginden kullanicinin eskinden girdigi veriler kaybolmasin diye bir onceki verilerini yeniden viewbaga gonderiyoruz.
                string FooterLogoSql2 = $"Select FooterLogoId, FooterLogoFotoUrl, FooterLogoAltBaslik from FooterLogo where FooterLogoId = {FooterLogoIdParametre}";

                ViewBag.Hata = "Doldurulmasi Zorunlu Alanlar Bos Birakilamaz";
                return RedirectToAction("Footer_Logo_Update", new { id = FooterLogoIdParametre} );
            }

            string fotoUrl = mevcutFotoUrlParametre;

            if (resimFileParametre != null && resimFileParametre.Length > 0)
            {
                // bu kod satiri eger resim uzantisi buyuk harflerle gelirse onu kucuk harflere ceviriyor. (.PNG, .JPEG) => (.png, .jpeg)
                string uzantiExtension = Path.GetExtension(resimFileParametre.FileName).ToLower();

                // bu kod sadece ".jpg", ".jpeg", ".png", ".gif", ".webp" bu uzantilara izin veriyor. hackerler .exe, .php ile injection yapamasin diye.
                string[] izinVerilenUzantilar = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

                // yani izin verilmeyen bir uzanti gelirse,
                if (izinVerilenUzantilar.Contains(uzantiExtension) == false)
                {
                    //  bu kodun amaci eger kullanici "FooterLogoAltBaslik" kismina birsey girmezse kod hataya gireceginden kullanicinin eskinden girdigi veriler kaybolmasin diye bir onceki verilerini yeniden viewbaga gonderiyoruz.
                    string FooterLogoSql2 = $"Select FooterLogoId, FooterLogoFotoUrl, FooterLogoAltBaslik from FooterLogo where FooterLogoId = {FooterLogoIdParametre}";

                    ViewBag.Hata = "Geçersiz dosya formatı! Sadece JPG, PNG, WEBP yükleyebilirsiniz.";
                    return RedirectToAction("Footer_Logo_Update", new { id = FooterLogoIdParametre });
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
                    resimFileParametre.CopyTo(dosyaKaydetmeAkisi);
                }

            //  fotoUrl artik kullanicinin ekledigi resmin geldigi son nokta.
                fotoUrl = "/Photos/FooterLogo/" + dosyaIsmi;
            }

            string sqlUpdateGuncelle = @" 
                   Update FooterLogo 
                   set 
                   FooterLogoAltBaslik = @AltBaslikBoslugu,
                   FooterLogoFotoUrl = @LogoFotoUrlBoslugu 
                   Where FooterLogoId = @idBoslugu";

       //  Bu kodda "SqlConnection baglanti" isminde bir sqlconnection variablesi acmamizin sebebi. her "_dal.benimSqlBaglantim" dedigimiz yerde yeni bir SqlConnection acmasi.
            using (SqlConnection baglanti = _dal.benimSqlBaglantim) 
            {
                using (SqlCommand emir = new SqlCommand(sqlUpdateGuncelle, baglanti))
                {
                    emir.Parameters.AddWithValue("@idBoslugu", FooterLogoIdParametre);
                    emir.Parameters.AddWithValue("@AltBaslikBoslugu", FooterLogoAltBaslikParametre);
                    emir.Parameters.AddWithValue("@LogoFotoUrlBoslugu", fotoUrl);

                    baglanti.Open();

                    emir.ExecuteNonQuery();
                }
            }
            
            return RedirectToAction("Footer_Logo");
        }
        #endregion

        #region Categories

        public IActionResult kategorilerSelect()
        {
            string kategorilerSql = "Select kategoriId, kategoriIsim, kategoriStatus From Kategoriler";
            ViewBag.KategorilerViewBag = _dal.CommandExecuteReader(kategorilerSql,_dal.benimSqlBaglantim);

            return View();
        }

        public IActionResult kategorilerUpdate(int id)
        {
            string kategorilerUpdateSql = $"Select kategoriId,kategoriIsim,kategoriStatus from Kategoriler where kategoriId = {id}";
            ViewBag.KategorilerUpdateViewBag = _dal.CommandExecuteReader(kategorilerUpdateSql, _dal.benimSqlBaglantim);

            return View();
        }

        [HttpPost]
        public IActionResult kategorilerUpdatePost(int KategoriIdParametre, string KategoriIsmiParametre, int KategoriStatusParametre)
        {
            if (string.IsNullOrEmpty(KategoriIsmiParametre))
            {
                string bosKontrol = $"Select kategoriId, kategoriIsim, kategoriStatus from Kategoriler where kategoriId = {KategoriIdParametre}";
                ViewBag.KategorilerUpdateViewBag = _dal.CommandExecuteReader(bosKontrol,_dal.benimSqlBaglantim);

                ViewBag.Hata = "Doldurulmasi Zorunlu Alanlar Bos Birakilamaz";
                return View("kategorilerUpdate");
            }

            string sqlUpdateKategoriler = @"
            Update kategoriler
            set
            kategoriIsim = @kategoriIsimTutucu,
            kategoriStatus = @kategoriStatusTutucu 
            where 
            kategoriId = @kategoriIdTutucu";

            using (SqlConnection baglanti = _dal.benimSqlBaglantim)
            {
                using (SqlCommand emir = new SqlCommand(sqlUpdateKategoriler,baglanti))
                {
                    emir.Parameters.AddWithValue("@kategoriIdTutucu", KategoriIdParametre);
                    emir.Parameters.AddWithValue("@kategoriIsimTutucu", KategoriIsmiParametre);
                    emir.Parameters.AddWithValue("@kategoriStatusTutucu", KategoriStatusParametre);

                    baglanti.Open();

                    emir.ExecuteNonQuery();
                }
            }
                return RedirectToAction("kategorilerSelect");
        }
        [HttpGet]
        public IActionResult CreateCategories()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategories(string kategoriIsimParametre, int kategoriStatusParametre)
        {
            string CreateCategoriesQuery = @"Insert into Kategoriler
            (kategoriIsim, kategoriStatus) 
            values 
            (@kategoriIsimYerTurucu, @kategoriStatusYerTutucu)";

            using (SqlConnection baglanti = _dal.benimSqlBaglantim) 
            {
                using (SqlCommand emir = new SqlCommand(CreateCategoriesQuery, baglanti))
                {
                    emir.Parameters.AddWithValue("@kategoriIsimYerTurucu", kategoriIsimParametre);
                    emir.Parameters.AddWithValue("@kategoriStatusYerTutucu", kategoriStatusParametre);

                    baglanti.Open();
                    emir.ExecuteNonQuery();
                }
            }
            return RedirectToAction("kategorilerSelect");
        }

        public IActionResult DeleteCategories(int id)
        {
            string DeleteKategoriQuery = $"Delete from Kategoriler where kategoriId = @kategoriIdYerTutucu; Delete From UrunKategori where kategoriId = @kategoriIdYerTutucu";

            using (SqlConnection baglanti = _dal.benimSqlBaglantim)
            {
                using (SqlCommand emir = new SqlCommand(DeleteKategoriQuery, baglanti))
                {
                    emir.Parameters.AddWithValue("@kategoriIdYerTutucu", id);

                    baglanti.Open();
                    emir.ExecuteNonQuery();
                }
            }

                return RedirectToAction("kategorilerSelect");
        }

        #endregion

        #region Urunler
        public IActionResult UrunlerSelect()
        {
            string urunSelectSql = "Select UrunId, UrunFiyat, UrunIsim, UrunAciklama, UrunlerKapakFoto1, UrunStatus from Urunler";
            ViewBag.UrunSelectViewBag = _dal.CommandExecuteReader(urunSelectSql, _dal.benimSqlBaglantim);

            return View();
        }

        public IActionResult UrunlerUpdateGet(int id)
        {
            string urunlerGetSql = $"Select UrunId, UrunFiyat, UrunIsim, UrunAciklama, UrunStatus, UrunlerKapakFoto1 from Urunler where UrunId = {id}; select UrunKategoriId, UrunId, kategoriId from UrunKategori where UrunId = {id}; Select kategoriId, kategoriIsim, kategoriStatus from Kategoriler";
            ViewBag.UrunUpdateGetViewBag = _dal.CommandExecuteReader(urunlerGetSql, _dal.benimSqlBaglantim);
            return View();
        }

        [HttpPost]
        public IActionResult UrunlerUpdatePost(int UrunIdParametre, decimal UrunFiyatParametre, string UrunIsimParametre, string UrunAciklamaParametre, int UrunStatusParametre, int[] KategoriIdParametre, IFormFile UrunKapakFotoUrlParametre, string mevcutFotografParametre)
        {
            if (UrunFiyatParametre <= 0 || 
                string.IsNullOrEmpty(UrunIsimParametre) ||
                string.IsNullOrEmpty(UrunAciklamaParametre))
            {
                string UrunlerUpdateOncesi = $"Select UrunFiyat, UrunIsim, UrunAciklama, UrunStatus, UrunlerKapakFoto1 from Urunler where UrunId = {UrunIdParametre}";
                ViewBag.UrunlerUpdateViewBag = _dal.CommandExecuteReader(UrunlerUpdateOncesi, _dal.benimSqlBaglantim);

                return View("UrunlerUpdateGet");
            }

            string fotoUrl = mevcutFotografParametre;

            if (UrunKapakFotoUrlParametre != null && UrunKapakFotoUrlParametre.Length > 0)
            {
                string uzantiExtension = Path.GetExtension(UrunKapakFotoUrlParametre.FileName).ToLower();

                string[] izinVerilenUzantilar = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

                if (izinVerilenUzantilar.Contains(uzantiExtension) == false)
                {
                    ViewBag.Error = "Sadece jpg, jpeg, png, gif, webp yüklenebilir.";
                    return View();
                }

                string dosyaIsmi = Guid.NewGuid().ToString() + uzantiExtension;

                string dosyaYolu = Path.Combine(_webHostiongEnviroment.WebRootPath, "Photos", "UrunKapak");

                string tamYol = Path.Combine(dosyaYolu, dosyaIsmi);

                using (var dosyaKaydetmeAkisi = new FileStream(tamYol, FileMode.Create))
                {
                    UrunKapakFotoUrlParametre.CopyTo(dosyaKaydetmeAkisi);
                }

                fotoUrl = "/Photos/UrunKapak/" + dosyaIsmi;
            }

            string UrunlerUpdateSql = @"
            Update Urunler set
            UrunFiyat = @UrunFiyatTutucu,
            UrunIsim = @UrunIsimTutucu,
            UrunAciklama = @UrunAciklamaTutucu,
            UrunStatus = @UrunStatusTutucu,
            UrunlerKapakFoto1 = @UrunlerKapakFoto1Tutucu
            where UrunId = @UrunIdTutucu";

            string UrunKategoriEkleQuery = $@"
            Insert into UrunKategori 
            (UrunId, kategoriId) 
            Values 
            (@UrunIdTutucu, @kategoriIdTutucu)";

            string UrunKategoriSilQuery = $@"
            Delete from UrunKategori 
            where 
            UrunId = @UrunIdTutucu";


            using (SqlConnection baglanti = _dal.benimSqlBaglantim)
            {
//  eger bu sekilde birden fazla emir(sqlCommand) varsa baglantiyi en tepede acman gerekiyor.
                baglanti.Open();

                using (SqlCommand emirUrunEkle = new SqlCommand(UrunlerUpdateSql,baglanti))
                {
                    emirUrunEkle.Parameters.AddWithValue("@UrunFiyatTutucu",UrunFiyatParametre);
                    emirUrunEkle.Parameters.AddWithValue("@UrunIsimTutucu", UrunIsimParametre);
                    emirUrunEkle.Parameters.AddWithValue("@UrunAciklamaTutucu", UrunAciklamaParametre);
                    emirUrunEkle.Parameters.AddWithValue("@UrunStatusTutucu", UrunStatusParametre);
                    emirUrunEkle.Parameters.AddWithValue("@UrunlerKapakFoto1Tutucu", fotoUrl);
                    emirUrunEkle.Parameters.AddWithValue("@UrunIdTutucu", UrunIdParametre);
                    
                    emirUrunEkle.ExecuteNonQuery();
                }

            //  hem silineceklerin hem de ekleneceklerin "id"si ayni parametreden geldiginden.
            //  biz secilen urunun once tum idlerini siliyoruz. sonra parametreden gelen tum "id"leri geri yukluyoruz.
                using (SqlCommand emirUrundenKategoriSil = new SqlCommand(UrunKategoriSilQuery, baglanti))
                {
                    emirUrundenKategoriSil.Parameters.AddWithValue("@UrunIdTutucu", UrunIdParametre);
                    emirUrundenKategoriSil.ExecuteNonQuery();
                }

                foreach (var eklenecekKategoriId in KategoriIdParametre)
                {
                    using (SqlCommand emirUruneKategoriEkle = new SqlCommand(UrunKategoriEkleQuery, baglanti))
                    {
                    
                        emirUruneKategoriEkle.Parameters.AddWithValue("@kategoriIdTutucu", eklenecekKategoriId);
                        emirUruneKategoriEkle.Parameters.AddWithValue("@UrunIdTutucu", UrunIdParametre);

                        emirUruneKategoriEkle.ExecuteNonQuery();
                    }
                }
            }
                return RedirectToAction("UrunlerSelect");
        }

        [HttpGet]
        public IActionResult CreateUrun()
        {
            string KategorilerSelectQuery = @"Select kategoriId, kategoriIsim, kategoriStatus from Kategoriler";
            ViewBag.KategorilerSelectViewBag = _dal.CommandExecuteReader(KategorilerSelectQuery, _dal.benimSqlBaglantim);

            return View();
        }

        [HttpPost]
        public IActionResult CreateUrun(decimal UrunFiyatParametre, string UrunIsimParametre, string UrunAciklamaParametre, int UrunStatusParametre, int[] KategoriIdParametre, IFormFile UrunKapakFotoUrlParametre)
        {
            string fotoUrl = null;

            if (UrunKapakFotoUrlParametre != null && UrunKapakFotoUrlParametre.Length > 0)
            {
                string uzantiExtension = Path.GetExtension(UrunKapakFotoUrlParametre.FileName).ToLower();

                string[] izinVerilenUzantilar = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

                if (izinVerilenUzantilar.Contains(uzantiExtension) == false)
                {
                    ViewBag.Error = "Sadece jpg, jpeg, png, gif, webp yüklenebilir.";
                    return View();
                }

                string dosyaIsmi = Guid.NewGuid().ToString() + uzantiExtension;

                string dosyaYolu = Path.Combine(_webHostiongEnviroment.WebRootPath, "Photos", "UrunKapak");

                string tamYol = Path.Combine(dosyaYolu, dosyaIsmi);

                using (var dosyaKaydetmeAkisi = new FileStream(tamYol, FileMode.Create))
                {
                    UrunKapakFotoUrlParametre.CopyTo(dosyaKaydetmeAkisi);
                }

                fotoUrl = "/Photos/UrunKapak/" + dosyaIsmi;
            }

            //  "DECLARE @YeniUrunId int = SCOPE_IDENTITY();" Urun olustururken urune id vermedigimizden cekebilecegimiz bir id yok. bu yuzden bu kod databaseye verecegimiz id'yi kendisi hepsalayip @YeniUrunId kismina yaziyor.
            string UrunEklemeQuery = @$"Insert into Urunler 
            (UrunFiyat, UrunIsim, UrunAciklama, UrunStatus, UrunlerKapakFoto1) 
            VALUES 
            (@UrunFiyatYerTutucu, @UrunIsimYerTutucu,@UrunAciklamaYerTutucu, @UrunStatusYerTutucu, @UrunKapakYerTutucu);
            
            Select SCOPE_IDENTITY();";

            int yeniUrunId;

            using (SqlConnection baglanti = _dal.benimSqlBaglantim)
            {
                using (SqlCommand emir = new SqlCommand(UrunEklemeQuery, baglanti))
                {// burada "@UrunFiyatYerTutucu" basindaki "@" Koymak zorundasin. yoksa okuyamiyor.
                    emir.Parameters.AddWithValue("@UrunFiyatYerTutucu", UrunFiyatParametre);
                    emir.Parameters.AddWithValue("@UrunIsimYerTutucu", UrunIsimParametre);
                    emir.Parameters.AddWithValue("@UrunAciklamaYerTutucu", UrunAciklamaParametre);
                    emir.Parameters.AddWithValue("@UrunStatusYerTutucu", UrunStatusParametre);
                    emir.Parameters.AddWithValue("@UrunKapakYerTutucu", fotoUrl);

                    baglanti.Open();
                    //  bu "emir.ExecuteScalar" kodu sqlde son yapilan islemin sonucunu getir diyormus.
                    //  SQL'de son yaptigimiz islem de "SELECT SCOPE_IDENTITY()" oldugundan bize yeni olusturulan urunun UrunId'sini getirdi.
                    yeniUrunId = Convert.ToInt32(emir.ExecuteScalar());
                }

                string KategoriIdEkleQuery = $@"Insert Into UrunKategori
                (UrunId, kategoriId)
                values 
                (@yeniUrunIdYerTutucu, @kategoriIdYerTutucu)";

                foreach (int eklenenKategoriId in KategoriIdParametre)
                {
                    using (SqlCommand emir2 = new SqlCommand(KategoriIdEkleQuery, baglanti))
                    {
                        emir2.Parameters.AddWithValue("@yeniUrunIdYerTutucu", yeniUrunId);
                        emir2.Parameters.AddWithValue("@kategoriIdYerTutucu", eklenenKategoriId);
                        emir2.ExecuteNonQuery();
                    }
                }
            }
                return RedirectToAction("UrunlerSelect");
        }

        public IActionResult UrunDelete(int id)
        {
            string UrunSilmeQuery = $"Delete from Urunler where UrunId = @idTutucu; Delete from UrunKategori where UrunId = @idTutucu";

            using (SqlConnection baglanti = _dal.benimSqlBaglantim)
            {
                using (SqlCommand emir = new SqlCommand(UrunSilmeQuery, baglanti)) 
                {
                    emir.Parameters.AddWithValue("@idTutucu", id);

                    baglanti.Open();
                    emir.ExecuteNonQuery();
                } 
            }

                return RedirectToAction("UrunlerSelect");
        }

        #endregion

    }
}
