<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="Admin_Default" Title="Club Listing" EnableViewState="false" %>

<asp:Content ID="DefaultContent" ContentPlaceHolderID="Content" Runat="Server">
    <h2>Club Listing</h2>
    <asp:GridView 
        ID="Clubs" 
        runat="server" 
        AutoGenerateColumns="false" 
        DataKeyNames="Id"        
        CssClass="fw">
        <Columns>
            <asp:HyperLinkField 
                HeaderText="Name" 
                DataTextField="Name" 
                DataNavigateUrlFields="Id"
                DataNavigateUrlFormatString="~/admin/club.aspx?clubid={0}" />
            <asp:HyperLinkField 
                HeaderText="Website" 
                DataTextField="SiteUrl"
                DataNavigateUrlFields="SiteUrl"                 
                Target="_blank" />
            <asp:BoundField 
                HeaderText="Modified" 
                DataField="ModifiedOn" 
                DataFormatString="{0:g}" />
            <asp:BoundField 
                HeaderText="Modified By" 
                DataField="ModifiedBy" />                
        </Columns>        
    </asp:GridView>
    <div class="pagination">
        <span class="page-count"><%=string.Format("{0} of {1} ({2} clubs)", _pager.CurrentPage + 1, _pager.PageCount, _pager.RowCount)%></span>
        <div class="page-links">
            <%=!_pager.IsFirstPage ? string.Format("<a href=\"?page=0&size={0}\">first</a>", _pager.PageSize) : "<span class=\"disabled\">first</span>"%><span class="seperator">|</span><%=!_pager.IsFirstPage ? string.Format("<a href=\"?page={0}&size={1}\">prev</a>", _pager.CurrentPage - 1, _pager.PageSize) : "<span class=\"disabled\">prev</span>"%><span class="seperator">|</span><%=!_pager.IsLastPage ? string.Format("<a href=\"?page={0}&size={1}\">next</a>", _pager.CurrentPage + 1, _pager.PageSize) : "<span class=\"disabled\">next</span>"%><span class="seperator">|</span><%=!_pager.IsLastPage ? string.Format("<a href=\"?page={0}&size={1}\">last</a>", _pager.PageCount - 1, _pager.PageSize) : "<span class=\"disabled\">last</span>"%>
        </div>
        <br class="clear" />
    </div>
    <div class="actions">
        <a href="categories.aspx?type=<%=Club.CategoryType%>" title="Edit club categories">Categories</a><span class="sep">|</span><a href="club.aspx" title="Add a club">Add</a>
    </div>
</asp:Content>

