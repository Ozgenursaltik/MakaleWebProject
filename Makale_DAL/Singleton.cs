using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_DAL
{
    public static class Singleton
    {
        private static DataBaseContext db;
        private static object lockobj = new object();

        public static DataBaseContext CreateContext()
        {
            lock(lockobj)
            {
                if(db==null)
                {
                db = new DataBaseContext();
                }
            }         
            return db;
        }
    }
}
