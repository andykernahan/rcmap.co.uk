<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="links.aspx.cs" Inherits="Admin_Links" Title="Useful Links" %>

<asp:Content ID="UsefulLinksContent" ContentPlaceHolderID="Content" Runat="Server">
    <h2>Useful Links</h2>
    <asp:GridView 
        ID="Links" 
        runat="server" 
        AutoGenerateColumns="false"
        DataKeyNames="Id"        
        CssClass="fw">
        <Columns>
            <asp:HyperLinkField 
                HeaderText="Title" 
                DataTextField="Name" 
                DataNavigateUrlFields="Id" 
                DataNavigateUrlFormatString="~/admin/link.aspx?linkid={0}" />
            <asp:HyperLinkField
                HeaderText="Url"
                DataTextField="Url"
                DataNavigateUrlFields="Url"
                DataNavigateUrlFormatString="{0}"
                Target="_blank" />
            <asp:TemplateField HeaderText="Category">
                <ItemTemplate>
                    <%#((Link)Container.DataItem).Category.Name%>
                </ItemTemplate>
            </asp:TemplateField>            
        </Columns>        
    </asp:GridView>
    <div class="pagination">
        <span class="page-count"><%=string.Format("{0} of {1} ({2} links)", _pager.CurrentPage + 1, _pager.PageCount, _pager.RowCount)%></span>
        <div class="page-links">
            <%=!_pager.IsFirstPage ? string.Format("<a href=\"?page=0&size={0}\">first</a>", _pager.PageSize) : "<span class=\"disabled\">first</span>"%><span class="seperator">|</span><%=!_pager.IsFirstPage ? string.Format("<a href=\"?page={0}&size={1}\">prev</a>", _pager.CurrentPage - 1, _pager.PageSize) : "<span class=\"disabled\">prev</span>"%><span class="seperator">|</span><%=!_pager.IsLastPage ? string.Format("<a href=\"?page={0}&size={1}\">next</a>", _pager.CurrentPage + 1, _pager.PageSize) : "<span class=\"disabled\">next</span>"%><span class="seperator">|</span><%=!_pager.IsLastPage ? string.Format("<a href=\"?page={0}&size={1}\">last</a>", _pager.PageCount - 1, _pager.PageSize) : "<span class=\"disabled\">last</span>"%>
        </div>
        <br class="clear" />
    </div>
    <div class="actions">
        <a href="categories.aspx?type=<%=Link.CategoryType%>" title="Edit link categories">Categories</a><span class="sep">|</span><a href="link.aspx" title="Add a link">Add</a>
    </div>
</asp:Content>



