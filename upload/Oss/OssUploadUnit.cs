using System;
using System.IO;
using System.Threading;
using Aliyun.OSS;
using Aliyun.OSS.Common;
using Common;
using Util;

namespace Oss
{
    public class OssUploadUnit : UploadUnitBase
    {

        AutoResetEvent _event = new AutoResetEvent(false);

        public OssUploadUnit(UploadAgentBase uploadAgent) : base(uploadAgent)
        {
        }

        protected override void StartUpload ()
        {
            var ossClient = (OssClient)UploadAgent.GetCtl();
            var bucketName = UploadAgent.GetCustomInfo().ToString();

            try
            {
                using (var fs = File.Open(FileInfo.LocalFileName, FileMode.Open))
                {
                    var metadata = new ObjectMetadata();
					// 增加自定义元信息。
                    metadata.UserMetadata.Add("mykey1", "myval1");
                    metadata.UserMetadata.Add("mykey2", "myval2");
                    metadata.CacheControl = "No-Cache";
                    metadata.ContentType = "text/html";
                    // string result = "Notice user: put object finish";

                    // 异步上传。
                    ossClient.BeginPutObject(bucketName, FileInfo.UploadFileName, fs, metadata, PutObjectCallback, null);

                    _event.WaitOne();
                }
            }
            catch (OssException ex)
            {
                Log.w (string.Format("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId));
            }
            catch (Exception ex)
            {
                Log.w (string.Format("Failed with error info: {0}", ex.Message));
            }
        }


        protected void PutObjectCallback (IAsyncResult ar)
        {
            try
            {
                var ossClient = (OssClient)UploadAgent.GetCtl();
                ossClient.EndPutObject(ar);
                UploadAgent.OnUploadFinish (FileInfo, string.Empty);
            }
            catch (Exception ex)
            {
                UploadAgent.OnUploadFinish (FileInfo, ex.ToString());
            }
            finally{
                _event.Set();
            }
        }

        

    }
}