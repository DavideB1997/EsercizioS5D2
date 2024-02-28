using EsercizioS5D2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EsercizioS5D2.Controllers
{
    public class ScarpaController : Controller
    {

        // get all

        [HttpGet]
        public IActionResult Index()
        {
            return View(StaticDb.GetAll());
        }



        //Aggiunta + Details


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details([FromRoute] int? id)
        {
            if (id.HasValue)
            {
                var scarpa = StaticDb.GetById(id);
                if (scarpa == null)
                {
                    return View("Error");
                }
                return View(scarpa);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public IActionResult Add(string nome, string descrizione, int prezzo, string imgScarpa, string imgAgg1, string imgAgg2)
        {
            var scarpa = StaticDb.Add(nome, descrizione, prezzo, imgScarpa, imgAgg1, imgAgg2);
            return RedirectToAction("Details", new { id = scarpa.IdScarpa });
        }

        //Modifica

        public IActionResult Edit([FromRoute] int? id)
        {
            if (id == null) return RedirectToAction("Index", "Home");

            var scarpa = StaticDb.GetById(id);
            if (scarpa == null) return View("Error");

            return View(scarpa);
        }



        [HttpGet]
        public IActionResult Edit(Scarpa scarpa)
        {
            var scarpaEdit = StaticDb.Modify(scarpa);
            if (scarpaEdit == null) return View("Error");

            return RedirectToAction("Details", new { id = scarpaEdit.IdScarpa });
        }

        // delete

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var scarpa = StaticDb.GetById(id);
            return View(scarpa);
        }

        [HttpPost]
        public IActionResult Delete(Scarpa scarpa)
        {
            var deletedScarpa = StaticDb.HardDelete(scarpa.IdScarpa);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SoftDelete(Scarpa scarpa)
        {
            var deletedScarpa = StaticDb.SoftDelete(scarpa.IdScarpa);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Cestino()
        {
            var deletedScarpa = StaticDb.GetAllDeleted();
            return View(deletedScarpa);
        }


        [HttpPost]
        public IActionResult Recover(Scarpa scarpa)
        {
            var recuperataScarpa = StaticDb.Recover(scarpa.IdScarpa);
            if(recuperataScarpa == null)
            {
                return RedirectToAction("Cestino");
            }
            return RedirectToAction("Details", new {id = recuperataScarpa.IdScarpa});
        }



    }



}
