<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TheDriver.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server" id="LoginForm" class="container-fluid">
        <input type="email" runat="server" id="EmailAddress" required="required" placeholder="Email Address" />
        <input type="password" runat="server" id="Password" required="required" placeholder="Password" />
        <asp:Button runat="server" Text="Submit" OnClick="Unnamed_Click" />
    </form>
    <div class="alert alert-warning" runat="server" id="result" visible="false"></div>
</asp:Content>

