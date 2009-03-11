<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="link.aspx.cs" Inherits="Admin_Link" Title="Useful Link - " %>

<asp:Content ID="LinkDescription" ContentPlaceHolderID="Content" Runat="Server">
    <h2>Link - <asp:Literal ID="LinkTitle" runat="server" /></h2>    
    <asp:ValidationSummary ID="ValSummary" runat="server" DisplayMode="BulletList" EnableClientScript="false" HeaderText="<p class='first'>Please correct the following errors:</p>" CssClass="val-summary" />
    <div class="row">
        <asp:Label AssociatedControlID="Name" runat="server" Text="Name:<span class='req-ind'>*</span>" />
        <asp:TextBox ID="Name" runat="server" MaxLength="64" />        
        <asp:RequiredFieldValidator ID="NameReqVal" runat="server" ControlToValidate="Name" EnableClientScript="false" Display="None" SetFocusOnError="true" ErrorMessage="Please the link name" />    
        <rcmap:MaxLengthValidator ID="NameLenVal" runat="server" ControlToValidate="Name" EnableClientScript="false" Display="None" SetFocusOnError="true" MaxLength="64" ErrorMessage="The link name is limited to 64 characters" />
    </div>
    <div class="row">
        <asp:Label AssociatedControlID="Url" runat="server" Text="Url:<span class='req-ind'>*<span>" />
        <asp:TextBox ID="Url" runat="server" MaxLength="128" />
        <asp:RequiredFieldValidator ID="UrlReqVal" runat="server" ControlToValidate="Url" EnableClientScript="false" Display="None" SetFocusOnError="true" ErrorMessage="Please enter the Url" />
        <rcmap:MaxLengthValidator ID="UrlLenVal" runat="server" ControlToValidate="Url" EnableClientScript="false" Display="None" SetFocusOnError="true" MaxLength="128" ErrorMessage="The Url limited to 128 characters" />
        <asp:RegularExpressionValidator ID="UrlVal" runat="server" ValidationExpression="^http://(([0-9]{1,3}.){3}[0-9]{1,3}|([0-9a-z-]+.)+[a-z]{2,6})(:[0-9]+)?/?" ControlToValidate="Url" EnableClientScript="false" Display="None" SetFocusOnError="true" ErrorMessage="Please enter a valid Url" />
    </div>
    <div class="row">
        <asp:Label AssociatedControlID="Category" runat="server" Text="Category:<span class='req-ind'>*</span>" />               
        <asp:DropDownList ID="Category" runat="server" DataTextField="Name" DataValueField="Id" AppendDataBoundItems="true">
            <asp:ListItem Value="" Text="" Selected="true" />
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="CategoryReqVal" runat="server" ControlToValidate="Category" EnableClientScript="false" Display="None" SetFocusOnError="true" ErrorMessage="Please select a category" />
    </div>
    <div class="row">
        <asp:Label AssociatedControlID="Description" runat="server" Text="Description:<span class='req-ind'>*</span>" />
        <asp:TextBox ID="Description" runat="server" TextMode="MultiLine" MaxLength="500" />
        <asp:RequiredFieldValidator ID="DescriptionReqVal" runat="server" ControlToValidate="Description" EnableClientScript="false" Display="None" SetFocusOnError="true" ErrorMessage="Please enter a description" />
        <rcmap:MaxLengthValidator ID="DescriptionLenVal" runat="server" ControlToValidate="Description" EnableClientScript="false" Display="None" SetFocusOnError="true" MaxLength="1000" ErrorMessage="The description is limited to 500 characters" />
    </div>
    <div class="actions">                
        <asp:LinkButton ID="Delete" runat="server" Text="Delete" ToolTip="Delete this link" OnClick="Delete_Click" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete this link?');" /><span class="sep" id="DeleteSep" runat="server">|</span><asp:HyperLink ID="Cancel" runat="server" ToolTip="Cancel" NavigateUrl="~/admin/links.aspx" Text="Cancel" /><span class="sep">|</span><asp:LinkButton ID="Submit" runat="server" Text="Save" ToolTip="Save this link" OnClick="Submit_Click" CausesValidation="true" />
    </div>
</asp:Content>

