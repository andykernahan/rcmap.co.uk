var _templates = {
    "vcard.fn": new Template("<div class=\"fn org\">#{Name}</div>"),        
    "vcard.adr": new Template("<div class=\"adr\">#{Content}</div>"),
    "vcard.adr.Ext": new Template("<div class=\"extended-address\">#{Ext}</div>"),
    "vcard.adr.St": new Template("<div class=\"street-address\">#{St}</div>"),
    "vcard.adr.Loc": new Template("<div class=\"locality\">#{Loc}</div>"),
    "vcard.adr.Reg": new Template("<div class=\"region\">#{Reg}</div>"),
    "vcard.adr.Pst": new Template("<div class=\"postal-code\">#{Pst}</div>"),    
    "vcard.geo": new Template("<span class=\"geo\"><label>Coordinates</label><span class=\"latitude\">#{Lat}</span>, <span class=\"longitude\">#{Lng}</span></span>"),    
    "vcard.orgurl" : new Template("<a class=\"url org\" href=\"#{Url}\" target=\"_blank\" title=\"Open #{Url} in a new window\">#{Url}</a>"),
    "vcard.cat": new Template("<a href=\"javascript:void(0)\" onclick=\"ShowAllInCategory('#{Name}');return false\" title=\"Show all #{Name} clubs in the country\">#{Name}</a>"),
    "vcard.cats": new Template("<div class=\"club-categories\"><label>Type</label>#{Cats}</div>"),    
    "vcard.dir" : new Template("Get directions: <a href=\"http://maps.google.co.uk/maps?&hl=en&daddr=#{UriSafeName}@#{Pt.Lat},#{Pt.Lng}&sll=#{Pt.Lat},#{Pt.Lng}&z=10\" target=\"_blank\" title=\"Get directions to #{Name}\">to here</a> - <a href=\"http://maps.google.co.uk/maps?&hl=en&saddr=#{UriSafeName}@#{Pt.Lat},#{Pt.Lng}&sll=#{Pt.Lat},#{Pt.Lng}&z=10\" target=\"_blank\" title=\"Get directions from #{Name}\">from here</a>"),
    "msg.incompat": new Template("<p class=\"ct\">Unfortunately your browser is not compatible with Google Maps and cannot display the content of this site.</p><p class=\"ct\">We highly recommend downloading the latest version of your browser or downloading another one, such as <a href=\"http://www.mozilla.com/firefox/\">Firefox</a>.</p></div>"),
    "msg.geofailed": new Template("<div class=\"msg\"><p>We were unable to find <strong>#{Query}</strong> on the map, please try the following:<ul><li>Check the spelling of your search</li><li>If applicable, try entering the name of a town or city as apposed to a region</li></ul></div>"),
    "msg.rpcfailed": new Template("<div class=\"msg\"><strong>RPC failure:</strong><br /><br />#{Error}</div>"),
    "msg.searchfailed": new Template("<div class=\"msg\"><p>We were unable to find any clubs that match your search, please try the following:</p><ul><li>Broaden your search by increasing its radius</li><li>If applicable, try entering the name of a town or city as apposed to a region</li></ul><p>Alternatively, if you know of a club in this area; please <a href=\"/submit.aspx?query=#{Query}&radius=#{Radius}&cat=#{Cat}\" title=\"Submit a club\">submit</a> it and we will add it to the map!</p></div>"),
    "msg.loading": new Template("<p class=\"busy\">Loading...</p>"),
    "msg.searching": new Template("<p class=\"busy\">Searching...</p>"),
    "menu.ctx.centre": new Template("Centre map here"),
    "menu.ctx.search": new Template("Search in this area"),
    "menu.ctx.zoomin": new Template("Zoom in"),    
    "menu.ctx.zoomout": new Template("Zoom out")    
};
var _map;
var _clubService;
var _categoryMap;

Event.observe(document, "dom:loaded", OnDomLoaded);
Event.observe(window, "unload", OnWindowUnload);
Event.observe(window, "resize", OnWindowResize);

function OnDomLoaded() {                                    
    
    if(InitUI()) {
        InitMap();
        InitServices();
        InitCategoryMap();
        DisplayInitialClubs();        
    }
}    

function OnWindowUnload() {

    GUnload();
}   

function OnWindowResize() {

    var map = $("map");    
    var lhs = $("scolling-container");
    
    map.style.height = (document.viewport.getHeight() - map.cumulativeOffset().top - 8) + "px";
    lhs.style.height = (document.viewport.getHeight() - lhs.cumulativeOffset().top - 7) + "px";    
}

