using MVCRehber.Models;
using MVCRehber.Models.Context;
using MVCRehber.Models.Entities;
using MVCRehber.Models.KisiModel;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace MVCRehber.Controllers
{
    public class KisiController : Controller
    {
        MvcRehberContext db = new MvcRehberContext();
        // GET: Kisi
        public ActionResult Index()
        {                       
                var model = new KisiIndexViewModel();
                model.Kisiler = db.Kisiler.ToList();
                model.Sehirler = db.Sehirler.ToList();

                return View(model);                        
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            var model = new KisiEkleViewModel
            {
                Kisi = new Kisi(),
                Sehirler = db.Sehirler.ToList()
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Ekle(Kisi kisi)
        {
            try
            {
                db.Kisiler.Add(kisi);
                db.SaveChanges();
                TempData["BasariliMesaj"] = "Ekleme İşlemi Başarılı";
            }
            catch (Exception)
            {
                TempData["HataMesaji"] = "Hata Oluştu Lütfen Yeniden Deneyin";
            }


            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            var kisi = db.Kisiler.Find(id);

            if (kisi == null)
            {
                TempData["HataMesaji"] = "Güncellenmek İstenen Kayıt Bulunamadı";
                return RedirectToAction("Index");
            }
            var model = new KisiGuncelleViewModel
            {
                Kisi = kisi,
                Sehirler = db.Sehirler.ToList()
            };

            ViewBag.Sehirler = new SelectList(db.Sehirler.ToList(), "SehirId", "SehirAdi");

            return View(model);
        }
        [HttpPost]
        public ActionResult Guncelle(Kisi kisi)
        {
            var eskiKisi = db.Kisiler.Find(kisi.Id);
            if (eskiKisi == null)
            {
                TempData["HataMesaji"] = "Güncellenmek İstenen Kayıt Bulunamadı";
                return RedirectToAction("Index");
            }

            eskiKisi.Ad = kisi.Ad;
            eskiKisi.Soyad = kisi.Soyad;
            eskiKisi.tel_no = kisi.tel_no;
            eskiKisi.email = kisi.email;
            eskiKisi.SehirId = kisi.SehirId;

            db.SaveChanges();

            TempData["BasariliMesaj"] = "Kişi Başarıyla Güncellendi";
            
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Detay(int id)
        {
            var kisi = db.Kisiler.Find(id);

            if (kisi == null)
            {
                TempData["HataMesaji"] = "Kişi Bulunamadı";
                return RedirectToAction("Index");
            }

            var model = new KisiDetayViewModel
            {
                Kisi = kisi,
                Sehirler = db.Sehirler.ToList()
            };
            return View(model);
        }

        public ActionResult Sil(int id) 
        {
            var kisi = db.Kisiler.Find(id);
            if (kisi == null)
            {
                TempData["HataMesaji"] = "Silinmek İstenen Kişi Bulunamadı";
                return RedirectToAction("Index");
            }
            db.Kisiler.Remove(kisi);
            db.SaveChanges();

            TempData["BasariliMesaj"] = "Kayıt Silindi";
            return RedirectToAction("Index");
        }

        //Mail Gönder kısmı çalışır vaziyettedir. Mail uygulamalarından kaynaklı hata alınabilir.

        public ActionResult MailGonder(int id)
        {
            var kisi = db.Kisiler.Find(id);

            if (kisi == null)
            {
                TempData["HataMesaji"] = "Kişi Bulunamadı";
                return RedirectToAction("Index");
            }
            return View(kisi);
        }
        
        
        [HttpPost]
        public ActionResult MailGonder(string MailAdres, string Baslik, string Mesaj)
        {        
            try
            {
                var gonderi_mail = new MailAddress("");
                var sifre = "";
                var alici_mail = new MailAddress(MailAdres);

                var smtp = new SmtpClient
                {
                    Host = "smtp.hotmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(gonderi_mail.Address,sifre)
                };

                using (var msg = new MailMessage(gonderi_mail, alici_mail)
                {
                    IsBodyHtml = true,
                    Subject = Baslik,
                    Body = Mesaj
                })
                {
                    smtp.Send(msg);
                }

                TempData["BasariliMesaj"] = "Mesajınız Başarıyla Gönderildi.";
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                TempData["HataMesaji"] = "Mesaj Gönderilemedi. Lütfen Tekrar Deneyiniz.";
                return RedirectToAction("Index");
            }
        }
    }
}