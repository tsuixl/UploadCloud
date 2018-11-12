using System.IO;
using Util;

namespace Common
{
    public class UploadFileInfo
    {
        public string LocalFileName { get; set; }
        public string UploadFileName { get; set; }

        public UploadFileInfo(string localFileName)
        {
            SetFileName (localFileName);
        }


        public void SetFileName (string localFileName)
        {
            LocalFileName = UploadUtil.NormalPath (localFileName);
        }


        public string GetName ()
        {
            return Path.GetFileName (LocalFileName);
        }


        public string GetFileNameWithoutExtension ()
        {
            return Path.GetFileNameWithoutExtension (LocalFileName);
        }


        public string CheckFile()
        {
            if (string.IsNullOrEmpty (LocalFileName))
            {
                return "文件路径异常!";
            }
            
            if (!File.Exists(LocalFileName))
            {
                return string.Format("本地文件不存在 [{0}]", LocalFileName);
            }

            return string.Empty;
        }

        public void InitUploadFileName (string rootLocalPath, string uploadPath)
        {   
            if (string.IsNullOrEmpty(uploadPath))
            {
                UploadFileName = LocalFileName.Replace (rootLocalPath, string.Empty);
            }
            else if (!string.IsNullOrEmpty(rootLocalPath))
            {
                UploadFileName = string.Format("{0}/{1}", uploadPath, LocalFileName.Replace (rootLocalPath, string.Empty)) ;
            }
            else
            {
                UploadFileName = uploadPath;
            }
            UploadFileName = UploadFileName.Replace("//", "/");
            Log.l (string.Format("UploadFileName {0}", UploadFileName));
        }

    }
}

