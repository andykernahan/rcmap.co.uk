<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="sandbox.aspx.cs" Inherits="Admin_Sandbox" Title="Sandbox" EnableViewState="false" %>

<asp:Content ID="SandboxContent" ContentPlaceHolderID="Content" Runat="Server">
    <div>
        <fieldset>
            <legend>Request</legend>
            <textarea id="request" cols="" rows="" style="width:100%;height:10em"></textarea>
        </fieldset>
        <fieldset>
            <legend>Response</legend>
            <textarea id="response" cols="" rows="" style="width:100%;height:20em"></textarea>
        </fieldset>
        <div class="actions">
            <input type="button" id="submit" value="send" />
        </div>
    </div>
    <script src="/js/core.js" type="text/javascript"></script>
    <script type="text/javascript" src="/services/proxy-generator.ashx?s=9F13363F-2F88-4040-9C2F-3160891572DF&h=js"></script>
    <script type="text/javascript">
    //<![CDATA[
        function createXmlRequest() {
            try {
                return new XMLHttpRequest();
            } catch(e) {}
            return new ActiveXObject("Microsoft.XMLHTTP");
        };
        $("submit").observe("click", function() {    
            $("response").setValue("");
            var http = createXmlRequest();    
            http.open("POST", RcMapClubService.SVC_ADDR, false, "", "");
            http.setRequestHeader("Content-Type","application/json");
            http.setRequestHeader("Accept","application/json");    
            http.send($("request").getValue());
            $("response").setValue(http.responseText);
        });
    //]]>
    </script>
</asp:Content>

