using MeditationVeranstaltungApp.Shared;
using System;
using System.Collections.Generic;

namespace MeditationVeranstaltungApp.Models
{
    public partial class GastInfoModel
    {
        public GastInfoModel()
        {
           
        }
        public int Id { get; set; }
        public string Veranstalltung = "SAS2024HH";
        public int AnzahlMaenner { get; set; }
        public int AnzahlWeiblich { get; set; }
        public DateOnly AnkunftAm { get; set; }
        public TimeOnly AnkunftUm { get; set; }
        public string AnkunftOrt { get; set; }
        public DateOnly AbfahrtAm { get; set; }
        public TimeOnly AbfahrtUm { get; set; }
        public string AbfahrtOrt { get; set; }
        public string? Notiz {  get; set; }

        public Anrede Anrede { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public Geschlecht Geschlecht { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Stadt { get; set; }
        public string Land { get; set; }

    }
}
