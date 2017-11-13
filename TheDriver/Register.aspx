<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="TheDriver.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server" id="RegisterForm" class="container-fluid">

        <input type="text" runat="server" id="FirstName" placeholder="First Name" required="required" />
        <input type="text" runat="server" id="LastName" placeholder="Last Name" required="required"/>
        <input type="email" runat="server" id="EmailAddress" placeholder="Email Address" required="required"/>
        <input type="tel" runat="server" id="PhoneNumber" placeholder="Phone Number" required="required"/>
        <input type="password" runat="server" id="Password" placeholder="Password" required="required"/>
        <input type="password" runat="server" id="ConfirmPassword" placeholder="Confirm Password" required="required"/>
        <asp:Button runat="server" CssClass="btn btn-primary" Text="Submit" ID="submiteButton" OnClick="submiteButton_Click" />
        <input type="button" runat="server" id="cancel" value="Cancel" class="btn btn-default" />
    </form>
        <div class="alert alert-warning" runat="server" id="result" visible="false"></div>

</asp:Content>