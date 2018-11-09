using System.IO;
using System.Collections.Generic;
using Util;

namespace Common
{
    public class UploadAgentBase
    {

        public ArgsInit ArgsData
        {
            get;
            set;
        }

        public virtual object GetCtl () { return null; }

        public virtual object GetCustomInfo () { return null; }

        public virtual void OnUploadFinish (UploadFileInfo fileInfo, string error) {}

        public virtual void StartUpload (List<string> fileOrFolders) {
            UploadUtil.NormalPaths (fileOrFolders);
        }


        public UploadAgentBase (ArgsInit data)
        {
            ArgsData = data;
        }

        public virtual List<UploadFileInfo> GetUploadFilesByFileOrFolder (List<string> fileOrFolders)
        {
            List <UploadFileInfo> uploadFiles = new List<UploadFileInfo>();
            foreach (var item in fileOrFolders)
            {
                if (Directory.Exists(item))
                {
                    //  目录
                    var files = Directory.GetFiles (item, "*.*", SearchOption.AllDirectories);
                    foreach (var f1 in files)
                    {
                        var fileInfo  = new UploadFileInfo(f1);
                        fileInfo.InitUploadFileName (ArgsData.LocalRootPath, ArgsData.UploadFolder);
                        uploadFiles.Add (fileInfo);
                    }
                }
                else if (File.Exists(item))
                {
                    var fileInfo  = new UploadFileInfo(item);
                    fileInfo.InitUploadFileName (ArgsData.LocalRootPath, ArgsData.UploadFolder);
                    uploadFiles.Add(fileInfo);
                }
                else
                {
                    Log.w (string.Format("文件或目录不存在! {0}", item));
                }
            }
            return uploadFiles;
        }
    }
}