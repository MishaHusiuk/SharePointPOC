using SharePointCSOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SharePointWebInterface
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            #region sensitive
            string domain = "<your domain name>";
            string appId = "<your app id>";
            string clientSecret = "<your client secret>";

            string siteName = "<your site name>";
            string libraryName = "MyDocuments";
            string testFileName = "test.txt";
            #endregion

            string host = $"https://{domain}.sharepoint.com/sites/{siteName}";
            string relativeFilePath = $"/sites/{siteName}/{libraryName}/{testFileName}";

            SharePointAdapter sharepoint = new SharePointAdapter(host, appId, clientSecret);

            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + testFileName + "\"");
            Response.AddHeader("Content-Length", sharepoint.GetFileLength(relativeFilePath).ToString());

            sharepoint.DownloadSingleFile(relativeFilePath, this.Response.OutputStream);

            Response.End();
        }

        protected void btnLongRunningTaskAndUpload_Click(object sender, EventArgs e)
        {
            Task.Run(new Action(() => {
                Thread.Sleep(1000 * 60);

                #region sensitive
                string domain = "38h2p1";
                string appId = "309ad51e-cac8-4f94-a0ad-dc0ca8cbd9b6";
                string clientSecret = "nkBKPymgcWooupLyj80riMLxNkcwj6DbHLuglnCLJHM=";

                string siteName = "SharePointPOC";
                string libraryName = "MyDocuments";
                string testFileName = "test.txt";
                #endregion

                string host = $"https://{domain}.sharepoint.com/sites/{siteName}";
                string relativeFilePath = $"/sites/{siteName}/{libraryName}/{testFileName}";

                SharePointAdapter sharepoint = new SharePointAdapter(host, appId, clientSecret);

                sharepoint.UploadFile(libraryName, "C:\\uploaded.txt");
            }));
        }
    }
}