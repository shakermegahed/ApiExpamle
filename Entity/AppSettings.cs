using System;
using System.Collections.Generic;
using System.Text;

namespace BB.CRM.Entities
{
    public class AppSettings
    {
        public string UploadFolder { get; set; }
        public string ImagesFolder { get; set; }
        public string ClientURL { get; set; }
        public bool RequireEmailConfirmation { get; set; }
        public int TrialDays { get; set; }
        public string AssetsS3Bucket { get; set; }
        public string PublicAPI { get; set; }
    }
}
