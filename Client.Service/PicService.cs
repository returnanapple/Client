using Client.Service.DataContract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using Client.Model.Manager;
using Client.Service.Reader;

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
            MemoryStream s = new MemoryStream();
            picStream.CopyTo(s);
            byte[] t = s.ToArray();
            string token = PictureManager.Create(t);
            return new OperatingResult<string>(token);
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="token">图片的存储令牌</param>
        /// <returns>返回图片的流文件</returns>
        public Stream Download(string token)
        {
            byte[] t = new PicRead().ReadPic(token);
            Stream s = new MemoryStream(t);
            return s;
        }
    }
}
