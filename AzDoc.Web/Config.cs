using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace AzDoc.Web
{
    public class Config : IConfig
    {
        private static IConfig _current = new Config();

        public static IConfig Current
        {
            get { return _current; }
        }

        public CloudStorageAccount GetStorageAccount()
        {
            return CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageAccount"));
        }

        internal static void SetCurrent(IConfig config)
        {
            if (config == null) throw new ArgumentNullException("config");
            _current = config;
        }
    }
}