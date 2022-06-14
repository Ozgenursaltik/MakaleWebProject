using Makale_DAL;
using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BLL
{
    public class Test
    {
        Repository<Kategori> repo_kat = new Repository<Kategori>();
        Repository<Kullanici> repo_kul = new Repository<Kullanici>();
        public Test()
        {
            //DataBaseContext db = new DataBaseContext();
            //db.Kullanicilar.ToList();

            List<Kategori> katlist = repo_kat.List();
            List<Kullanici> kullist = repo_kul.List();
        }

        public void InsertTest()
        {
            repo_kul.Insert(new Kullanici()
            {
                KullaniciAdi = "Betül",
                KullaniciSoyadi = "Keser",
                Email = "btl@hotmail.com",
                KullaniciTakmaAdi = "betül",
                Sifre = "123",
                Aktif = true,
                Admin = true,
                AktifGuid = Guid.NewGuid(),
                KayitTarihi = DateTime.Now,
                DegistirmeTarihi = DateTime.Now.AddMinutes(5),
                DegistirenKullanici = "betül"
            });
        }

        public void UpdateTest()
        {
            Kullanici kullanici = repo_kul.Find(x => x.KullaniciAdi == "betül");
            if (kullanici != null)
            {
                kullanici.KullaniciAdi = "Deniz";
                repo_kul.Save();
            }
        }

        public void DeleteTest()
        {
            Kullanici kullanici = repo_kul.Find(x => x.KullaniciAdi == "Deniz");
            if (kullanici != null)
            {
                repo_kul.Delete(kullanici);
            }
        }

        Repository<Yorum> repo_yorum = new Repository<Yorum>();
        Repository<Not> repo_not = new Repository<Not>();
        public void YorumTest()
        {
            Kullanici kullanici = repo_kul.Find(x => x.Id == 1);
            Not makale = repo_not.Find(x => x.Id == 3);
            Yorum yorum = new Yorum(){
                YorumText="Bu bir test yorumudur.",
                KayitTarihi=DateTime.Now,
                DegistirmeTarihi=DateTime.Now,
                DegistirenKullanici="gizem",
                Kullanici=kullanici,
                Makale=makale           
            };
            repo_yorum.Insert(yorum);
        }
    }
}
