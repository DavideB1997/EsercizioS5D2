using System.Data;

namespace EsercizioS5D2.Models
{
    public class StaticDb
    {
        private static int _maxId = 0;
        private static List<Scarpa> _scarpe = new List<Scarpa>();


        public static List<Scarpa> GetAll()
        {
            List<Scarpa> notDeletedScarpe = [];
            foreach (var scarpa in _scarpe)
            {
                if(scarpa.DeletedAt == null)
                {
                    notDeletedScarpe.Add(scarpa);   
                }
            }
            return notDeletedScarpe;
        }




        public static Scarpa Add(string nome, string descrizione, int prezzo, string imgScarpa, string imgAgg1, string imgAgg2)
        {
            _maxId++;

            var scarpa = new Scarpa() { IdScarpa = _maxId, Nome = nome, Descrizione = descrizione, Prezzo = prezzo, ImgScarpa = imgScarpa, ImgAgg1 = imgAgg1, ImgAgg2 = imgAgg2 };

            _scarpe.Add(scarpa);

            return scarpa;
        }

        public static Scarpa? GetById(int? id)
        {
            if (id == null) return null;

            for (int i = 0; i < _scarpe.Count; i++)
            {
                Scarpa scarpa = _scarpe[i];
                if (scarpa.IdScarpa == id)
                    return scarpa;
            }

            return null;
        }

        public static Scarpa? Modify(Scarpa scarpa)
        {
            foreach (var scarpaInList in _scarpe)
            {
                if(scarpaInList.IdScarpa == scarpa.IdScarpa)
                {
                    scarpaInList.Nome = scarpa.Nome;
                    scarpaInList.Descrizione = scarpa.Descrizione;
                    scarpaInList.Prezzo = scarpa.Prezzo;
                    scarpaInList.ImgScarpa = scarpa.ImgScarpa;
                    scarpaInList.ImgAgg1= scarpa.ImgAgg1;
                    scarpaInList.ImgAgg2= scarpa.ImgAgg2;
                    return scarpaInList;
                }

            }
            return null;
        }

        public static Scarpa? SoftDelete(int idToDelete)
        {
            int? deletedIndex = findScarpaIndex(idToDelete);

            if(deletedIndex != null)
            {
                var deletedScarpa = _scarpe[(int)deletedIndex];
                deletedScarpa.DeletedAt = DateTime.UtcNow;
                return deletedScarpa;
            }

            return null;
        }





        public static Scarpa? HardDelete(int idToDelete)
        {
            int? deletedIndex = findScarpaIndex(idToDelete);

            if(deletedIndex != null)
            {
                var deletedScarpa = _scarpe[(int)deletedIndex];
                _scarpe.RemoveAt((int)deletedIndex);
                return deletedScarpa;
            }

            return null;
        }

        private static int? findScarpaIndex(int idToDelete)
        {
            int i;
            bool scarpaTrovata = false;
            for (i = 0; i < _scarpe.Count; i++)
            {
                if (_scarpe[i].IdScarpa == idToDelete)
                {
                    scarpaTrovata = true;
                    break;
                }
            }

            if (scarpaTrovata) return i;
            return null;
        }



        public static List<Scarpa> GetAllDeleted()
        {
            List<Scarpa> deletedScarpe = [];
            foreach(var scarpa in _scarpe)
            {
                if(scarpa.DeletedAt != null)
                {
                    deletedScarpe.Add(scarpa);
                }
            }

            return deletedScarpe; ;
        }


        public static Scarpa? Recover(int idToRecover)
        {
            int? index = findScarpaIndex((int)idToRecover);

            if (index != null)
            {
                var recuperataScarpa = _scarpe[(int) index];
                recuperataScarpa.DeletedAt = null;
                return recuperataScarpa;
            }

            return null;
        }
    }
}
