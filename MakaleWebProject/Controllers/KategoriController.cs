using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
//using MakaleWebProject.Data;
using Makale_Entities;
using Makale_BLL;

namespace MakaleWebProject.Controllers
{
    public class KategoriController : Controller
    {

        // GET: Kategori
        KategoriYonet kategoriYonet = new KategoriYonet();

        public ActionResult Index()
        {
            return View(kategoriYonet.KategoriGetir());
        }

        // GET: Kategori/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = kategoriYonet.KategoriBul(id.Value);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        // GET: Kategori/Create
        public ActionResult Create()
        {
            return View();
        }

        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Kategori kategori)
        {
            ModelState.Remove("KayitTarihi");
            ModelState.Remove("DegistirmeTarihi");
            ModelState.Remove("DegistirenKullanici");

            if (ModelState.IsValid)
            {
                BusinessLayerResult<Kategori> sonuc = kategoriYonet.KategoriKaydet(kategori);

                if (sonuc.hata.Count>0)
                {
                    sonuc.hata.ForEach(x=>ModelState.AddModelError("",x));
                    return View(kategori);
                }                
                return RedirectToAction("Index");
            }
            return View(kategori);
        }

        // GET: Kategori/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = kategoriYonet.KategoriBul(id.Value);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Kategori kategori)
        {
            ModelState.Remove("KayitTarihi");
            ModelState.Remove("DegistirmeTarihi");
            ModelState.Remove("DegistirenKullanici");

            if (ModelState.IsValid)
            {
                BusinessLayerResult<Kategori> sonuc = kategoriYonet.KategoriUpdate(kategori);

                if (sonuc.hata.Count > 0)
                {
                    sonuc.hata.ForEach(x => ModelState.AddModelError("", x));
                    return View(kategori);
                }
                return RedirectToAction("Index");
            }
            return View(kategori);
        }

        // GET: Kategori/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = kategoriYonet.KategoriBul(id.Value);
            if (kategori==null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        // POST: Kategori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            //Kategori sil olacak
            Kategori kategori = kategoriYonet.KategoriBul(id);         
            BusinessLayerResult<Kategori> sonuc = kategoriYonet.KategoriSil(kategori.Id);
            {
                if (sonuc.hata.Count>0)
                {
                    return View(kategori);
                }
                return RedirectToAction("Index");
            }
            
        }
    }
}
