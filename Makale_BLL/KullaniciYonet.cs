using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Makale_Common;
using Makale_DAL;
using Makale_Entities;


namespace Makale_BLL
{
    public class KullaniciYonet
    {
        private Repository<Kullanici> repo_kul = new Repository<Kullanici>();
        BusinessLayerResult<Kullanici> kullanicisonuc = new BusinessLayerResult<Kullanici>();


        public BusinessLayerResult<Kullanici> KullaniciKaydet(RegisterViewModel model)
        {
            //Kullanıcı adı ve eposta kontrolü
            //Kayıt işlemi 
            //Aktivasyon maili gönder

            Kullanici kullanici = repo_kul.Find(x => x.KullaniciTakmaAdi == model.KullaniciTakmaAdi || x.Email == model.Email);
            
            if (kullanici!=null)
            {
                if (kullanici.KullaniciTakmaAdi == model.KullaniciTakmaAdi)
                {

                    kullanicisonuc.hata.Add("Kullanııcı adı kayıtlı");
                }
                if (kullanici.Email == model.Email)
                {
                    kullanicisonuc.hata.Add("E-posta adresi kayıtlı");                  
                }
            }
            else
            {
                int sonuc = repo_kul.Insert(new Kullanici() {
                    KullaniciTakmaAdi=model.KullaniciTakmaAdi,
                    Email=model.Email,
                    Sifre=model.Sifre,
                    AktifGuid=Guid.NewGuid(),
                    Aktif=false,
                    Admin=false
                    
                });
                if (sonuc>0)
                {
                    kullanicisonuc.Sonuc = repo_kul.Find(x => x.Email == model.Email && x.KullaniciTakmaAdi == model.KullaniciTakmaAdi);

                    string siteUrl = ConfigHelper.Get<string>("SiteRootUrl");
                    string activateUrl = $"{siteUrl}/Home/UserActivate/{kullanicisonuc.Sonuc.AktifGuid}";
                    string body = $"Merhaba{kullanicisonuc.Sonuc.KullaniciAdi}{kullanicisonuc.Sonuc.KullaniciSoyadi}<br> Hesabınızı aktifleştirmek için , <a href='{activateUrl}' target='_blank'> tıklayınız</a>";
                    // _blank:yeni sekmede aç
                    MailHelper.SendMail(body, kullanicisonuc.Sonuc.Email, "Hesap aktifleştirme");  


                    // Aktivasyon maili gönderilecek
                }
            }
            return kullanicisonuc;
        }

        public BusinessLayerResult<Kullanici> LoginKullanici(LoginViewModel model)
        {
            kullanicisonuc.Sonuc = repo_kul.Find(x => x.KullaniciTakmaAdi == model.KullaniciTakmaAdi && x.Sifre == model.Sifre);

            if (kullanicisonuc.Sonuc!=null)
            {
                if (!kullanicisonuc.Sonuc.Aktif)
                {
                    kullanicisonuc.hata.Add("Kullanıcı aktifleştirilmemiştir.Lütfen e-postanızı kontrol ediniz.");
                } 
            }
            else
            {
                kullanicisonuc.hata.Add("Kullanıcı adı ya da şifre uyuşmuyor.");
            }
            return kullanicisonuc;
        }

        public BusinessLayerResult<Kullanici> ActivateUser(Guid aktifGuid)
        {
            kullanicisonuc.Sonuc = repo_kul.Find(x => x.AktifGuid == aktifGuid);
            if (kullanicisonuc.Sonuc != null)
            {
                if (kullanicisonuc.Sonuc.Aktif)
                {
                    kullanicisonuc.hata.Add("Kullanıcı zaten aktif edilmiştir.");
                    return kullanicisonuc;
                }

                kullanicisonuc.Sonuc.Aktif = true;
                repo_kul.Update(kullanicisonuc.Sonuc);
            }
            return kullanicisonuc;
        }

        public BusinessLayerResult<Kullanici> KullaniciBul(int id)
        {
            kullanicisonuc.Sonuc = repo_kul.Find(x => x.Id == id);
            if (kullanicisonuc.Sonuc==null)
            {
                kullanicisonuc.hata.Add("Kullanıcı bulunamadı.");
            }
            return kullanicisonuc;
        }

        public BusinessLayerResult<Kullanici> KullaniciUpdate(Kullanici model)
        {
            Kullanici user = repo_kul.Find(x=>x.Id!=model.Id && (x.KullaniciTakmaAdi==model.KullaniciTakmaAdi || x.Email==model.Email));

            if (user!=null && user.Id!=model.Id)
            {
                if (user.KullaniciTakmaAdi==model.KullaniciTakmaAdi)
                {
                    kullanicisonuc.hata.Add("Bu kullanıcı adı kayıtlı.");
                }

                if (user.Email == model.Email)
                {
                    kullanicisonuc.hata.Add("Bu eposta kayıtlı.");
                }
                return kullanicisonuc;
            }
            kullanicisonuc.Sonuc = repo_kul.Find(X => X.Id == model.Id);
            kullanicisonuc.Sonuc.Email = model.Email;
            kullanicisonuc.Sonuc.KullaniciAdi = model.KullaniciAdi;
            kullanicisonuc.Sonuc.KullaniciSoyadi = model.KullaniciSoyadi;
            kullanicisonuc.Sonuc.KullaniciTakmaAdi = model.KullaniciTakmaAdi;
            kullanicisonuc.Sonuc.Sifre = model.Sifre;
            

            if (string.IsNullOrEmpty(model.ProfilResmi)==false)
            {
                kullanicisonuc.Sonuc.ProfilResmi = model.ProfilResmi;                
            }
            if (repo_kul.Update(kullanicisonuc.Sonuc)==0)
            {
                kullanicisonuc.hata.Add("Kullanıcı güncellenemedi.");
            }
            return kullanicisonuc;
        }

        public BusinessLayerResult<Kullanici> KullaniciSil(int id)
        {
            Kullanici user = repo_kul.Find(x => x.Id == id);

            if (user==null)
            {
                kullanicisonuc.hata.Add("Kullanıcı bulunamadı.");
            }
            else
            {
                if (repo_kul.Delete(user)==0)
                {
                    kullanicisonuc.hata.Add("Kullanıcı silinemedi.");
                }
            }
            return kullanicisonuc;
        }
    }
}
