import { Component } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { SignalRService } from './signalr.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [SignalRService]
})


export class AppComponent {
  title = 'app';

  constructor(private hub: SignalRService) { }

  ngOnInit(): void {
    this.hub.ngOnInit();

  }

}
