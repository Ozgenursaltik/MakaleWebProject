using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_Entities
{
    [Table("Begeniler")]
    public class Begeni
    {
        // Kim hangi makaleyi beğendi 
        [Key]
        public int BegeniID { get; set; }



        public virtual Not Makale { get; set; }
        public virtual Kullanici Kullanici { get; set; }
    }
}
