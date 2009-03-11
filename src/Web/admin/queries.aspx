<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="queries.aspx.cs" Inherits="Admin_Queries" Title="Queries Submitted" %>

<asp:Content ID="QueriesContent" ContentPlaceHolderID="Content" Runat="Server">
    <h2>Queries Submitted</h2>
    <asp:GridView 
        ID="Queries" 
        runat="server" 
        AutoGenerateColumns="false"
        DataKeyNames="Id"        
        CssClass="fw">
        <Columns>
            <asp:HyperLinkField 
                HeaderText="Submitted By" 
                DataTextField="UserName" 
                DataNavigateUrlFields="Id" 
                DataNavigateUrlFormatString="~/admin/query.aspx?queryid={0}" />
            <asp:TemplateField HeaderText="Content">
                <ItemTemplate>
                    <%#GetQueryText((Query)Container.DataItem)%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Category">
                <ItemTemplate>
                    <%#((Query)Container.DataItem).Category.Name%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField 
                HeaderText="Created" 
                DataField="CreatedOn" 
                DataFormatString="{0:g}" />            
        </Columns>        
    </asp:GridView>
    <div class="actions">
        <a href="categories.aspx?type=<%=Query.CategoryType%>" title="Edit query categories">Categories</a>
    </div>
</asp:Content>

