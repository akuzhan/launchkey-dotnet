<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LoginConfirm.aspx.cs" Inherits="LaunchKey.Examples.AspNetWebForms.Account.LoginConfirm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    
    <div style="text-align: center">
        <asp:HiddenField ID="AuthRequest" runat="server" />

        <p><strong>Hello! What is your name?</strong></p>
        <asp:TextBox ID="FriendlyName" runat="server" autofocus>
        </asp:TextBox><br />
        <asp:RequiredFieldValidator ControlToValidate="FriendlyName" Text="... please?" runat="server" />
       
        <div>

            <asp:Button ID="SubmitButton" Text="Continue" runat="server" 
                onclick="SubmitButton_Click" />
        </div>
    </div>

</asp:Content>
