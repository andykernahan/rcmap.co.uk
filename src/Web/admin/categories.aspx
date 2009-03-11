<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="categories.aspx.cs" Inherits="Admin_Categories" Title="Club Categories" %>

<asp:Content ID="CategoriesContent" ContentPlaceHolderID="Content" Runat="Server">
    <h2><%=QsType%> Category Listing</h2>
    <div class="yui-g">
        <div class="yui-u first">
            <asp:GridView 
                ID="Categories" 
                runat="server" 
                AutoGenerateColumns="false" 
                DataKeyNames="Id" 
                OnRowEditing="Categories_RowEditing" 
                OnRowDeleting="Categories_RowDeleting">        
                <Columns>
                    <asp:BoundField 
                        HeaderText="Name" 
                        DataField="Name" />
                    <asp:BoundField 
                        HeaderText="Sort Order" 
                        DataField="SortOrder" />
                    <asp:CommandField 
                        HeaderText="Actions"
                        EditText="edit"
                        DeleteText="delete" 
                        ShowEditButton="true" 
                        ShowDeleteButton="true" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="yui-u">
            <asp:ValidationSummary ID="ValSummary" runat="server" DisplayMode="BulletList" EnableClientScript="false" HeaderText="<p class='first'>Please correct the following errors:</p>" CssClass="val-summary" />
            <div class="row">
                <asp:Label AssociatedControlID="Input" runat="server" Text="Name" />
                <asp:TextBox ID="Input" runat="server" MaxLength="64" />
                <asp:RequiredFieldValidator ID="InputReqVal" runat="server" ControlToValidate="Input" EnableClientScript="false" Display="None" ErrorMessage="Please enter a category" />
                <rcmap:MaxLengthValidator ID="InputLenVal" runat="server" ControlToValidate="Input" EnableClientScript="false" Display="None" SetFocusOnError="true" MaxLength="64" ErrorMessage="The category name is limited to 64 characters" />
            </div>
            <div class="row">
                <asp:Label AssociatedControlID="SortOrder" runat="server" Text="Sort Order" />
                <asp:TextBox ID="SortOrder" runat="server" />
                <asp:CompareValidator ID="SortOrderTVal" runat="server" ControlToValidate="SortOrder" EnableClientScript="false" Display="None" Type="String" Operator="DataTypeCheck" SetFocusOnError="true" ErrorMessage="Please enter an integer for the sort order" />
            </div>
            <div class="actions">
                <asp:LinkButton ID="Action" runat="server" Text="add" OnClick="Action_Click" CommandName="Insert" CausesValidation="true" />
            </div>            
        </div>
    </div>    
</asp:Content>

