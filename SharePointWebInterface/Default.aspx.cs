using SharePointCSOM;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}