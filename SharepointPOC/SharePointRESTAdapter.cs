using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SharepointPOC
{
    internal class SharePointRESTAdapter
    {
        private static void ReadFileContents(string accessToken, string host, string location, string fileName, string relativeFilePath)
        {
            HttpWebRequest endpointRequest = (HttpWebRequest)HttpWebRequest.Create($"{host}/_api/web/GetFolderByServerRelativeUrl('{location}')/Files('{fileName}')/$value");
            endpointRequest.Method = "GET";
            endpointRequest.Accept = "application/json;odata=verbose";
            endpointRequest.Headers.Add("Authorization", "Bearer " + accessToken);
            WebResponse endpointResponse = endpointRequest.GetResponse();

            using (var reader = new StreamReader(endpointResponse.GetResponseStream()))
            {
                string result = reader.ReadToEnd();
                Console.WriteLine(result);
            }
        }

        private static void ReadFileByURL(string accessToken, string host, string location, string fileName, string relativeFilePath)
        {
            HttpWebRequest endpointRequest = (HttpWebRequest)HttpWebRequest.Create($"{host}/_api/web/GetFolderByServerRelativeUrl('{location}')/Files('{fileName}')/$value");
            endpointRequest.Method = "GET";
            endpointRequest.Accept = "application/json;odata=verbose";
            endpointRequest.Headers.Add("Authorization", "Bearer " + accessToken);
            WebResponse endpointResponse = endpointRequest.GetResponse();

            using (var reader = new StreamReader(endpointResponse.GetResponseStream()))
            {
                string result = reader.ReadToEnd();
                Console.WriteLine(result);
            }
        }

        private static void ReadFileVersions(string accessToken, string host, string location, string fileName, string relativeFilePath)
        {
            HttpWebRequest endpointRequest = (HttpWebRequest)HttpWebRequest.Create($"{host}/_api/Web/GetFileByServerRelativePath(decodedurl='{relativeFilePath}')/Versions");
            // OR: 
            // HttpWebRequest endpointRequest = (HttpWebRequest)HttpWebRequest.Create($"{host}/_api/Web/GetFolderByServerRelativeUrl('{location}')/Files('{fileName}')/Versions");
            endpointRequest.Method = "GET";
            endpointRequest.Accept = "application/json;odata=verbose";
            endpointRequest.Headers.Add("Authorization", "Bearer " + accessToken);
            WebResponse endpointResponse = endpointRequest.GetResponse();

            using (var reader = new StreamReader(endpointResponse.GetResponseStream()))
            {
                string result = reader.ReadToEnd();
                Console.WriteLine(result);
            }
        }
    }
}
