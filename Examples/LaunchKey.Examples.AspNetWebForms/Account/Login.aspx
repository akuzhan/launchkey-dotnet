<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="LaunchKey.Examples.AspNetWebForms.Account.Login" %>

<%@ Register Src="~/Controls/LoginControl.ascx" TagName="LoginControl" TagPrefix="LK" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Log In with LaunchKey
    </h2>

    <LK:LoginControl runat="server" />

</asp:Content>
