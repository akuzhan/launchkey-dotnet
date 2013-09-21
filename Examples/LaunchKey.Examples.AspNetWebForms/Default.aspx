<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="LaunchKey.Examples.AspNetWebForms._Default" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to the <em><strong>ASP.NET WebForms LaunchKey Example</strong></em>
    </h2>
    <p>
        Greetings! This demo application allows you to login using LaunchKey. Login credentials are stored in the session, but not persisted in any storage.
    </p>
    <p>In a real-world application you would persist user logon information and profile data in a database. See the ASP.NET MVC example for a crude implementation of that.</p>

</asp:Content>
