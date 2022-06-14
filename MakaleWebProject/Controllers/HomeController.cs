using Makale_BLL;
using Makale_Entities;
//using MakaleWebProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MakaleWebProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        MakaleYonet my = new MakaleYonet();
        KategoriYonet ky = new KategoriYonet();
        KullaniciYonet kulyonet = new KullaniciYonet();
        public ActionResult Index()
        {
            //Test test = new Test();
            //test.InsertTest();
            //test.UpdateTest();
            //test.DeleteTest();
            //test.YorumTest();
            
            return View(my.MakaleGetir().OrderByDescending(x=>x.DegistirmeTarihi).ToList());
        }

        public ActionResult Kategori(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Kategori kategori=ky.KategoriBul(id.Value);
            if (kategori==null)
            {
                return HttpNotFound();
            }
            return View("Index",kategori.Makaleler);
        }

        public ActionResult EnBegenilenler()
        {
            return View("Index", my.MakaleGetir().OrderByDescending(x => x.BegeniSayisi).ToList());
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Kullanici> sonuc = kulyonet.LoginKullanici(model);

                if (sonuc.hata.Count>0)
                {
                    sonuc.hata.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }
                Session["login"] = sonuc.Sonuc;
                return RedirectToAction("Index");
            }

            return View();
        }



        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Kullanici> result = kulyonet.KullaniciKaydet(model);

                if (result.hata.Count>0)
                {
                    result.hata.ForEach(x => ModelState.AddModelError("", x));
                        return View(model);
                }

                return RedirectToAction("RegisterOK");
            }
            return View();
        }

        public ActionResult RegisterOK()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult ProfilGoster()
        {
            Kullanici user = Session["login"] as Kullanici;
            BusinessLayerResult<Kullanici> result= kulyonet.KullaniciBul(user.Id);
            if (result.hata.Count>0)
            {
                //Hata sayfasına yönlendirilebilirsiniz
            }
            return View(result.Sonuc);
        }



        public ActionResult ProfilDuzenle()
        {
            Kullanici user = Session["login"] as Kullanici;
            BusinessLayerResult<Kullanici> result = kulyonet.KullaniciBul(user.Id);
            if (result.hata.Count > 0)
            {
                //Hata sayfasına yönlendirilebilirsiniz
            }
            return View(result.Sonuc);
        }




        [HttpPost]
        public ActionResult ProfilDuzenle(Kullanici model,HttpPostedFileBase profilresim)
        {
            ModelState.Remove("DegistirenKullanici");
            if (ModelState.IsValid)
            {
                if (profilresim!=null && (profilresim.ContentType=="image/jpeg" ||
                    profilresim.ContentType == "image/jpg" ||
                    profilresim.ContentType=="image/png"))
            {
                string dosyaadi = $"user_{model.Id}.{profilresim.ContentType.Split('/')[1]}";
                profilresim.SaveAs(Server.MapPath($"~/image/{dosyaadi}"));
                model.ProfilResmi = dosyaadi;
            }
            BusinessLayerResult<Kullanici> sonuc = kulyonet.KullaniciUpdate(model);

            if (sonuc.hata.Count>0)
            {
                for (int i = 0; i < sonuc.hata.Count; i++)
                  {
                      ModelState.AddModelError("", (sonuc.hata)[i]);
                  }
                return View(model);
            }
            Session["login"] = sonuc.Sonuc;
            return RedirectToAction("ProfilGoster");
            }
            return View(model);
        }


        public ActionResult ProfilSil()
        {
            Kullanici user = Session["login"] as Kullanici;
            BusinessLayerResult<Kullanici> sonuc = kulyonet.KullaniciSil(user.Id);

            if (sonuc.hata.Count>0)
            {
                return RedirectToAction("ProfilGoster");
            }

            Session.Clear();
            return RedirectToAction("Index");
        }


        public ActionResult UserActivate(Guid id)
        {
            BusinessLayerResult<Kullanici> sonuc = kulyonet.ActivateUser(id);

            if (sonuc.hata.Count>0)
            {
                TempData["error"] = sonuc.hata;
                return RedirectToAction("UserActivateError");
            }

            return RedirectToAction("UserActivateOK");
        }


        public ActionResult UserActivateError()
        {
            List<string> hatamesaj = null;
            if (TempData["error"]!=null)
            {
                hatamesaj= TempData["error"] as List<string>;
            }

            return View(hatamesaj);
        }


        public ActionResult UserActivateOK()
        {
            return View();
        }
    }
}