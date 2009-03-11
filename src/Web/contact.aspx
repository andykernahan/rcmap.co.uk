<%@ Page Language="C#" MasterPageFile="~/content.master" AutoEventWireup="true" CodeFile="contact.aspx.cs" Inherits="Contact" Title="RC Map - Contact Us" %>
<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>

<asp:Content ID="ClubsMetaDesc" ContentPlaceHolderID="MetaDesc" runat="server">
    <meta name="description" content="RC Map - If you would like to leave some feedback, suggest a feature, or just have a general question regarding the site, please get in touch." />
</asp:Content>
<asp:Content ID="ContactContent" ContentPlaceHolderID="Content" Runat="Server">
    <h1>Contact RC Map</h1>
    <p class="single">If you would like to leave some feedback, suggest a feature, or just have a general question regarding the site, please enter the required information below and submit the form.</p>
    <asp:ValidationSummary ID="ValSummary" runat="server" DisplayMode="BulletList" EnableClientScript="false" HeaderText="<p class='first'>Please correct the following errors:</p>" CssClass="val-summary" />
    <div class="row">
        <asp:Label AssociatedControlID="Name" runat="server" Text="Name:<span class='req-ind'>*</span>" />
        <asp:TextBox ID="Name" runat="server" MaxLength="128" />        
        <asp:RequiredFieldValidator ID="NameReqVal" runat="server" ControlToValidate="Name" EnableClientScript="false" Display="None" SetFocusOnError="true" ErrorMessage="Please enter your name" />    
        <rcmap:MaxLengthValidator ID="NameLenVal" runat="server" ControlToValidate="Name" EnableClientScript="false" Display="None" SetFocusOnError="true" MaxLength="128" ErrorMessage="Your name is limited to 128 characters" />
    </div>
    <div class="row">        
        <asp:Label AssociatedControlID="Email" runat="server" Text="Email:<span class='req-ind'>*</span>" />   
        <asp:TextBox ID="Email" runat="server" />                
        <asp:RequiredFieldValidator ID="EmailReqVal" runat="server" ControlToValidate="Email" EnableClientScript="false" Display="None" SetFocusOnError="true" ErrorMessage="Please enter your email address" />        
        <rcmap:MaxLengthValidator ID="EmailLenVal" runat="server" ControlToValidate="Email" EnableClientScript="false" Display="None" SetFocusOnError="true" MaxLength="128" ErrorMessage="Your email address is limited to 128 characters" />
        <asp:RegularExpressionValidator ID="EmailRegexVal" runat="server" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$" ControlToValidate="Email" EnableClientScript="false" Display="None" SetFocusOnError="true" ErrorMessage="The email address appears to invalid" />
    </div>
    <div class="row">
        <asp:Label AssociatedControlID="Category" runat="server" Text="Category:<span class='req-ind'>*</span>" />               
        <asp:DropDownList ID="Category" runat="server" DataTextField="Name" DataValueField="Id" AppendDataBoundItems="true">
            <asp:ListItem Value="" Text="" Selected="true" />
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="CategoryReqVal" runat="server" ControlToValidate="Category" EnableClientScript="false" Display="None" SetFocusOnError="true" ErrorMessage="Please select a category" />
    </div>
    <div class="row">
        <asp:Label AssociatedControlID="Content" runat="server" Text="Query:<span class='req-ind'>*</span>" />
        <asp:TextBox ID="Content" runat="server" TextMode="MultiLine" MaxLength="1000" />
        <asp:RequiredFieldValidator ID="ContentReqVal" runat="server" ControlToValidate="Content" EnableClientScript="false" Display="None" SetFocusOnError="true" ErrorMessage="Please enter your query" />
        <rcmap:MaxLengthValidator ID="ContentLenVal" runat="server" ControlToValidate="Content" EnableClientScript="false" Display="None" SetFocusOnError="true" MaxLength="1000" ErrorMessage="Your query is limited to 1000 characters" />
    </div>
    <div class="row">
        <asp:Label AssociatedControlID="Recaptcha" runat="server" Text="Captcha:<span class='req-ind'>*</span>" />
        <recaptcha:RecaptchaControl ID="Recaptcha" runat="server" PublicKey="6LcELQcAAAAAAPGyE_XzzFuu9JIVTDO0_L99YJsU " PrivateKey="6LcELQcAAAAAAF87e5qP-X1s4UEWfAXZmtwMbs7F" Theme="white" ErrorMessage="Please ensure the captcha words are correct" />
        <br class="clear" />
    </div>
    <div class="actions">        
        <asp:Button ID="SumbitButton" runat="server" OnClick="SumbitButton_Click" Text="Submit" ToolTip="Submit your query" CssClass="aw"/>
    </div>
</asp:Content>