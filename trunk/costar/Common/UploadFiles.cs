using System;
using System.Collections.Generic;
using System.Web;
using System.IO;

namespace Common
{
    public class UploadFiles
    {
        private int _maxFileSize;
        private string _fileExtensionType;
        private string _directory;
        private int _newFileName;
        private Random rd = new Random();

        /// <summary>
        /// 上传大小
        /// </summary>
        public int MaxFileSize
        {
            set { _maxFileSize = value; }
        }

        /// <summary>
        /// 上传类型
        /// </summary>
        public string FileExtensionType
        {
            set { _fileExtensionType = value; }
        }

        /// <summary>
        /// 上传目录
        /// </summary>
        public string Dircetory
        {
            set { _directory = value; }
        }

        /// <summary>
        /// 新文件名类型:1-原文件名,2-日期命名
        /// </summary>
        public int NewFileName
        {
            set { _newFileName = value; }
        }

        public UploadFiles()
        {
            _maxFileSize = 500;
            _fileExtensionType = ".gif.png.jpeg.jpg.rar.doc.ppt.xls.xlsx.pptx.docx";
            _directory = "/UploadFiles/" + DateTime.Now.ToString("yyyy-MM") + "/";
            _newFileName = 2;
        }

        /// <summary>
        /// 多文件上传(未测)
        /// </summary>
        /// <param name="postedFile"></param>
        /// <param name="c">分割符</param>
        /// <returns></returns>
        public string Upload(HttpFileCollection postedFile, char c)
        {
            string s = string.Empty;
            for (int i = 0; i < postedFile.Count; i++)
            {
                if (!string.IsNullOrEmpty(postedFile[i].FileName))
                {
                    s += Upload(postedFile[i]) + c;
                }
            }
            return s.TrimEnd(c);
        }

        public void UploadDel(string filePath)
        {
            string dir = HttpContext.Current.Request.PhysicalApplicationPath;//当前应用程序的根目录
            string savePath = dir + filePath;//文件全路径
            if (File.Exists(savePath))
            {
                File.Delete(savePath);//如果文件已经存在就将已存在的文件删除
            }
        }

        public string Upload(HttpPostedFile postedFile)
        {
            string fileName, fileExtension;
            fileName = Path.GetFileName(postedFile.FileName);
            fileExtension = Path.GetExtension(fileName).ToLower();

            //上传类型
            if (_fileExtensionType.IndexOf(fileExtension) < 0 || string.IsNullOrEmpty(fileExtension))
            {
                //MyCommon.Alert("上传类型不正确");
                return "1";
            }

            //上传大小
            if (Convert.ToDouble(postedFile.ContentLength / 1024) > _maxFileSize)
            {
                //MyCommon.Alert("图片限制" + _maxFileSize + "K");
                return "2";
            }

            //不存在目录则创建,需要服务器权限
            if (!Directory.Exists(HttpContext.Current.Request.MapPath(_directory)))
            {
                Directory.CreateDirectory(HttpContext.Current.Request.MapPath(_directory));
            }

            //上传后文件名
            string newFileName = string.Empty;
            switch (_newFileName)
            {
                case 1:
                    newFileName = fileName;
                    break;
                case 2:
                    newFileName = DateTime.Now.ToString("yyMMddHHmmss") + rd.Next(1000).ToString().PadLeft(4, '0') + fileExtension;
                    break;
            }

            postedFile.SaveAs(HttpContext.Current.Request.MapPath(_directory) + newFileName);
            return _directory + newFileName + "|" + postedFile.ContentLength / 1024;
        }
    }
}