function InitUI() {

    if(GBrowserIsCompatible()) {
        $("page").show();    
        OnWindowResize();
        $("query").focus();   
        return true;
    } else {
        $("page").update(_templates["msg.incompat"].evaluate(window)).show();
        return false;
    }
}

function InitMap() {

    _map = new RcMap($("map"));
    GEvent.addListener(_map, "SearchComplete", RcMap_SearchComplete);
    _map.Centre();    
    AddMapContextMenu();
}

function InitServices() {

    _clubService = new RcMapClubService();
}

function InitCategoryMap() {

    var options = $("club-category").options;
    
    _categoryMap = {};
    for(var i = 0, l = options.length; i < l; ++i) {
        _categoryMap[options[i].value] = options[i].text;
    }
}

function DisplayInitialClubs() {

    if(window._initialClubs) {        
        AddLocationsToMap(_initialClubs);
        _map.ZoomToFit();
        SyncDetailListWithMap();
        if(_initialClubs.length === 1) {
            GEvent.trigger(_map.GetMarkers()[0], "click");
        }
    }
}

function FindNearby(lat, lng, radius, categories) {

    _clubService.Search(lat, lng, radius, categories, 
        function(response) {            
            if(!response.error) {
                if(response.result.length > 0) {
                    AddLocationsToMap(response.result);
                    SyncDetailListWithMap();
                    if(lat !== 0 || lng !== 0) {
                        _map.SetCentre(new GLatLng(lat, lng));
                        ZoomToFit(true);
                    } else {
                        ZoomToFit(false);
                    }                                        
                } else {
                    OnSearchFailed();
                }
            } else {
                OnRpcFailed(response.error);
            }                    
        }                        
    );         
}

function Search() {
    
    var q = $F("query").strip();
    var c = $F("club-category");
    
    if(q !== "") {
        ShowAsSearching();
        _map.Search(q);
    } else if(c !== "") {
        ShowAsSearching();
        FindNearby(0, 0, 0, [parseInt(c)]);
    } else {
        $("query").focus();
    }
}

function RcMap_SearchComplete(sender, e) { 
    
    if(e.Success) {
        FindNearby(e.Point.lat(), e.Point.lng(), parseInt($F("radius")), [parseInt($F("club-category")) || null].compact());
    } else {        
        OnGeocodeFailed();
    }
}

function ShowAll() {    
    
    ShowAsLoading();    
    $("search").reset();    
    _clubService.FindByCountry(Countries.UK, function(response) {        
        if(!response.error) {                    
            AddLocationsToMap(response.result);
            ZoomToFit(false);
            SyncDetailListWithMap();
        } else {
            OnRpcFailed(response.error);
        }                    
    });
}

function SyncDetailListWithMap() {
    
    $("detail-list").update();
    _map.GetSortedMarkers().each(AddMarkerToDetailList);
}

function AddLocationsToMap(locations) {      

    locations.each(function(l) {
        SubscribeToMarkerEvents(_map.AddMarker(l));
    });
}

function SubscribeToMarkerEvents(marker) {

    GEvent.addListener(marker, "click", function() {
        var detail = $("detail_" + marker.GetTag().Id);
        // This may seem odd doing this here but at least the logic is centralised.
        if(!detail.hasClassName("selected")) {
            marker.openInfoWindow(GetInfoWindowContent(marker.GetTag()));
            detail.addClassName("selected");
            TrackClubView(marker.GetTag().Id);
        } else {            
            _map.GetMap().closeInfoWindow();
        }
    });
    GEvent.addListener(marker, "infowindowclose", function() {
        $("detail_" + marker.GetTag().Id).removeClassName("selected");
    });
}

function GetInfoWindowContent(location) {

    if(location.$GetInfoWindowContent) {
        return location.$GetInfoWindowContent;
    }
    
    var detail = $E("div", {className: "info-window-content vcard"});        
    
    detail.insert(_templates["vcard.fn"].evaluate(location));    
    if(location.Url) {        
        detail.insert(_templates["vcard.orgurl"].evaluate(location));
    }
    detail.insert(FormatAddress(location.Addr, ",<br />"));
    detail.insert(_templates["vcard.geo"].evaluate(location.Pt));    
    detail.insert(_templates["vcard.cats"].evaluate({Cats: MapCategories(location.Cats).join(", ")}));
    location.UriSafeName = encodeURIComponent ? encodeURIComponent(location.Name) : escape(location.Name);
    $E("div", {className: "actions"}, detail).insert(_templates["vcard.dir"].evaluate(location));
    
    location.$GetInfoWindowContent = detail;
    
    return detail;
}

function TrackClubView(id) {

    if(window._pageTracker) {        
        //_pageTracker._trackPageview("/club/iwv/" + id);
    }
}

