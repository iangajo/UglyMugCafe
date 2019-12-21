import { Component, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { SignalRService } from '../signalr.service'

@Component({
  selector: 'app-barista',
  templateUrl: './barista.component.html'
})

@Injectable()
export class BaristaComponent {

  private orders: IOrder[];
  private customerName: string;
  private message: string;

  constructor(private http: HttpClient, private hub: SignalRService) {

    this.http.get<IOrder[]>('http://localhost:63754/api/v1/order/all').subscribe(result => {
      this.orders = result;

    }, error => console.error(error));

    this.customerName = localStorage.getItem("customerName");
  }

  public onProcess(selectedItem: any) {

    this.http.put('http://localhost:63754/api/v1/order/process?orderNumber=' + selectedItem.orderNumber, JSON.stringify(null),
      {
        headers: new HttpHeaders({
          'Content-Type': 'application/json'
        })
      }).subscribe(result => {
      
    }, error => console.error(error));
  }

  public onCancel(selectedItem: any) {

    this.http.put('http://localhost:63754/api/v1/order/cancel?orderNumber=' + selectedItem.orderNumber, JSON.stringify(null),
      {
        headers: new HttpHeaders({
          'Content-Type': 'application/json'
        })
      }).subscribe(result => {

    }, error => console.error(error));
  }

  public isDisabled(selectionitem: any) {
    if (selectionitem.status === 'Completed' || selectionitem.status === 'Cancelled') {
      return true;
    }
    return false;
  }

  ngOnInit(): void {

    this.hub.conn.on("BroadcastMessage", (type: string, payload: string) => {

      this.http.get<IOrder[]>('http://localhost:63754/api/v1/order/all').subscribe(result => {
        this.orders = result;

      }, error => console.error(error));

      this.message = payload;

    });
  }

}

interface IOrder {
  orderId: number;
  customer: string;
  orderNumber: string;
  orders: any[];
  orderDate: any;
  status: string;
}
