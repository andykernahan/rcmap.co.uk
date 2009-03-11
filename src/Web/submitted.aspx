<%@ Page Language="C#" MasterPageFile="~/content.master" AutoEventWireup="true" CodeFile="submitted.aspx.cs" Inherits="Submitted" Title="RC Map - Submitted" EnableViewState="false" %>
<%@ OutputCache CacheProfile="ContentPage" VaryByParam="type" %>

<asp:Content ID="SubmittedContent" ContentPlaceHolderID="Content" Runat="Server">
    <h1>Submitted!</h1>
    <asp:MultiView ID="Views" runat="server">
        <asp:View ID="ClubSubmitted" runat="server">
            <p class="first">Thanks for submitting a club to RC Map!</p>
            <p>Once the club information has been verified, the club will be added to the map and the contact email address will be notified.</p>
            <p>Please note that this process may take upto five working days so please be patient!</p>    
        </asp:View>
        <asp:View ID="QuerySubmitted" runat="server">
            <p class="first">Thanks for submitting a query!</p>            
            <p>Whilst we will endeavour to respond as quickly as possible, it may take up to five working days so please be patient!</p>
        </asp:View>
        <asp:View ID="SubscribeRequestSubmitted" runat="server">
            <p class="first">Thanks for submitting a query!</p>            
            <p>Whilst we will endeavour to respond as quickly as possible, it may take up to five working days so please be patient!</p>
        </asp:View>
    </asp:MultiView>        
    <p class="sign-off">RC Map</p>
</asp:Content>

