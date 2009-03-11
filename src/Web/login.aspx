<%@ Page Language="C#" MasterPageFile="~/content.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="Login" Title="Login to RC Map" EnableViewState="false" %>

<asp:Content ContentPlaceHolderID="LeftMenuItems" runat="server" ID="LoginLeftMenu">                    
    <li><a href="/default.aspx">Home</a></li>                                            
</asp:Content>
<asp:Content ID="LoginContent" ContentPlaceHolderID="Content" Runat="Server">            
    <h1>RC Map Login</h1>
    <asp:Literal ID="FailureText" runat="server" />
    <asp:ValidationSummary ID="ValSummary" runat="server" DisplayMode="BulletList" EnableClientScript="false" HeaderText="<p class='first'>Please correct the following errors:</p>" CssClass="val-summary" />   
    <div class="row">
        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" Text="Username" />
        <asp:TextBox ID="UserName" runat="server"/>
        <asp:RequiredFieldValidator ID="UserNameReqVal" runat="server" ControlToValidate="UserName" EnableClientScript="false" Display="None" SetFocusOnError="true" ErrorMessage="Please enter your username" />
    </div>
    <div class="row">
        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" Text="Password" />
        <asp:TextBox ID="Password" runat="server" TextMode="Password"/>
        <asp:RequiredFieldValidator ID="PasswordReqVal" runat="server" ControlToValidate="Password" EnableClientScript="false" Display="None" SetFocusOnError="true" ErrorMessage="Please enter your password" />
    </div>             
    <br class="clear" />
    <div class="hr">&nbsp;</div>
    <div class="actions">
        <asp:Button ID="Submit" runat="server" Text="Login" CausesValidation="true" OnClick="Submit_Click" />
    </div>        
</asp:Content>

