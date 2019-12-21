import { Component } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { SignalRService } from '../signalr.service'

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  private orders: IOrder[];
  constructor(private http: HttpClient, private hub: SignalRService) {
    this.http.get<IOrder[]>('http://localhost:63754/api/v1/order/all').subscribe(result => {
      this.orders = result;

    }, error => console.error(error));
  }

  ngOnInit(): void {

    this.hub.conn.on("BroadcastMessage", (type: string, payload: string) => {

      if (type === 'Add' || type === 'Update' || type === 'Cancel' || type === 'Processed') {

        this.http.get<IOrder[]>('http://localhost:63754/api/v1/order/all').subscribe(result => {
          this.orders = result;
        }, error => console.error(error));

      }


    });
  }

  public saveCustomerName(customer: string) {
    localStorage.setItem("customerName", customer);

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
