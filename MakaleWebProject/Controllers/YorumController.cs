using Makale_BLL;
using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MakaleWebProject.Controllers
{
    public class YorumController : Controller
    {
        // GET: Yorum
        public ActionResult YorumGoster(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MakaleYonet makaleYonet = new MakaleYonet();
            Not not = makaleYonet.NotBul(id.Value);
            if (not==null)
            {
                return HttpNotFound();
            }
            return PartialView("_PartialYorum",not.Yorumlar);
        }

        YorumYonet yorumyonet = new YorumYonet();
        [HttpPost]
        public ActionResult YorumUpdate(int? id,string text)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yorum yorum = yorumyonet.YorumBul(id.Value);

            if (yorum==null)
            {
                return new HttpNotFoundResult();
            }
            yorum.YorumText = text;

            if(yorumyonet.YorumUpdate(yorum)>0)
            {
                return Json(new { sonuc = true });
            }
            return Json(new { sonuc = false });
        }

        public ActionResult YorumSil(int? id, string text)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yorum yorum = yorumyonet.YorumBul(id.Value);

            if (yorum == null)
            {
                return new HttpNotFoundResult();
            }
            yorum.YorumText = text;

            if (yorumyonet.YorumSil(yorum) > 0)
            {
                return Json(new { sonuc = true },JsonRequestBehavior.AllowGet);
            }
            return Json(new { sonuc = false },JsonRequestBehavior.AllowGet);
        }

        MakaleYonet makaleyonet = new MakaleYonet();
        [HttpPost]
        public ActionResult YorumEkle(Yorum yorum,int? notid)
        {
            if (notid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Not not = makaleyonet.NotBul(notid.Value);

            if (not == null)
            {
                return new HttpNotFoundResult();
            }

            yorum.Makale = not;
            yorum.Kullanici = Session["login"] as Kullanici;

            if(yorumyonet.YorumEkle(yorum)>0)
            {
                return Json(new { sonuc = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { sonuc = false }, JsonRequestBehavior.AllowGet);
        }
    }
} 