<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SharePointWebInterface._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>ASP.NET</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-6">
            <h2>Sharepoint POC</h2>
            <p>
                Download the file
            </p>
            <p>
                <asp:Button runat="server" ID="btnDownload" OnClick="btnDownload_Click" Text="Download Sample File"/>
            </p>
        </div>
         <div class="col-md-6">
            <h2>Trigger long-running task and upload a file</h2>
            <p>
                Upload file
            </p>
            <p>
                <asp:Button runat="server" ID="btnLLTUpload" OnClick="btnLongRunningTaskAndUpload_Click" Text="Upload Sample File"/>
            </p>
        </div>
    </div>

</asp:Content>
