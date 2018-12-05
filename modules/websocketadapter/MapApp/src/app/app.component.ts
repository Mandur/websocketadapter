import { Component, OnInit } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { map } from 'rxjs/operators';
// declare the leaflet variable
declare let L;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  initMap() {
    //Create and render the map
    //const map = L.map('map').setView([47.5952, -122.3316], 16);
    var map = L.map('map').setView([51.505, -0.09], 13);

    //var tileLayer = L.tileLayer('assets/tiles/{z}/{x}/{y}.png', {
    //  cursor: true,
    //  minZoom: 0,
    //  maxZoom: 19,
    //  attribution: '© <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
    //});

    //var kmlLayer = new L.KML("/api/kml", { async: true });

    //kmlLayer.on("loaded", function (e) {
    //  map.fitBounds(e.target.getBounds());
    //});

    //map.addLayer(kmlLayer);
   // map.addLayer(tileLayer);
  }

    ngOnInit(): void {
      console.debug("initaliazing map");
      this.initMap();
      console.debug("connecting to websocket");
      const connection = new signalR.HubConnectionBuilder()
        .withUrl("/chathub")
        .build();

      connection.start().catch(err => document.write(err));

      connection.on("messageReceived", (username: string, message: string) => {
     

        console.debug(username + " " + message);
      });

    }
  title = 'MapApp';

}
