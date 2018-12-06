import { Component, OnInit } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { map } from 'rxjs/operators';
// declare the leaflet variable
declare let L;
declare let mapref;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  initMap() {
    //Create and render the map
    //const map = L.map('map').setView([47.5952, -122.3316], 16);
    console.debug("initaliazing map");
    // initialize the map
    var map = L.map('map').setView([47.30, 8.52], 13);

    // load a tile layer
    L.tileLayer('https://server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}', {
      attribution: 'Tiles &copy; Esri &mdash; Source: Esri, i-cubed, USDA, USGS, AEX, GeoEye, Getmapping, Aerogrid, IGN, IGP, UPR-EGP, and the GIS User Community'
    }).addTo(map);
    return map;
    //var kmlLayer = new L.KML("/api/kml", { async: true });

    //kmlLayer.on("loaded", function (e) {
    //  map.fitBounds(e.target.getBounds());
    //});

    //map.addLayer(kmlLayer);
    // map.addLayer(tileLayer);
  }

  ngOnInit(): void {

    var mapref = this.initMap();

    console.debug("connecting to websocket");

    const connection = new signalR.HubConnectionBuilder()
      .withUrl("/chathub")
      .build();

    connection.start().catch(err => document.write(err));

    connection.on("ReceiveMessage", (username: string, message: string) => {
      let msg = JSON.parse(message);
      var circle = L.circle([msg.Position.Lat, msg.Position.Lon], {
        color: 'red',
        fillColor: '#f03',
        fillOpacity: 0.5,
        radius: 500
      }).addTo(mapref);
      console.debug(username + " " + message);
    });

  }
  title = 'MapApp';

}
