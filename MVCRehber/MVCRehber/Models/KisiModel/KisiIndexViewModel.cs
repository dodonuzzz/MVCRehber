using MVCRehber.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCRehber.Models.KisiModel
{
    public class KisiIndexViewModel
    {
        public List<Kisi> Kisiler { get; set; }
        public List<Sehir> Sehirler { get; set; }
    }
}