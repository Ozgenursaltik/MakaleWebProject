using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Makale_Entities
{
    [Table("Makaleler")]
    public class Not:EntityBase
    {
        [Required,StringLength(60)]
        public string MakaleBaslik { get; set; }
        [Required, StringLength(1000)]
        public string MakaleIcerik { get; set; }
        public bool Taslak { get; set; }
        public int BegeniSayisi { get; set; }
        public int KategoriID { get; set; }



        public virtual Kategori Kategori { get; set; }
        public virtual Kullanici Kullanici { get; set; }
        public virtual List<Yorum> Yorumlar { get; set; }
        public virtual List<Begeni> Begeniler { get; set; }


        public Not()
        {
            Yorumlar = new List<Yorum>();
            Begeniler = new List<Begeni>();
        }
    }
}
