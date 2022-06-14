using Makale_DAL;
using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BLL
{
    public class KategoriYonet
    {
        private Repository<Kategori> repo_kat = new Repository<Kategori>();
        public List<Kategori> KategoriGetir()
        {
            return repo_kat.List();
        }

        public Kategori KategoriBul(int id)
        {
            return repo_kat.Find(x => x.Id == id);
        }


        BusinessLayerResult<Kategori> kategorisonuc = new BusinessLayerResult<Kategori>();
        public BusinessLayerResult<Kategori> KategoriKaydet(Kategori model)
        {
         
            Kategori kategori = repo_kat.Find(x => x.KategoriBaslik == model.KategoriBaslik);

            if (kategori != null)
            {               
                    kategorisonuc.hata.Add("Kategori adı kayıtlı");             
            }
            else
            {
                int sonuc = repo_kat.Insert(new Kategori()
                {
                    KategoriBaslik=model.KategoriBaslik,
                    Aciklama=model.Aciklama
                });              
            }
            return kategorisonuc;
        }



        public BusinessLayerResult<Kategori> KategoriUpdate(Kategori model)
        {
            Kategori kategori = repo_kat.Find(x => x.KategoriBaslik == model.KategoriBaslik && x.Id!=model.Id);

            if (kategori!=null)
            {
                kategorisonuc.hata.Add("Kategori adı kayıtlı.");
            }
            else
            {
                kategorisonuc.Sonuc = repo_kat.Find(x => x.Id == model.Id);
                kategorisonuc.Sonuc.KategoriBaslik = model.KategoriBaslik;
                kategorisonuc.Sonuc.Aciklama = model.Aciklama;

                int sonuc=repo_kat.Update(kategorisonuc.Sonuc);
                if (sonuc>0)
                {
                    kategorisonuc.Sonuc = repo_kat.Find(x => x.Id == model.Id);
                }
            }
            return kategorisonuc;
        }



        public BusinessLayerResult<Kategori> KategoriSil(int id)
        {
            Kategori kategori = repo_kat.Find(x => x.Id == id);

            //kategorinin notlarını bul
            //notların yorumlarını bul
            //notların like bul sil
            //notu sil
            //kategori sil

            if (kategori==null)
            {
                kategorisonuc.hata.Add("Kategori bulunamdı.");

            }
            int sonuc = repo_kat.Delete(kategori);
            return kategorisonuc;
        }

    }
}
