using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AspCoreBl.Misc
{
    public static class AppCommon
    {
        public static JsonSerializerSettings SerializerSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                };
            }
        }
        public const string AppName = "App";
        public static readonly byte[] SymmetricSecurityKey = Encoding.ASCII.GetBytes("CarnivalPreCarnivalSale_Carnival_Pre_CarnivalSale_Carnival_PreSale");
        private static readonly string currDirectory = Directory.GetCurrentDirectory();
        private const string appfilesFolderName = "Appfiles";
        private const string emailtemplatesFolderName = "Emailtemplates";
        public static string ConfirmEmailTemplateFilePath
        {
            get
            {
                var filePath = Path.Combine(currDirectory, appfilesFolderName, emailtemplatesFolderName, "ConfirmEmail.html");
                if (File.Exists(filePath))
                    return filePath;
                else
                    return "";
            }
        }
        public static string SetPasswordEmailTemplateFilePath
        {
            get
            {
                var filePath = Path.Combine(currDirectory, appfilesFolderName, emailtemplatesFolderName, "SetPassword.html");
                if (File.Exists(filePath))
                    return filePath;
                else
                    return "";
            }
        }
        public static string ResetPasswordEmailTemplateFilePath
        {
            get
            {
                var filePath = Path.Combine(currDirectory, appfilesFolderName, emailtemplatesFolderName, "ResetPassword.html");
                if (File.Exists(filePath))
                    return filePath;
                else
                    return "";
            }
        }
        public static string ExceptionEmailTemplateFilePath
        {
            get
            {
                var filePath = Path.Combine(currDirectory, appfilesFolderName, emailtemplatesFolderName, "Exception.html");
                if (File.Exists(filePath))
                    return filePath;
                else
                    return "";
            }
        }
    }
}
