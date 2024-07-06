using MeditationVeranstaltungApp.Data;
using MeditationVeranstaltungApp.Data.Migrations;
using MeditationVeranstaltungApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System;

namespace MeditationVeranstaltungApp.Controllers
{
    [Authorize]
    public class GastInfoController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;

        public GastInfoController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            var query = context.GastInfos.Include(g => g.Kontakt).Include(g => g.FahrerKontakt);
            if (User.IsInRole("Admin"))
            {
                var userId = userManager.GetUserId(HttpContext.User);
                var gastInfos = query.ToList();
                ViewBag.GastInfos = gastInfos;
            }
            else
            {
                var userId = userManager.GetUserId(HttpContext.User);
                var gastInfos = query.Where(g => g.UserId == userId).ToList();
                ViewBag.GastInfos = gastInfos;
            }

            return View();
        }

        public IActionResult CreateEditGastInfo(int id)
        {
            if (id == 0)
            {
                return View();
            }

            var gastInfoAusDB = context.GastInfos.Include(q => q.Kontakt).FirstOrDefault(g => g.Id == id);

            if (gastInfoAusDB == null)
            {
                return NotFound();
            }

            var gastInfoModel = new GastInfoModel
            {
                Veranstalltung = gastInfoAusDB.Veranstalltung,
                AnzahlMaenner = gastInfoAusDB.AnzahlMaenner,
                AnzahlWeiblich = gastInfoAusDB.AnzahlWeiblich,
                AnkunftAm = DateOnly.FromDateTime(gastInfoAusDB.AnkunftAm),
                AnkunftUm = TimeOnly.FromDateTime(gastInfoAusDB.AnkunftAm),
                AnkunftOrt = gastInfoAusDB.AnkunftOrt,
                AbfahrtAm = DateOnly.FromDateTime(gastInfoAusDB.AbfahrtAm),
                AbfahrtUm = TimeOnly.FromDateTime(gastInfoAusDB.AbfahrtAm),
                AbfahrtOrt = gastInfoAusDB.AbfahrtOrt,
                Notiz = gastInfoAusDB.Notiz,

                Anrede = gastInfoAusDB.Kontakt.Anrede,
                Vorname = gastInfoAusDB.Kontakt.Vorname,
                Nachname = gastInfoAusDB.Kontakt.Nachname,
                Geschlecht = gastInfoAusDB.Kontakt.Geschlecht,
                Email = gastInfoAusDB.Kontakt.Email,
                Telefon = gastInfoAusDB.Kontakt.Telefon,
                Stadt = gastInfoAusDB.Kontakt.Stadt,
                Land = gastInfoAusDB.Kontakt.Land,


            };

            return View("CreateEditGastInfo", gastInfoModel);
        }

        public IActionResult CreateEditGastInfoSubmit(GastInfoModel gastInfoModel)
        {
            if (gastInfoModel != null)
            {

                var gastInfo = new GastInfo();
                gastInfo.UserId = userManager.GetUserId(HttpContext.User);

                if (gastInfoModel.Id != 0)
                {
                    gastInfo = context.GastInfos.Include(q => q.Kontakt).FirstOrDefault(g => g.Id == gastInfoModel.Id);
                    if (gastInfo == null)
                    {
                        return NotFound();
                    }
                }


                gastInfo.Veranstalltung = gastInfoModel.Veranstalltung;
                gastInfo.AnzahlMaenner = gastInfoModel.AnzahlMaenner;
                gastInfo.AnzahlWeiblich = gastInfoModel.AnzahlWeiblich;
                gastInfo.AnkunftAm = gastInfoModel.AnkunftAm.ToDateTime(gastInfoModel.AnkunftUm);
                gastInfo.AnkunftOrt = gastInfoModel.AnkunftOrt;
                gastInfo. AbfahrtAm = gastInfoModel.AbfahrtAm.ToDateTime(gastInfoModel.AbfahrtUm);
                gastInfo.AbfahrtOrt = gastInfoModel.AbfahrtOrt;
                gastInfo.Notiz = gastInfoModel.Notiz;

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
                    gastInfo.Kontakt = kontakt;
                }
                else
                {
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

                if (gastInfoModel.Id == 0)
                {
                    context.GastInfos.Add(gastInfo);
                }
                else
                {
                    context.GastInfos.Update(gastInfo);
                    context.Entry(gastInfo).State = EntityState.Modified;

                }
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest();
            }


        }
        public IActionResult Absage(AbsageModel absageModel)
        {
            var gastInfo = context.GastInfos.FirstOrDefault(g => g.Id == absageModel.Id);
            gastInfo.AbgesagtAm = DateTime.Now;
            gastInfo.AbsageGrund = absageModel.AbsageGrund; context.GastInfos.Update(gastInfo);
            context.Entry(gastInfo).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult FahrerZuweisen(int id)
        {
            var fahrerZuweisenModel = new FahrerZuweisenModel
            {
                gastInfoId = id
            };
            return View("FahrerZuweisen", fahrerZuweisenModel);
        }

        public IActionResult FahrerZuweisenSubmit(FahrerZuweisenModel fahrerZuweisenModel)
        {
            var gastInfoAusDB = context.GastInfos.FirstOrDefault(g => g.Id == fahrerZuweisenModel.gastInfoId);

            var fahrerKontakt = context.Kontakts
                .Where(k => k.Vorname == fahrerZuweisenModel.Vorname &&
                k.Nachname == fahrerZuweisenModel.Nachname &&
                k.Telefon == fahrerZuweisenModel.Telefon &&
                k.Stadt == fahrerZuweisenModel.Stadt &&
                k.Land == fahrerZuweisenModel.Land
                ).FirstOrDefault();

            if (fahrerKontakt != null)
            {
                gastInfoAusDB.FahrerKontaktId = fahrerKontakt.Id;
                gastInfoAusDB.FahrerKontakt = fahrerKontakt;
            }
            else
            {
                gastInfoAusDB.FahrerKontakt = new Kontakt
                {
                    Anrede = fahrerZuweisenModel.Anrede,
                    Vorname = fahrerZuweisenModel.Vorname,
                    Nachname = fahrerZuweisenModel.Nachname,
                    Geschlecht = fahrerZuweisenModel.Geschlecht,
                    Email = fahrerZuweisenModel.Email,
                    Telefon = fahrerZuweisenModel.Telefon,
                    Stadt = fahrerZuweisenModel.Stadt,
                    Land = fahrerZuweisenModel.Land,
                };

            }


            context.Entry(gastInfoAusDB).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Details", gastInfoAusDB);
        }

        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return View();
            }

            var gastInfoAusDB = context.GastInfos.Include(q => q.Kontakt).Include(g => g.FahrerKontakt).FirstOrDefault(g => g.Id == id);

            if (gastInfoAusDB == null)
            {
                return NotFound();
            }

            ViewBag.GastInfo = gastInfoAusDB;
            return View();
        }
    }
}
