using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
//using NUnit.Framework;
using System.Web.Hosting;
using Jacobs.Models.DAL;
using Jacobs.Models;
using GoogleApi.Test.Maps.DistanceMatrix;

namespace GoogleApi.Test
{

    public abstract class BaseTest: FindingPaths
    {
        protected virtual AppSettings Settings { get; private set; }
        protected virtual string ApiKey => this.Settings.ApiKey;
        protected virtual string CryptoKey => this.Settings.CryptoKey;
        protected virtual string ClientId => this.Settings.ClientId;
        protected virtual string SearchEngineId => this.Settings.SearchEngineId;


        public virtual void Setup()
        {
            var directoryInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory ?? "").Parent?.Parent?.Parent;

             var fileInfo = System.IO.Path.Combine(HostingEnvironment.MapPath("~"), "application.js");
          

            if (fileInfo == null)
                throw new NullReferenceException("fileinfo");

            StreamReader file = File.OpenText(fileInfo);
            var reader = new JsonTextReader(file);
            var jsonSerializer = new JsonSerializer();
            this.Settings = jsonSerializer.Deserialize<AppSettings>(reader);

            reader.Close();
            file.Close();

            /*
            using var file = File.OpenText(fileInfo.FullName);
            {
                using var reader = new JsonTextReader(file);
                {
                    var jsonSerializer = new JsonSerializer();
                    this.Settings = jsonSerializer.Deserialize<AppSettings>(reader);
                }
            }
            */
        }

        public class AppSettings
        {
            [JsonProperty("ApiKey")]
            public string ApiKey { get; set; }

            [JsonProperty("CryptoKey")]
            public string CryptoKey { get; set; }

            [JsonProperty("ClientId")]
            public string ClientId { get; set; }

            [JsonProperty("SearchEngineId")]
            public string SearchEngineId { get; set; }
        }
    }
}