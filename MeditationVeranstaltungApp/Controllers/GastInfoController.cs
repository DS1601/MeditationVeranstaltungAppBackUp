using MeditationVeranstaltungApp.Data;
using MeditationVeranstaltungApp.Data.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var gastInfos = context.GastInfos.Include(q => q.Kontakt).ToList();
            ViewBag.GastInfos = gastInfos;
            return View();
        }
    }
}
