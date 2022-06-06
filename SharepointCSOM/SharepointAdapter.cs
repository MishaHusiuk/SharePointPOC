using Microsoft.SharePoint.Client;
using System;
using System.IO;
using PnP.Framework;


// Authentication: https://docs.microsoft.com/en-us/answers/questions/341294/how-to-perform-sharepoint-online-authentication-in.html
// Helpful:
// https://stackoverflow.com/questions/45674435/401-unauthorized-exception-while-downloading-file-from-sharepoint
// https://sharepoint.stackexchange.com/a/219148

namespace SharepointCSOM
{
    public class SharePointAdapter
    {
        private readonly string siteURL;
        private readonly string appId;
        private readonly string clientSecret;
        public SharePointAdapter(string siteURL, string appId, string clientSecret)
        {
            this.siteURL = siteURL;
            this.appId = appId;
            this.clientSecret = clientSecret;
        }
        public void ListDirectoryContents(string remoteLocationToList)
        {
            using (ClientContext ctx = new AuthenticationManager().GetACSAppOnlyContext(siteURL, appId, clientSecret))
            {
                Web web = ctx.Web;
                ctx.Load(web);

                FileCollection files = ctx.Web.GetFolderByServerRelativeUrl(remoteLocationToList).Files;

                ctx.Load(files);
                ctx.ExecuteQuery();

                foreach (Microsoft.SharePoint.Client.File file in files)
                {
                    Console.WriteLine(file.ServerRelativeUrl);
                }
            }
        }
        public void DownloadSingleFile(string fileRelativeUrl, string destinationLocation)
        {
            using (ClientContext ctx = new AuthenticationManager().GetACSAppOnlyContext(siteURL, appId, clientSecret))
            {
                Web web = ctx.Web;
                ctx.Load(web);

                ctx.ExecuteQuery();

                var file = ctx.Web.GetFileByServerRelativeUrl(fileRelativeUrl);

                ctx.Load(file);
                ctx.ExecuteQuery();
                ClientResult<Stream> streamResult = file.OpenBinaryStream();
                ctx.ExecuteQuery();

                var filePath = $"{destinationLocation}\\{file.Name}";

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    streamResult.Value.CopyTo(fileStream);
                }
            }
        }
        public void DownloadAllFilesFromDirectory(string remoteDirectory, string destinationLocation)
        {
            using (ClientContext ctx = new AuthenticationManager().GetACSAppOnlyContext(siteURL, appId, clientSecret))
            {
                Web web = ctx.Web;
                ctx.Load(web);

                FileCollection files = ctx.Web.GetFolderByServerRelativeUrl(remoteDirectory).Files;

                ctx.Load(files);
                ctx.ExecuteQuery();

                foreach (Microsoft.SharePoint.Client.File file in files)
                {
                    ClientResult<Stream> streamResult = file.OpenBinaryStream();
                    ctx.ExecuteQuery();

                    var filePath = $"{destinationLocation}\\{file.Name}";

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        streamResult.Value.CopyTo(fileStream);
                    }
                }
            }
        }
        public void UploadFile(string destinationDirectory, string localFileLocation)
        {
            if (!System.IO.File.Exists(localFileLocation))
                throw new FileNotFoundException("File not found.", localFileLocation);

            // Prepare to upload 
            string fileName = Path.GetFileName(localFileLocation);
            FileStream fileStream = System.IO.File.OpenRead(localFileLocation);

            using (ClientContext ctx = new AuthenticationManager().GetACSAppOnlyContext(siteURL, appId, clientSecret))
            {
                Web web = ctx.Web;
                ctx.Load(web);

                FileCollection files = ctx.Web.GetFolderByServerRelativeUrl(destinationDirectory).Files;

                ctx.Load(files);
                ctx.ExecuteQuery();

                FileCreationInformation fci = new FileCreationInformation();
                fci.Overwrite = true;
                fci.ContentStream = fileStream;
                fci.Url = fileName;

                files.Add(fci);
                // Checkout ??? 
                ctx.ExecuteQuery();
            }
        }
        public void AddFolder(string parentDirectory, string newDirectory)
        {
            using (ClientContext ctx = new AuthenticationManager().GetACSAppOnlyContext(siteURL, appId, clientSecret))
            {
                Web web = ctx.Web;
                ctx.Load(web);


                Folder remoteDirectory = ctx.Web.GetFolderByServerRelativeUrl(parentDirectory);

                remoteDirectory.AddSubFolder(newDirectory, null);

                ctx.Load(remoteDirectory);
                ctx.ExecuteQuery();
            }
        }
    }
}
