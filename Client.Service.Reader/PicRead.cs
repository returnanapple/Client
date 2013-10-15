using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.Model;

namespace Client.Service.Reader
{
    public class PicRead
    {
        public byte[] ReadPic(string token)
        {
            using (Model2DataContext db = new Model2DataContext())
            {
                Picture pic = db.PondOfPicture.First(x => x.Token == token);
                return pic.Content;
            }
        }
    }
}
