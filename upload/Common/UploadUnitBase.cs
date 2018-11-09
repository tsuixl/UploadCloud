using Util;

namespace Common
{
    public class UploadUnitBase
    {
        protected UploadAgentBase UploadAgent;

        public UploadFileInfo FileInfo
        {
            get; protected set;
        }

        public UploadUnitBase (UploadAgentBase uploadAgent)
        {
            UploadAgent = uploadAgent;
        }


        public bool Start (UploadFileInfo uploadFileInfo)
        {
            var error = uploadFileInfo.CheckFile ();
            if (!string.IsNullOrEmpty(error))
            {
                Log.w (error);
                return false;
            }

            if (uploadFileInfo == null)
            {
                Log.w ("uploadFileInfo == null");
                return false;
            }

            FileInfo = uploadFileInfo;
            StartUpload();
            return true;
        }


        protected virtual void StartUpload ()
        { }

    }
}