using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.Model;

namespace Client.Model.Manager
{
    public class PictureManager
    {
        public static string Create(byte[] content)
        {
            using (Model2DataContext db = new Model2DataContext())
            {
                string token = Guid.NewGuid().ToString("N");
                Picture pic = new Picture(token, content);
                db.Pictures.Add(pic);
                db.SaveChanges();

                return token;
            }
        }
    }
}
