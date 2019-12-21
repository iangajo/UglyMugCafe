import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';

@Injectable()
export class SignalRService {
  public conn: signalR.HubConnection;

  constructor() { }

  ngOnInit(): void {

    this.conn = new signalR.HubConnectionBuilder().configureLogging(signalR.LogLevel.Information)
      .withUrl("http://localhost:50772/notify")
      .build();

    this.conn.start().then(function () {
      console.log('Connected!');
    }).catch(function (err) {
      return console.error(err.toString());
    });


    //const connection = new signalR.HubConnectionBuilder().configureLogging(signalR.LogLevel.Information)
    //  .withUrl("http://localhost:50772/notify")
    //  .build();

    //connection.start().then(function () {
    //  console.log('Connected!');
    //}).catch(function (err) {
    //  return console.error(err.toString());
    //});




    //this.conn.on("BroadcastMessage", (type: string, payload: string) => {
    //  alert('received');
    //  //this.order.orders = [];
    //  //this.order.loadData(this.http);

    //});


  }
}
