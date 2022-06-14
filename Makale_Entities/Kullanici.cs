using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entities
{
    [Table("Kullanicilar")]
    public class Kullanici:EntityBase
    {
        [StringLength(25)]
        public string KullaniciAdi { get; set; }
        [StringLength(25)]
        public string KullaniciSoyadi { get; set; }
        [Required,StringLength(25)]
        public string KullaniciTakmaAdi { get; set; }
        [Required,StringLength(50)]
        public string Email { get; set; }
        [Required, StringLength(25)]
        public string Sifre { get; set; }
        [StringLength(30)]
        public string ProfilResmi { get; set; }
        public bool Admin { get; set; }  //kullanıcı admin mi değil mi
        public bool Aktif { get; set; }
        [Required]
        public Guid AktifGuid { get; set; }


        public virtual List<Not> Makaleler { get; set; }
        public virtual List<Yorum> Yorumlar { get; set; }
        public virtual List<Begeni> Begeniler { get; set; }
    }
}
