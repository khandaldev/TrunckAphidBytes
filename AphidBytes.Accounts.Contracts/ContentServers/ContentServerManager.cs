using AphidBytes.Core.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts.ContentServers
{
    public class ContentServerManager
    {
        private static List<IContentServer> _contentServers;
        protected static List<IContentServer> ContentServers => _contentServers ?? (_contentServers = ContentServerRepository.GetContentServers());

        public static IContentServer GetDefaultContentServer()
        {
            return ContentServers.FirstOrDefault(cs => cs.IsDefault); 
        }

        public static IContentServer GetServerById(int contentServerId)
        {
            return ContentServers.FirstOrDefault(cs => cs.ServerId == contentServerId);
        }
    }

    internal class ContentServerRepository
    {
        public static List<IContentServer> GetContentServers()
        {
            return new List<IContentServer>
            {
                new LocalContentServer()
            };
        }
    }

    public interface IContentServer
    {
        int ServerId { get;  }
        string ServerName { get; }
        string ServerDomain { get; }
        bool IsDefault { get; }
        string UploadToContentServer(Stream contentToUpload);
        string GetUri(string path, bool useHttps = false);
    }

    public class LocalContentServer : IContentServer
    {
        public ConfigurationValue<string> LocalServerPhysicalPath = new ConfigurationValue<string>("contentServer.default.localPath");
        public ConfigurationValue<string> LocalServerDomain = new ConfigurationValue<string>("contentServer.default.domain");
        public const int DefaultServerId = 1;
        public const string DefaultServerName = "aphidbyte.images";

        public int ServerId { get { return DefaultServerId; } }
        public string ServerName { get { return DefaultServerName; } } 
        public string ServerDomain { get { return LocalServerDomain.Value; } }
        public bool IsDefault { get { return true; } }

        public string UploadToContentServer(Stream contentToUpload)
        {
            var fileName = AttemptGetEmptyFileName();
            var fileUri = Path.Combine(LocalServerPhysicalPath.Value, fileName);
            using (var fileStream = File.Create(fileUri))
            {
                contentToUpload.Seek(0, SeekOrigin.Begin);
                contentToUpload.CopyTo(fileStream);
            }

            return fileName; 
        }
        public string GetUri(string path, bool useHttps = false)
        {
            var defaultDomain = LocalServerDomain.Value; 
            if (useHttps)
            {
                defaultDomain = defaultDomain.Replace("http://", "https://");
            }
            return Path.Combine(defaultDomain, path);
        }

        private string AttemptGetEmptyFileName()
        {
            var fileNameFound = false;
            var fileNameAttempts = 0; 
            while (!fileNameFound && fileNameAttempts < 20)
            {
                fileNameAttempts++;
                var fileName = $"{Guid.NewGuid().ToString()}.jpg";
                var fileUri = Path.Combine(LocalServerPhysicalPath.Value, fileName);
                if (!File.Exists(fileUri))
                {
                    return fileName;
                }
            }

            throw new Exception("Could not find a valid place to store profile image.");
        }

        
    }
}
