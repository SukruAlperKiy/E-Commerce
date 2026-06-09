using EntityLayer.Concrete;

namespace PresentationLayer.Models
{
    public class FooterAllViewModel
    {
        public List<Footer> FooterListModel { get; set; }
        public List<FooterSol> FooterSolListModel { get; set; }

        public string FooterLogoFotoUrl { get; set; }
        public string FooterLogoAltBaslik { get; set; }

        public string kategoriIsim { get; set; }
        public string kategoriStatus { get; set; }

    }
}