function AddMarkerToDetailList(marker) {    
    
    var location = marker.GetTag();
    var detail = $E("div", {id: "detail_" + location.Id, className: "detail vcard"}, $("detail-list"));    
    
    detail.observe("click", function() { 
        GEvent.trigger(marker, "click"); 
    });    
    detail.insert(_templates["vcard.fn"].evaluate(location));    
    detail.insert(FormatAddress(location.Addr, ", "));
    detail.insert(_templates["vcard.cats"].evaluate({Cats: MapCategories(location.Cats).join(", ")}));
    
    return detail;            
}

function MapCategories(cats) {

    if(cats.$MapCategories) {
        return cats.$MapCategories;
    }
    
    var mapped = [];
    
    cats.each(function(cat, index) {
        mapped[index] = _categoryMap[cat.toString()] || "";        
    });    
    cats.$MapCategories = mapped;
        
    return mapped;
}

function FormatAddress(addr, sep) {

    var components = [];
    
    ["Ext", "St", "Loc", "Reg", "Pst"].each(function(prop) {
        var value = addr[prop];
        if(value && value !== "") {            
            components.push(_templates["vcard.adr." + prop].evaluate(addr));
        }
    });    
    return _templates["vcard.adr"].evaluate({Content: components.join(sep)});
}

function AddMapContextMenu() {    

    var gmap = _map.GetMap();
    var menu = $E("div", {id: "context-menu"}).hide();    
    
    menu.getLatLng = function() {
        return gmap.fromContainerPixelToLatLng(new GPoint(this.offsetLeft, this.offsetTop));
    };
    // Zoom map in.
    $E("div", {className: "item"}, menu).observe("click", function() {
        var point = menu.getLatLng();
        menu.hide();
        gmap.setCenter(point, gmap.getZoom() + 1);
    }).insert(_templates["menu.ctx.zoomin"].evaluate(window));
    // Zoom map out.
    $E("div", {className: "item"}, menu).observe("click", function() {
        var point = menu.getLatLng();
        menu.hide();
        gmap.setCenter(point, gmap.getZoom() - 1);
    }).insert(_templates["menu.ctx.zoomout"].evaluate(window));
    // Centre map here.
    $E("div", {className: "item"}, menu).observe("click", function() {        
        var point = menu.getLatLng();
        menu.hide();
        gmap.panTo(point);
    }).insert(_templates["menu.ctx.centre"].evaluate(window));
    // Seperator.
    $E("div", {className: "item-sep"}, menu)
    // Search in this area.
    $E("div", {className: "item"}, menu).observe("click", function() {
        var point = menu.getLatLng();
        menu.hide();
        ShowAsSearching();
        $("search").reset();        
        FindNearby(point.lat(), point.lng(), 30, []);
    }).insert(_templates["menu.ctx.search"].evaluate(window));

    // http://econym.googlepages.com/context.htm
    GEvent.addListener(gmap, "singlerightclick", function(pixel, tile) {        
        var x = pixel.x;
        var y = pixel.y;
        if(x > gmap.getSize().width - 140) { 
            x = gmap.getSize().width - 140;
        }
        if(y > gmap.getSize().height - 100) { 
            y = gmap.getSize().height - 100;
        }
        (new GControlPosition(G_ANCHOR_TOP_LEFT, new GSize(x, y))).apply(menu);        
        menu.show();
    });
    GEvent.addListener(gmap, "click", function() {
        menu.hide();
    });
    gmap.getContainer().appendChild(menu);
}

function ZoomToFit(includeMapCentre) {    

    _map.ZoomToFit(includeMapCentre !== undefined ? includeMapCentre : true);
}

function ShowAsSearching() {
    
    _map.ClearMarkers()    
    $("detail-list").update(_templates["msg.searching"].evaluate(window));
}

function ShowAsLoading() {        
    
    _map.ClearMarkers();
    $("detail-list").update(_templates["msg.loading"].evaluate(window));
}

function OnGeocodeFailed() {    
    
    var args = {Query: $F("query").escapeHTML()};
    $("detail-list").update(_templates["msg.geofailed"].evaluate(args));
}

function OnRpcFailed(error) {
    
    var args = {Error: Object.toJSON(error).escapeHTML()};
    $("detail-list").update(_templates["msg.rpcfailed"].evaluate(args));
}

function OnSearchFailed() {    
    
    var args = {Query: $F("query").escapeHTML(), Radius: $F("radius"), Cat: $F("club-category").escapeHTML()};
    $("detail-list").update(_templates["msg.searchfailed"].evaluate(args));
}