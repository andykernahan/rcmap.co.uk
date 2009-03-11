<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="club.aspx.cs" Inherits="Admin_Club" Title="New Club" %>

<asp:Content ID="ClubContent" ContentPlaceHolderID="Content" Runat="Server">
    <h2>Club - <asp:Literal ID="ClubTitle" runat="server" /></h2>
    <asp:ValidationSummary ID="ValSummary" runat="server" DisplayMode="BulletList" EnableClientScript="false" HeaderText="<p class='first'>Please correct the following errors:</p>" CssClass="val-summary" />
    <div class="yui-g">
        <div class="yui-u first">
            <div class="row">
                <asp:Label AssociatedControlID="Name" runat="server" Text="Club Name:<span class='req-ind'>*</span>" />
                <asp:TextBox ID="Name" runat="server" MaxLength="128" />
                <asp:RequiredFieldValidator ID="NameReqVal" runat="server" ControlToValidate="Name" EnableClientScript="false" Display="None" SetFocusOnError="true" ErrorMessage="Please enter the club name" />    
                <rcmap:MaxLengthValidator ID="NameLenVal" runat="server" ControlToValidate="Name" EnableClientScript="false" Display="None" SetFocusOnError="true" MaxLength="1000" ErrorMessage="The club name is limited to 128 characters" />
            </div>
            <div class="row">
                <asp:Label AssociatedControlID="Extended" runat="server" Text="Address Line 1:" />            
                <asp:TextBox ID="Extended" runat="server" MaxLength="128" />
                <rcmap:MaxLengthValidator ID="ExtendedLenVal" runat="server" ControlToValidate="Extended" EnableClientScript="false" Display="None" SetFocusOnError="true" MaxLength="128" ErrorMessage="The first line of the address is limited to 128 characters" />
            </div>
            <div class="row">
                <asp:Label AssociatedControlID="Street" runat="server" Text="Address Line 2:" />
                <asp:TextBox ID="Street" runat="server" MaxLength="128" />
                <rcmap:MaxLengthValidator ID="StreetLenVal" runat="server" ControlToValidate="Street" EnableClientScript="false" Display="None" SetFocusOnError="true" MaxLength="128" ErrorMessage="The second line of the address is limited to 128 characters" />
            </div>
            <div class="row">
                <asp:Label AssociatedControlID="Locality" runat="server" Text="Address Line 3:" />
                <asp:TextBox ID="Locality" runat="server" MaxLength="128" />
                <rcmap:MaxLengthValidator ID="LocalityLenVal" runat="server" ControlToValidate="Locality" EnableClientScript="false" Display="None" SetFocusOnError="true" MaxLength="128" ErrorMessage="The third line of the address is limited to 128 characters" />
            </div>
            <div class="row">
                <asp:Label AssociatedControlID="Region" runat="server" Text="Region:<span class='req-ind'>*</span>" />               
                <asp:DropDownList ID="Region" runat="server" SelectionMode="Multiple" DataTextField="Name" DataValueField="Id" AppendDataBoundItems="true">
                    <asp:ListItem Text="" Value="" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RegionReqVal" runat="server" ControlToValidate="Region" EnableClientScript="false" Display="None" SetFocusOnError="true" ErrorMessage="Please select the region" />
            </div>
            <div class="row">
                <asp:Label AssociatedControlID="Postcode" runat="server" Text="Postcode:<span class='req-ind'>*</span>" />   
                <asp:TextBox ID="Postcode" runat="server" MaxLength="15" />
                <asp:RequiredFieldValidator ID="PostcodeReqVal" runat="server" ControlToValidate="Postcode" EnableClientScript="false" Display="None" SetFocusOnError="true" ErrorMessage="Please enter a postcode" />    
                <rcmap:MaxLengthValidator ID="PostcodeLenVal" runat="server" ControlToValidate="Postcode" EnableClientScript="false" Display="None" SetFocusOnError="true" MaxLength="15" ErrorMessage="The postcode is limited to 15 characters" />
            </div>
            <div class="row">
                <asp:Label AssociatedControlID="Latitude" runat="server" Text="Latitude:<span class='req-ind'>*</span>" />
                <asp:TextBox ID="Latitude" runat="server" />
                <asp:RequiredFieldValidator ID="LongitudeReqVal" runat="server" ControlToValidate="Longitude" EnableClientScript="false" Display="None" SetFocusOnError="true" ErrorMessage="Please enter the longitude" />                
            </div>
            <div class="row">
                <asp:Label AssociatedControlID="Longitude" runat="server" Text="Longitude:<span class='req-ind'>*</span>" />
                <asp:TextBox ID="Longitude" runat="server" />
                <asp:RequiredFieldValidator ID="LatitudeReqVal" runat="server" ControlToValidate="Latitude" EnableClientScript="false" Display="None" SetFocusOnError="true" ErrorMessage="Please enter the latitude" />
            </div>
             <div class="row">
                <asp:Label AssociatedControlID="ContactName" runat="server" Text="Contact Name:" />
                <asp:TextBox ID="ContactName" runat="server" MaxLength="128" />
                <rcmap:MaxLengthValidator ID="ContactNameLenVal" runat="server" ControlToValidate="ContactName" EnableClientScript="false" Display="None" SetFocusOnError="true" MaxLength="128" ErrorMessage="The contact name is limited to 128 characters" />                                
            </div>
            <div class="row">
                <asp:Label AssociatedControlID="ContactEmail" runat="server" Text="Contact Email:" />   
                <asp:TextBox ID="ContactEmail" runat="server" MaxLength="128" />
                <rcmap:MaxLengthValidator ID="EmailLenVal" runat="server" ControlToValidate="ContactEmail" EnableClientScript="false" Display="None" SetFocusOnError="true" MaxLength="128" ErrorMessage="The contact email address is limited to 128 characters" />
                <asp:RegularExpressionValidator ID="EmailRegexVal" runat="server" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$" ControlToValidate="ContactEmail" EnableClientScript="false" Display="None" SetFocusOnError="true" ErrorMessage="The entered email address appears to invalid" />
            </div>
        </div>
        <div class="yui-u">
            <div class="row">
                <asp:Label AssociatedControlID="ClubCategories" runat="server" Text="Categories:<span class='req-ind'>*</span>" />               
                <asp:ListBox ID="ClubCategories" runat="server" SelectionMode="Multiple" DataTextField="Name" DataValueField="Id" style="height:18em;" />
                <asp:RequiredFieldValidator ID="ClubCategoriesReqVal" runat="server" ControlToValidate="ClubCategories" EnableClientScript="false" Display="None" SetFocusOnError="true" ErrorMessage="Please select one or more categories" />
            </div>
            <div class="row">
                <asp:Label AssociatedControlID="SiteUrl" runat="server" Text="Website:" />
                <asp:TextBox ID="SiteUrl" runat="server" MaxLength="128" />
                <rcmap:MaxLengthValidator ID="SiteUrlLenVal" runat="server" ControlToValidate="SiteUrl" EnableClientScript="false" Display="None" SetFocusOnError="true" MaxLength="128" ErrorMessage="The club website is limited to 128 characters" />
                <asp:RegularExpressionValidator ID="SiteUrlVal" runat="server" ValidationExpression="^http://(([0-9]{1,3}.){3}[0-9]{1,3}|([0-9a-z-]+.)+[a-z]{2,6})(:[0-9]+)?/?" ControlToValidate="SiteUrl" EnableClientScript="false" Display="None" SetFocusOnError="true" ErrorMessage="The entered site url appears to invalid" />
            </div>
        </div>
    </div>    
    <div class="actions">                
        <asp:LinkButton ID="Delete" runat="server" Text="Delete" ToolTip="Delete this club" OnClick="Delete_Click" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete this club?');" /><span class="sep" id="DeleteSep" runat="server">|</span><asp:HyperLink ID="Cancel" runat="server" ToolTip="Cancel" NavigateUrl="~/admin/default.aspx" Text="Cancel" /><span class="sep">|</span><asp:LinkButton ID="Submit" runat="server" Text="Save" ToolTip="Save this club" OnClick="Submit_Click" CausesValidation="true" />
    </div>
    <div class="audit"><asp:Literal ID="Audit" runat="server" /></div>
</asp:Content>

