//using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

/// example taken from: https://docs.microsoft.com/en-us/sharepoint/dev/sp-add-ins/working-with-folders-and-files-with-rest
/// Doesn't work
namespace SharepointPOC
{
    internal class ExamplesCSOM_UserCredentials
    {
        /// <summary>
        /// Download File Via Rest API
        /// </summary>
        /// <param name="webUrl">https://xxxxx/sites/DevSite</param>
        /// <param name="credentials"></param>
        /// <param name="documentLibName">MyDocumentLibrary</param>
        /// <param name="fileName">test.docx</param>
        /// <param name="path">C:\\</param>
        public void DownloadFileViaRestAPI(string webUrl, ICredentials credentials, string documentLibName, string fileName, string path)
        {
            webUrl = webUrl.EndsWith("/") ? webUrl.Substring(0, webUrl.Length - 1) : webUrl;
            string webRelativeUrl = null;
            if (webUrl.Split('/').Length > 3)
            {
                webRelativeUrl = "/" + webUrl.Split(new char[] { '/' }, 4)[3];
            }
            else
            {
                webRelativeUrl = "";
            }

            using (WebClient client = new WebClient())
            {
                client.Headers.Add("X-FORMS_BASED_AUTH_ACCEPTED", "f");
                client.Credentials = credentials;
                Uri endpointUri = new Uri(webUrl + "/_api/web/GetFileByServerRelativeUrl('" + webRelativeUrl + "/" + documentLibName + "/" + fileName + "')/$value");

                byte[] data = client.DownloadData(endpointUri);
                FileStream outputStream = new FileStream(path + fileName, FileMode.OpenOrCreate | FileMode.Append, FileAccess.Write, FileShare.None);
                outputStream.Write(data, 0, data.Length);
                outputStream.Flush(true);
                outputStream.Close();
            }
        }
        //public void Download(string siteURL, string email, string password)
        //{
        //    //set credential of SharePoint online
        //    SecureString secureString = new SecureString();
        //    foreach (char c in password.ToCharArray())
        //    {
        //        secureString.AppendChar(c);
        //    }
        //    ICredentials credentials = new SharePointOnlineCredentials(email, secureString);

        //    //set credential of SharePoint 2013(On-Premises)
        //    //string userName = "Administrator";
        //    //string password = "xxxxxxx";
        //    //string domain = "CONTOSO";
        //    //ICredentials credentials = new NetworkCredential(userName, password, domain);

        //    DownloadFileViaRestAPI(siteURL, credentials, "MyDocuments", "test.txt", "c:\\Users\\mhuziuk");

        //    Console.WriteLine("success");
        //    Console.ReadLine();
        //}
    }
}
