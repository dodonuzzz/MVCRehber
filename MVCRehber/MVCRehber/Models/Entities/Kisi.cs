using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCRehber.Models.Entities
{
    [Table("Kisiler")]
    public class Kisi
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        [DisplayName("Telefon Numarası")]
        public string tel_no { get; set; }
        [DisplayName("Mail Adresi")]
        public string email { get; set; }
        [DisplayName("Şehir")]
        public int SehirId { get; set; }
        public Sehir Sehir { get; set; }
    }
}