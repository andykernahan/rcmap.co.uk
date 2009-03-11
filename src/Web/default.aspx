<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_Default" EnableViewState="false" EnableSessionState="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head>
    <title id="windowTitle" runat="server">RC Map - Your Map to RC!</title>
    <meta name="keywords" content="RcMap, RC Map, Clubs, Tracks, Club Search, Track Search, Club Map, Track Map, RC Cars, RC Cars, RC Bikes, RC Bikes" />
    <meta id="metaDescription" runat="server" name="description" content="RC Map - Your map for finding RC clubs in your area." />
    <link rel="alternate" type="application/rss+xml" href="http://feeds.rcmap.co.uk/rcmap/locations" title="RC Map - Location Feed" />
    <link rel="stylesheet" type="text/css" href="/css/base.css" media="screen" />
    <link rel="stylesheet" type="text/css" href="/css/map-page.css" media="screen" />
<!--[if IE]>
	<style type="text/css" media="screen">@import "/css/map-page-ie.css";</style>
<![endif]-->
    <script src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=<%=Configuration.MapApiKey%>" type="text/javascript"></script>
    <script src="http://www.google.com/uds/api?file=uds.js&amp;v=1.0&amp;source=uds-msw&amp;key=<%=Configuration.GeoApiKey%>" type="text/javascript"></script>
    <script type="text/javascript" src="/js/core.js"></script>
    <script type="text/javascript" src="/js/rcmap.js"></script>
    <script type="text/javascript" src="/services/proxy-generator.ashx?s=9F13363F-2F88-4040-9C2F-3160891572DF&amp;h=js"></script>
    <script type="text/javascript" src="/js/map-page.js"></script>
</head>
<body>
    <noscript>
        <p class="ct">JavaScript must be enabled in order for you to use RC Map. However, it seems JavaScript is either disabled or not supported by your browser.</p>
        <p class="ct">To view RC Map, enable JavaScript by changing your browser options, and then <a href="">try</a> again.</p>
    </noscript>
    <div id="page" style="display:none;">
        <div id="logo">
            <h1 onclick="window.location.replace('/default.aspx');return false">rcmap.co.uk</h1>
        </div>
        <ul id="nav">
            <li><a href="/about.aspx" title="About RC Map">About</a></li>
            <li class="sep">|</li>
            <li><a href="/clubs.aspx" title="View the complete RC club list">Clubs</a></li>
            <li class="sep">|</li>
            <li><a href="/submit.aspx" title="Submit a RC club to RC Map">Submit</a></li>
            <li class="sep">|</li>
            <li><a class="feed" href="http://feeds.rcmap.co.uk/rcmap/locations" title="Subscribe to RC Map's RSS Feed">Feed</a></li>
        </ul>
        <br class="clear" />
        <div id="lhs">
            <form action="javascript:Search();" id="search">
                <div class="criteria">
                    <div class="criterion">
                        <label for="query">Search:</label>
                        <input type="text" id="query" tabindex="10"/>
                        <label for="query" class="inline">Radius:</label>
                        <select id="radius" tabindex="20">
                            <option value="10">10</option>
                            <option value="20">20</option>
                            <option value="30" selected="selected">30</option>
                            <option value="40">40</option>
                            <option value="50">50</option>
                        </select>
                        miles
                    </div>
                    <div class="criterion">
                        <label for="club-category">Type:</label>
                        <select id="club-category" tabindex="30">
                            <option value=""></option>
                            <asp:Repeater ID="ClubCategoryOptions" runat="server">
                                <ItemTemplate><option value="<%#Eval("Id")%>"><%#HtmlEncode((string)Eval("Name"))%></option></ItemTemplate>
                            </asp:Repeater>
                        </select>
                    </div>
                </div>
                <div class="actions">
                    <a href="javascript:void(0)" onclick="ShowAll();return false" tabindex="50" title="Show all clubs">Show All</a><span class="sep">|</span><a href="javascript:void(0)" onclick="Search();return false" tabindex="40" title="Search for clubs">Search</a>
                </div>
            </form>
            <div id="scolling-container">
                <div id="detail-list">
                    <div class="msg">
                        <h3>Welcome to RC Map!</h3>
                        <p class="first">To get started, enter your search criteria above and click search. Your search may be a postcode such as <q>L13</q> or the name of a town or city, such as <q>Southport</q>.</p>
                        <p>You may also select to search for RC clubs of a specific type, such as 1:10 Electric Indoor or 1:8 Rallycross.</p>
                    </div>
                </div>
            </div>
        </div>
        <div id="map"></div>
    </div>
    <asp:Panel ID="Analytics" runat="server">
        <script type="text/javascript">
        //<![CDATA[
            var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
            document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
        //]]>
        </script>
        <script type="text/javascript">
        //<![CDATA[
            var _pageTracker = _gat._getTracker("<%=Configuration.AnalyticsKey%>");
            _pageTracker._initData();
            _pageTracker._trackPageview();
        //]]>
        </script>
    </asp:Panel>
    <form id="aspnetForm" runat="server"></form>
</body>
</html>