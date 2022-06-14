using Makale_DAL;
using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BLL
{
    public class MakaleYonet
    {
        private Repository<Not> repo_not = new Repository<Not>();
        BusinessLayerResult<Not> not_result = new BusinessLayerResult<Not>();
        public List<Not> MakaleGetir()
        {
            return repo_not.List();
        }

        public IQueryable<Not> ListQeryable()
        {
            return repo_not.ListQeryable();
        }

        public Not NotBul(int id)
        {
            return repo_not.Find(x => x.Id == id);
        }

        public BusinessLayerResult<Not> NotKaydet(Not not)
        {
            not_result.Sonuc = repo_not.Find(x => x.MakaleBaslik == not.MakaleBaslik && x.KategoriID == not.KategoriID);
            if (not_result.Sonuc!=null)
            {
                not_result.hata.Add("Bu makale kayıtlı.");
            }
            else
            {
                Not n = new Not();
                n.Kullanici = not.Kullanici;
                n.KategoriID = not.KategoriID;
                n.MakaleBaslik = not.MakaleBaslik;
                n.MakaleIcerik = not.MakaleIcerik;
                n.Taslak = not.Taslak;
                n.DegistirenKullanici = not.Kullanici.KullaniciTakmaAdi;
                int sonuc=repo_not.Insert(n);
                if (sonuc==0)
                {
                    not_result.hata.Add("Makale kaydedilemedi.");
                }
                else
                {
                    not_result.Sonuc = n;
                }               
            }
            return not_result;
        }



        public BusinessLayerResult<Not> NotUpdate(Not not)
        {
            not_result.Sonuc = repo_not.Find(x => x.MakaleBaslik == not.MakaleBaslik && x.KategoriID == not.KategoriID && x.Id!=not.Id);
            if (not_result.Sonuc!=null)
            {
                not_result.hata.Add("Bu makale kayıtlı.");
            }
            else
            {
                not_result.Sonuc = repo_not.Find(x => x.Id == not.Id);
                not_result.Sonuc.KategoriID = not.KategoriID;
                not_result.Sonuc.MakaleBaslik = not.MakaleBaslik;
                not_result.Sonuc.MakaleIcerik = not.MakaleIcerik;
                not_result.Sonuc.Taslak = not.Taslak;
                not_result.Sonuc.DegistirenKullanici = not.DegistirenKullanici;

                int sonuc = repo_not.Update(not_result.Sonuc);
                if (sonuc == 0)
                {
                    not_result.hata.Add("Makale değiştirilemedi.");
                }
                else
                {
                    not_result.Sonuc = repo_not.Find(x => x.Id == not.Id);
                }
            }
            return not_result;
        }



        public BusinessLayerResult<Not> NotSil(int id)
        {
            Not not = repo_not.Find(x => x.Id == id);
            if (not!=null)
            {
                int sonuc = repo_not.Delete(not);
                if (sonuc==0)
                {
                    not_result.hata.Add("Makale silinemedi.");
                }
            }
            else
            {
                not_result.hata.Add("Makale bulunamadı.");
            }
            return not_result;
        }
    }
}
