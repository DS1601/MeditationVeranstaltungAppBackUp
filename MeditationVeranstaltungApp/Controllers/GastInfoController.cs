using MeditationVeranstaltungApp.Data;
using MeditationVeranstaltungApp.Data.Migrations;
using MeditationVeranstaltungApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace MeditationVeranstaltungApp.Controllers
{
    public class GastInfoController : Controller
    {
        private readonly ApplicationDbContext context;

        public GastInfoController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var gastInfos = context.GastInfos.Include(q => q.Kontakt).Include(q => q.FahrerKontakt).ToList();
            ViewBag.GastInfos = gastInfos;
            return View();
        }

        public IActionResult CreateEditGastInfo(int id)
        {

            return View();
        }

        public IActionResult CreateEditGastInfoSubmit(GastInfoModel gastInfoModel)
        {
            if (gastInfoModel != null)
            {
                var gastInfo = new GastInfo
                {
                    Veranstalltung = gastInfoModel.Veranstalltung,
                    AnzahlMaenner = gastInfoModel.AnzahlMaenner,
                    AnzahlWeiblich = gastInfoModel.AnzahlWeiblich,
                    AnkunftAm = gastInfoModel.AnkunftAm.ToDateTime(gastInfoModel.AnkunftUm),
                    AnkunftOrt = gastInfoModel.AnkunftOrt,
                    AbfahrtAm = gastInfoModel.AbfahrtAm.ToDateTime(gastInfoModel.AbfahrtUm),
                    AbfahrtOrt = gastInfoModel.AbfahrtOrt,
                    Notiz = gastInfoModel.Notiz,
                    UserId= "93df0a45-7232-45e6-b882-8dcc0966ba8a"
                };



                if (gastInfoModel.Id != null)
                { //  Create

                    var kontakt = context.Kontakts
                        .Where(k => k.Vorname == gastInfoModel.Vorname &&
                        k.Nachname == gastInfoModel.Nachname &&
                        k.Telefon == gastInfoModel.Telefon &&
                        k.Stadt == gastInfoModel.Stadt &&
                        k.Land == gastInfoModel.Land
                        )
                        .FirstOrDefault();

                    if (kontakt != null)
                    {
                        gastInfo.KontaktId = kontakt.Id;
                    }
                    else {
                        gastInfo.Kontakt = new Kontakt
                        {
                            Anrede = gastInfoModel.Anrede,
                            Vorname = gastInfoModel.Vorname,
                            Nachname = gastInfoModel.Nachname,
                            Geschlecht = gastInfoModel.Geschlecht,
                            Email = gastInfoModel.Email,
                            Telefon = gastInfoModel.Telefon,
                            Stadt = gastInfoModel.Stadt,
                            Land = gastInfoModel.Land,
                        };
                       
                    }

                    context.Add( gastInfo );
                    context.SaveChanges();


                }
            }
            return RedirectToAction("Index");
        }


    }
}
