using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Makale_Entities;

namespace Makale_DAL
{
    public class DBInitializer:CreateDatabaseIfNotExists<DataBaseContext>
    {
        protected override void Seed(DataBaseContext context)
        {
            Kullanici admin = new Kullanici() {
                KullaniciAdi="Gizem",
                KullaniciSoyadi="Saltık",
                Email="gizems@gmail.com",
                Aktif=true,
                Admin=true,
                KullaniciTakmaAdi="gizem",
                Sifre="123456",
                AktifGuid=Guid.NewGuid(),
                KayitTarihi=DateTime.Now,
                ProfilResmi="user.png",
                DegistirmeTarihi=DateTime.Now.AddMinutes(5),
                DegistirenKullanici="gizem"       
            };
            context.Kullanicilar.Add(admin);
            context.SaveChanges();

            for (int i = 0; i < 9; i++)
            {
                Kullanici user = new Kullanici()
                {
                    KullaniciAdi = FakeData.NameData.GetFirstName(),
                    KullaniciSoyadi = FakeData.NameData.GetSurname(),
                    Email=FakeData.NetworkData.GetEmail(),
                    AktifGuid=Guid.NewGuid(),
                    Aktif=true,
                    Admin=false,
                    KullaniciTakmaAdi=$"user{i}",
                    Sifre="123456",
                    KayitTarihi=DateTime.Now.AddDays(-1),
                    ProfilResmi="user.png",
                    DegistirmeTarihi=DateTime.Now,
                    DegistirenKullanici=$"user{i}"
                };
                context.Kullanicilar.Add(user);
            }
            context.SaveChanges();

            List<Kullanici> klist = context.Kullanicilar.ToList();  //kullanıcıları çekmek için oluşturduk.
            //Kategoriler ekleniyor.
            for (int i = 0; i < 10; i++)
            {
                Kategori kategori = new Kategori()
                {
                    KategoriBaslik=FakeData.PlaceData.GetStreetName(),
                    Aciklama=FakeData.PlaceData.GetAddress(),
                    KayitTarihi=DateTime.Now,
                    DegistirmeTarihi=DateTime.Now,
                    DegistirenKullanici="gizem"
                };
                context.Kategoriler.Add(kategori);


                //Kategoriye makale ekleniyor.

                for (int j = 0; j < 6; j++)
                {
                    Not not = new Not()
                    {
                        MakaleBaslik = FakeData.NameData.GetCompanyName(),
                        MakaleIcerik = FakeData.TextData.GetSentences(3),
                        Taslak = false,
                        BegeniSayisi = FakeData.NumberData.GetNumber(1, 9),
                        Kategori = kategori,
                        KayitTarihi = DateTime.Now.AddDays(-2),
                        DegistirmeTarihi = DateTime.Now,
                        Kullanici = klist[j],
                        DegistirenKullanici= klist[j].KullaniciTakmaAdi
                    };
                    kategori.Makaleler.Add(not);  //Kategori.cs'de ctor. (Makaleler = new List<Not>();)


                    // Makaleye yorum ekleniyor.
                    for (int k = 0; k < 3; k++)
                    {
                        Yorum yorum = new Yorum()
                        {
                            YorumText=FakeData.TextData.GetSentence(),
                            KayitTarihi=DateTime.Now,
                            DegistirmeTarihi=DateTime.Now,
                            Kullanici=klist[FakeData.NumberData.GetNumber(1,9)],
                            DegistirenKullanici= klist[FakeData.NumberData.GetNumber(1, 9)].KullaniciTakmaAdi
                        };
                        not.Yorumlar.Add(yorum);  //Not.cs'de ctor. (Yorumlar = new List<Yorum>();)
                    }


                    //Makaleye beğeni ekleniyor
                    for (int x = 0; x < not.BegeniSayisi; x++)
                    {
                        Begeni begeni = new Begeni()
                        {
                            Kullanici=klist[FakeData.NumberData.GetNumber(1,9)],
                            Makale=not
                        };
                        not.Begeniler.Add(begeni);  //Not.cs'de ctor. (Begeniler = new List<Begeni>();)
                    }
                }             
            }
            context.SaveChanges();
        }
    }
}
