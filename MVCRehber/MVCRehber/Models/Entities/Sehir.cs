using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCRehber.Models.Entities
{
    [Table("Sehirler")]
    public class Sehir
    {
        public int SehirId { get; set; }
        public string SehirAdi { get; set; }
        public string UlkeAdi { get; set; }
    }
}