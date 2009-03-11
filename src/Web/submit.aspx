<%@ Page Language="C#" MasterPageFile="~/content.master" AutoEventWireup="true" CodeFile="submit.aspx.cs" Inherits="SubmitClub" Title="RC Map - Submit a RC Club" EnableViewState="true" %>
<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>

<asp:Content ID="ClubsMetaDesc" ContentPlaceHolderID="MetaDesc" runat="server">
    <meta name="description" content="RC Map - If you know of a club that is not on the map, please submit it and we will add it to the map!" />
</asp:Content>
<asp:Content ID="SubmitContent" ContentPlaceHolderID="Content" runat="Server">    
    <h1>Submit a RC Club</h1>
    <asp:ValidationSummary ID="ValSummary" runat="server" DisplayMode="BulletList" EnableClientScript="false" HeaderText="<p class='first'>Please correct the following errors:</p>" CssClass="val-summary" />
    <div class="yui-g">
        <div class="yui-u first">        
            <div class="row">
                <asp:Label AssociatedControlID="Name" runat="server" Text="RC Club Name:<span class='req-ind'>*</span>" />
                <asp:TextBox ID="Name" runat="server" MaxLength="128" onfocus="ShowHelp('Name');" />
                <asp:RequiredFieldValidator ID="NameReqVal" runat="server" ControlToValidate="Name" EnableClientScript="false" Display="None" SetFocusOnError="true" ErrorMessage="Please enter the club name" />    
                <rcmap:MaxLengthValidator ID="NameLenVal" runat="server" ControlToValidate="Name" EnableClientScript="false" Display="None" SetFocusOnError="true" MaxLength="1000" ErrorMessage="The club name is limited to 128 characters" />
            </div>
            <div class="row">
                <asp:Label AssociatedControlID="Extended" runat="server" Text="Address Line 1:<span class='req-ind'>*</span>" />            
                <asp:TextBox ID="Extended" runat="server" MaxLength="128" onfocus="ShowHelp('Extended');" />
                <asp:RequiredFieldValidator ID="ExtendedReqVal" runat="server" ControlToValidate="Extended" EnableClientScript="false" Display="None" SetFocusOnError="true" ErrorMessage="Please enter the first line of the address" />    
                <rcmap:MaxLengthValidator ID="ExtendedLenVal" runat="server" ControlToValidate="Extended" EnableClientScript="false" Display="None" SetFocusOnError="true" MaxLength="128" ErrorMessage="The first line of the address is limited to 128 characters" />
            </div>
            <div class="row">
                <asp:Label AssociatedControlID="Street" runat="server" Text="Address Line 2:" />
                <asp:TextBox ID="Street" runat="server" MaxLength="128" onfocus="ShowHelp('Street');" />
                <rcmap:MaxLengthValidator ID="StreetLenVal" runat="server" ControlToValidate="Street" EnableClientScript="false" Display="None" SetFocusOnError="true" MaxLength="128" ErrorMessage="The second line of the address is limited to 128 characters" />
            </div>
            <div class="row">
                <asp:Label AssociatedControlID="Locality" runat="server" Text="Address Line 3:" />
                <asp:TextBox ID="Locality" runat="server" MaxLength="128" onfocus="ShowHelp('Locality');" />
                <rcmap:MaxLengthValidator ID="LocalityLenVal" runat="server" ControlToValidate="Locality" EnableClientScript="false" Display="None" SetFocusOnError="true" MaxLength="128" ErrorMessage="The third line of the address is limited to 128 characters" />
            </div>
            <div class="row">
                <asp:Label AssociatedControlID="Region" runat="server" Text="Region:<span class='req-ind'>*</span>" />               
                <asp:DropDownList ID="Region" runat="server" SelectionMode="Multiple" DataTextField="Name" DataValueField="Id" AppendDataBoundItems="true" onfocus="ShowHelp('Region');">
                    <asp:ListItem Value="" Text="" Selected="true" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RegionReqVal" runat="server" ControlToValidate="Region" EnableClientScript="false" Display="None" SetFocusOnError="true" ErrorMessage="Please select the region" />
            </div>
            <div class="row">
                <asp:Label AssociatedControlID="Postcode" runat="server" Text="Postcode:<span class='req-ind'>*</span>" />   
                <asp:TextBox ID="Postcode" runat="server" MaxLength="15" onfocus="ShowHelp('Postcode');" />
                <asp:RequiredFieldValidator ID="PostcodeReqVal" runat="server" ControlToValidate="Postcode" EnableClientScript="false" Display="None" SetFocusOnError="true" ErrorMessage="Please enter a postcode" />    
                <rcmap:MaxLengthValidator ID="PostcodeLenVal" runat="server" ControlToValidate="Postcode" EnableClientScript="false" Display="None" SetFocusOnError="true" MaxLength="15" ErrorMessage="The postcode is limited to 15 characters" />
            </div>
            <div class="row">
                <asp:Label AssociatedControlID="ClubCategories" runat="server" Text="Categories:<span class='req-ind'>*</span>" />               
                <asp:ListBox ID="ClubCategories" runat="server" SelectionMode="Multiple" DataTextField="Name" DataValueField="Id" onfocus="ShowHelp('ClubCategories');" />
                <asp:RequiredFieldValidator ID="ClubCategoriesReqVal" runat="server" ControlToValidate="ClubCategories" EnableClientScript="false" Display="None" SetFocusOnError="true" ErrorMessage="Please select one or more categories" />
            </div>
            <div class="row">
                <asp:Label AssociatedControlID="SiteUrl" runat="server" Text="Website:" />
                <asp:TextBox ID="SiteUrl" runat="server" MaxLength="128" onfocus="ShowHelp('SiteUrl');" />
                <rcmap:MaxLengthValidator ID="SiteUrlLenVal" runat="server" ControlToValidate="SiteUrl" EnableClientScript="false" Display="None" SetFocusOnError="true" MaxLength="128" ErrorMessage="The club website is limited to 128 characters" />
                <asp:RegularExpressionValidator ID="SiteUrlVal" runat="server" ValidationExpression="^http://(([0-9]{1,3}.){3}[0-9]{1,3}|([0-9a-z-]+.)+[a-z]{2,6})(:[0-9]+)?/?" ControlToValidate="SiteUrl" EnableClientScript="false" Display="None" SetFocusOnError="true" ErrorMessage="The entered site url appears to invalid" />
            </div>
            <div class="row">
                <asp:Label AssociatedControlID="ContactName" runat="server" Text="Contact Name:<span class='req-ind'>*</span>" />
                <asp:TextBox ID="ContactName" runat="server" MaxLength="128" onfocus="ShowHelp('ContactName');" />
                <rcmap:MaxLengthValidator ID="ContactNameLenVal" runat="server" ControlToValidate="ContactName" EnableClientScript="false" Display="None" SetFocusOnError="true" MaxLength="128" ErrorMessage="The contact name is limited to 128 characters" />                
                <asp:RequiredFieldValidator ID="ContactNameReqVal" runat="server" ControlToValidate="ContactName" EnableClientScript="false" Display="None" SetFocusOnError="true" ErrorMessage="Please enter a contact name" />    
            </div>
            <div class="row">
                <asp:Label AssociatedControlID="Email" runat="server" Text="Contact Email:<span class='req-ind'>*</span>" />   
                <asp:TextBox ID="Email" runat="server" MaxLength="128" onfocus="ShowHelp('Email');" />
                <asp:RequiredFieldValidator ID="EmailReqVal" runat="server" ControlToValidate="Email" EnableClientScript="false" Display="None" SetFocusOnError="true" ErrorMessage="Please enter a contact email address" />
                <rcmap:MaxLengthValidator ID="EmailLenVal" runat="server" ControlToValidate="Email" EnableClientScript="false" Display="None" SetFocusOnError="true" MaxLength="128" ErrorMessage="The contact email address is limited to 128 characters" />
                <asp:RegularExpressionValidator ID="EmailRegexVal" runat="server" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$" ControlToValidate="Email" EnableClientScript="false" Display="None" SetFocusOnError="true" ErrorMessage="The entered email address appears to invalid" />
            </div>
            <div class="row">                
                <recaptcha:RecaptchaControl ID="Recaptcha" runat="server" PublicKey="6LcELQcAAAAAAPGyE_XzzFuu9JIVTDO0_L99YJsU " PrivateKey="6LcELQcAAAAAAF87e5qP-X1s4UEWfAXZmtwMbs7F" Theme="white" ErrorMessage="Please ensure the captcha words are correct" />
                <br class="clear" />
            </div>
        </div>
        <div class="yui-u">
            <div id="SubmitHelp">
                <p class="first">Please enter all the information you can about the RC club and then click <i>Submit</i>.</p>
                <p>Once the RC club information has been verified, the RC club will be added to the map and the contact email address will be notified.</p>
                <p>Please note that this process may take upto five working days so please be patient!</p>
            </div>
            <div id="NameHelp" style="display: none;">
                <p class="first">Please enter the name of the club.</p>
                <p>Please note that the name is limited to <strong>128 characters</strong>.</p>
                <p>This is a <strong>required</strong> field.</p>            
            </div>
            <div id="SiteUrlHelp" style="display: none;">
                <p class="first">If your club has a website, please enter it here.</p>
                <p>Please note that the Url is limited to <strong>128 characters</strong> and the Url must <strong>include http://</strong>.</p>            
            </div>
            <div id="ExtendedHelp" style="display: none;">
                <p class="first">Please enter the first line of the club's address.</p>
                <p>Please note that the line is limited to <strong>128 characters</strong>.</p>
                <p>This is a <strong>required</strong> field.</p>
            </div>
            <div id="StreetHelp" style="display: none;">
                <p class="first">Please enter the second line of the club's address.</p>
                <p>Please note that the line is limited to <strong>128 characters</strong>.</p>            
            </div>
            <div id="LocalityHelp" style="display: none;">
                <p class="first">Please enter the third line of the club's address.</p>
                <p>Please note that the line is limited to <strong>128 characters</strong>.</p>            
            </div>
            <div id="RegionHelp" style="display: none;">
                <p class="first">Please select the region the of club.</p>
                <p>This is a <strong>required</strong> field.</p>
            </div>
            <div id="PostcodeHelp" style="display: none;">
                <p class="first">Please enter the club's postcode.</p>
                <p>Please note that the postcode is limited to <strong>15 characters</strong> and must be <strong>valid</strong>.</p>
                <p>This is a <strong>required</strong> field.</p>
            </div>
            <div id="ClubCategoriesHelp" style="display: none;">
                <p class="first">Please select one or more categories that the club falls into.</p>
                <p>To select multiple categories, hold down <i>Crtl</i> and then select the category.</p>            
                <p>This is a <strong>required</strong> field.</p>
            </div>
            <div id="ContactNameHelp" style="display: none;">
                <p class="first">Please enter a contact name for the club.</p>
                <p>Please note that the name is limited to <strong>128 characters</strong>.</p>
                <p>This is a <strong>required</strong> field.</p>
            </div>
            <div id="EmailHelp" style="display: none;">
                <p class="first">Please enter a contact email address.</p>
                <p>This email address will receive notification when the club has been added to the map or if there are any problems. It <strong>will not</strong> be used for any other purpose.</p>
                <p>Please note that the address is limited to <strong>128 characters</strong>.</p>
                <p>This is a <strong>required</strong> field.</p>
            </div>
        </div>        
    </div>    
    <div class="actions">
        <input type="button" value="Cancel" onclick="document.location.replace('/');return false" title="Cancel the submission" />
        <asp:Button ID="SumbitButton" runat="server" OnClick="SumbitButton_Click" Text="Submit" ToolTip="Submit the club" CssClass="aw"/>
    </div>
</asp:Content>
