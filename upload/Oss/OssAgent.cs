using Aliyun.OSS;
using Aliyun.OSS.Common;
using System.Collections.Generic;
using Common;
using Util;

namespace Oss
{
    public class OssAgent : UploadAgentBase
    {
        public OssInit Data { get; private set; }
        private OssClient _ossClient;


        public override object GetCtl()
        {
            return _ossClient;
        }

        public override object GetCustomInfo()
        {
            return Data.BucketName;
        }


        public OssAgent (OssInit data) :base (data)
        {
            Data = data;
        }


        public override void OnUploadFinish(UploadFileInfo fileInfo, string error)
        {
            if (string.IsNullOrEmpty(error))
            {
                Log.l (string.Format("上传成功 => {0}", fileInfo.UploadFileName));
            }
            else
            {
                Log.w (string.Format("上传失败 => {0}, {1}", fileInfo.UploadFileName, error));
            }
        }

        
        public override void StartUpload (List<string> fileOrFolders)
        {
            base.StartUpload (fileOrFolders);
            var uploadFiles = GetUploadFilesByFileOrFolder (fileOrFolders);
            foreach (var f in uploadFiles)
            {
                OssUploadUnit uploadUnit = new OssUploadUnit(this);
                uploadUnit.Start(f);
            }
        }


        public bool Connect ()
        {
            try
            {
                var conf = new ClientConfiguration();
                conf.ConnectionTimeout = Data.ConnectionTimeout;
                conf.EnalbeMD5Check = Data.Md5Checkout;

                _ossClient = new OssClient(Data.Endpoint, Data.AccessKeyId, Data.AccessKeySecret, conf); 
                return true;
            }
            catch (System.Exception)
            {
                
            }
            return false;
        } 


        #region 上传
        
        public void PutFile (string localFileName, string uploadFileName)
        {
            if (_ossClient != null)
            {
                _ossClient.PutObject (Data.BucketName, uploadFileName, localFileName);
            }
        }

        #endregion

        public bool IntlContainBucket (string name)
        {
            if (_ossClient != null)
            {
                return _ossClient.DoesBucketExist (name);
            }
            return false;
        }

        
    }
}