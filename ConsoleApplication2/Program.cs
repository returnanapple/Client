using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ConsoleApplication2.PicService;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            //FileStream fs = new FileStream("f:/0.jpg", FileMode.Open);
            //PicServiceClient client = new PicServiceClient();
            //var result = client.Upload(fs);
            //Console.WriteLine(result.Content);

            PicServiceClient client = new PicServiceClient();
            var t = client.Download("0416415f6f7f4ee6a37a077556dbb8cb");
            MemoryStream ts = new MemoryStream();
            t.CopyTo(ts);
            FileStream fs = new FileStream("E:/0.jpg", FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(ts.ToArray());
            fs.Flush();
            bw.Close();
        }
    }
}
