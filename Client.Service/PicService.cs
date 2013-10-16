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
        public string Upload(Stream picStream)
        {
            MemoryStream s = new MemoryStream();
            picStream.CopyTo(s);
            byte[] t = s.ToArray();
            string token = PictureManager.Create(t);
            return token;
        }

        /// <summary>
        /// 上传并裁剪
        /// </summary>
        /// <param name="picStream">图片的流文件</param>
        /// <param name="poin_x">所要截图的起始点的x坐标</param>
        /// <param name="poin_y">所要截图的起始点的y坐标</param>
        /// <param name="width">所要截图的矩阵的宽</param>
        /// <param name="height">所要截图的矩阵的高</param>
        /// <returns>返回带图片的存储令牌的操作结果</returns>
        public string UploadAndCut(Stream picStream, int poin_x, int poin_y, int width, int height)
        {
            MemoryStream s = new MemoryStream();
            picStream.CopyTo(s);
            Bitmap img = new System.Drawing.Bitmap(s);
            Rectangle r = new System.Drawing.Rectangle(poin_x, poin_y, width, height);
            Bitmap _img = img.Clone(r, PixelFormat.Format32bppArgb);
            MemoryStream ss = new MemoryStream();
            _img.Save(ss, ImageFormat.Jpeg);
            byte[] t = ss.ToArray();
            string token = PictureManager.Create(t);
            return token;
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
