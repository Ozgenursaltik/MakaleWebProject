using Makale_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Makale_Entities;

namespace Makale_BLL
{
    public class YorumYonet
    {
        private Repository<Yorum> rep_yorum = new Repository<Yorum>();
        public Yorum YorumBul(int id)
        {
            return rep_yorum.Find(x => x.Id == id);
        }

        public int YorumUpdate(Yorum yorum)
        {
            return rep_yorum.Update(yorum);
        }

        public int YorumSil(Yorum yorum)
        {
            return rep_yorum.Delete(yorum);
        }

        public int YorumEkle(Yorum yorum)
        {
            return rep_yorum.Insert(yorum);
        }
    }
}
