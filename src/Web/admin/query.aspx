<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="query.aspx.cs" Inherits="Admin_Query" Title="Query" EnableViewState="false" %>

<asp:Content ID="QueryContent" ContentPlaceHolderID="Content" Runat="Server">
    <h2>Query submitted by <asp:Literal ID="Name" runat="server" /></h2>
    <div class="row"><label>Name</label><div class="value"><asp:Literal ID="UserName" runat="server" /></div></div>
    <div class="row"><label>Email</label><div class="value"><asp:Literal ID="UserEmail" runat="server" /></div></div>
    <div class="row"><label>Category</label><div class="value"><asp:Literal ID="Category" runat="server" /></div></div>
    <div class="row"><label>Content</label><div class="value"><asp:Literal ID="Content" runat="server" /></div></div>
    <div class="actions">
        <asp:LinkButton ID="DeleteButton" runat="server" OnClick="DeleteButton_Click" ToolTip="Delete this query" Text="Delete" /><span class="sep">|</span><a href="queries.aspx" title="Back to queries">Back</a>
    </div>
    <div class="audit"><div><asp:Literal ID="Audit" runat="server" /></div></div>
</asp:Content>

