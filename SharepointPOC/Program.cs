using System;
using System.IO;
using SharepointPOC;

namespace SharepointTests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region sensitive
            string domain = "<your domain name>";
            string appId = "<your app id>";
            string clientSecret = "<your client secret>";

            string siteName = "<your site name>";
            string libraryName = "MyDocuments";
            string testFileName = "text.txt";
            #endregion

            string host = $"https://{domain}.sharepoint.com/sites/{siteName}";
            string relativeFilePath = $"/sites/{siteName}/{libraryName}/t{testFileName}";

            string localLocation = Directory.GetCurrentDirectory().Replace("bin\\Debug", "downloads");
            string fileToUpload = Directory.GetCurrentDirectory() + "\\SharePointPOC.exe";
            string newFolder = "test-folder";

            SharePointCSOMAdapter sharepoint = new SharePointCSOMAdapter(host, appId, clientSecret);

            Console.WriteLine(@"Select action:
1 - List directory contents
2 - Download single file
3 - Download all files from directory
4 - Upload file
5 - Add Remote Folder");

            string action = Console.ReadLine();

            switch (action) {
                case "1": 
                    sharepoint.ListDirectoryContents(libraryName);
                    break;
                case "2":
                    sharepoint.DownloadSingleFile(relativeFilePath, localLocation);
                    break;
                case "3":
                    sharepoint.DownloadAllFilesFromDirectory(libraryName, localLocation);
                    break;
                case "4":
                    sharepoint.UploadFile(libraryName, fileToUpload);
                    break;
                case "5":
                    sharepoint.AddFolder(libraryName, newFolder);
                    break;
                default:
                    Environment.Exit(0);
                    break;
            }

            Console.WriteLine(@"Done.
Press any key to exit");
            Console.ReadKey();
        }
    }
}
