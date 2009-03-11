
if(!GMarker.prototype.GetTag) {
    GMarker.prototype.GetTag = function() { return this._tag; };
    GMarker.prototype.SetTag = function(tag) { this._tag = tag; };
}

var RcMap = Class.create({    
    initialize: function(mapElement) {
    
        this._map = null;
        this._query = null;
        this._searcher = null;
        this._geocoder = null;    
        this._markers = [];
        this.ConfigureMap(mapElement);
        this.Centre();
    },    
    ConfigureMap: function(mapElement) {
    
        this._map = new GMap2($(mapElement));
        this._map.enableScrollWheelZoom();
        this.AddMapControls();
        this.SubscribeToMapEvents();    
    },    
    AddMapControls: function() {
    
        this.GetMap().addControl(new GLargeMapControl());
        this.GetMap().addControl(new GMapTypeControl()); 
    },    
    SubscribeToMapEvents: function() {
        
    },    
    Search: function(query) {

        this._query = query;
        this.OnSearching(query);
        this.GetSearcher().execute(query);
    },    
    Searcher_ExecuteCallback: function() {
        
        var results = this.GetSearcher().results;
        
        if(results && results.length > 0) {            
            this.OnSearchComplete({Query: this._query, Success: true, Point: new GLatLng(results[0].lat, results[0].lng)});
            return;
        }            
        this.GetGeocoder().getLatLng(this._query, this.Geocoder_GetLatLngCallback.bind(this));
    },    
    Geocoder_GetLatLngCallback: function(point) {
    
        if(point) {
            this.OnSearchComplete({Query: this._query, Success: true, Point: point});
        } else {
            this.OnSearchComplete({Query: this._query, Success: false});
        }
    },    
    OnSearching: function(args) {
    
        GEvent.trigger(this, "Searching", this, args);
    },    
    OnSearchComplete: function(args) {
    
        GEvent.trigger(this, "SearchComplete", this, args);
    },    
    Centre: function() {
    
        var latLng = new GLatLng(54.876606654108684, -3.427734375);
        
        if(this.GetMap().isLoaded()) {
            this.GetMap().panTo(latLng);
            this.GetMap().setZoom(6);
        } else {     
            this.GetMap().setCenter(latLng, 6, G_HYBRID_MAP);
        }
    },    
    SetCentre: function(point, zoom) {    

        this.GetMap().setCenter(point, zoom ? zoom : 6);
    },    
    GetCentre: function() {
    
        return this.GetMap().getCenter();
    },    
    ZoomToFit: function(includeMapCentre) {
    
        if(this.GetMarkers().length === 0)
            return;
    
        var bounds;
        
        if(includeMapCentre) {
            var centre = this.GetMap().getCenter();
            bounds = new GLatLngBounds(centre, centre);
        } else {
            bounds = new GLatLngBounds();
        }        
        this.ForEachMarker(function(m) { 
            bounds.extend(m.getPoint()); 
        });                   
        this.SetCentre(bounds.getCenter(), Math.min(this.GetMap().getBoundsZoomLevel(bounds), 15));
    },    
    ApplyFilter: function(pred) {    
        
        this.ForEachMarker(function(marker) {
            marker[pred(marker) ? "show" : "hide"]();
        });   
    },    
    AddMarker: function(location) {
    
        var marker = this.CreateMarker(location);    
        
        this.GetMap().addOverlay(marker);
        
        return marker;
    },    
    AddMarkers: function(locations, clearFirst) {
    
        if(clearFirst)
            this.ClearMarkers();
        for(var i = 0, length = locations.length; i < length; ++i)
            this.AddMarker(locations[i]);
    },    
    ClearMarkers: function() {
        
        this.GetMap().clearOverlays();        
        this.GetMarkers().length = 0;
    },    
    GetSortedMarkers: function() {   
        
        var markers = this.GetMarkers();
        var mapCentre = this.GetCentre();

        markers.sort(function(a, b) { 
            return a.getPoint().distanceFrom(mapCentre) - b.getPoint().distanceFrom(mapCentre) 
        });
        
        return markers;
    },    
    ForEachMarker: function(action) {    
    
        var markers = this.GetMarkers();
        
        for(var i = 0, length = markers.length; i < length; ++i)
            action(markers[i]);
    },    
    CreateMarker: function(location) {                
        
        var point = new GLatLng(location.Pt.Lat, location.Pt.Lng);
        var marker = new GMarker(point);
        
        marker.SetTag(location);
        this.GetMarkers().push(marker);
        
        return marker;
    },    
    GetMap: function() { return this._map; },        
    GetMarkers: function() { return this._markers; },        
    GetGeocoder: function() { 
    
        if(!this._geocoder) {
            this._geocoder = new GClientGeocoder();
            this._geocoder.setBaseCountryCode("GB");
        }
        return this._geocoder; 
    },        
    GetSearcher: function() { 
            
        if(!this._searcher) {
            this._searcher = new GlocalSearch();
            this._searcher.setNoHtmlGeneration();            
            this._searcher.setCenterPoint(new GPoint(52.477964, -0.921035));
            this._searcher.setSearchCompleteCallback(this, this.Searcher_ExecuteCallback);
        }        
        return this._searcher; 
    }
});

var Countries = {
    UK: 1
};
