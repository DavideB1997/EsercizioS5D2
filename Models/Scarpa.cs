namespace EsercizioS5D2.Models
{
    public class Scarpa
    {
        public int IdScarpa { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }

        public int Prezzo { get; set; }

        public string ImgScarpa { get; set; }

        public string ImgAgg1 { get; set; }
        public string ImgAgg2 { get; set; }

        public DateTime? DeletedAt { get; set; } = null;
    }
}
