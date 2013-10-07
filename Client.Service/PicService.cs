using Client.Service.DataContract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace Client.Service
{
    /// <summary>
    /// 图片服务
    /// </summary>
    public class PicService : IPicService
    {
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="picStream">图片的流文件</param>
        /// <returns>返回带图片的存储令牌的操作结果</returns>
        public OperatingResult<string> Upload(Stream picStream)
        {
            #region 检查/创建文件夹
            string dPath = string.Format("{0}/img", AppDomain.CurrentDomain.BaseDirectory);
            if (!Directory.Exists(dPath))
            {
                Directory.CreateDirectory(dPath);
            }
            #endregion
            Stream ts = new MemoryStream();
            picStream.CopyTo(ts);
            string token = Guid.NewGuid().ToString("N");
            string path = string.Format("{0}/img/{1}.jpg"
                , AppDomain.CurrentDomain.BaseDirectory
                , token);
            Image img = Image.FromStream(ts);
            img.Save(path, ImageFormat.Jpeg);
            return new OperatingResult<string>(token);
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="token">图片的存储令牌</param>
        /// <returns>返回图片的流文件</returns>
        public Stream Download(string token)
        {
            string path = string.Format("{0}/img/{1}.jpg"
                , AppDomain.CurrentDomain.BaseDirectory
                , token);
            FileStream fs = new FileStream(path, FileMode.Open);
            return fs;
        }
    }
}
