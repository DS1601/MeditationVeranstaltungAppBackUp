using MeditationVeranstaltungApp.Shared;
using System;
using System.Collections.Generic;

namespace MeditationVeranstaltungApp.Models
{
    public partial class AbsageModel
    {
        public AbsageModel()
        {
           
        }
        public int Id { get; set; }
        
        public string? AbsageGrund {  get; set; }

    }
}
