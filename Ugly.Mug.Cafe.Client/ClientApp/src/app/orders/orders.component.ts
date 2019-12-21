import { Component, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { SignalRService } from '../signalr.service'

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html'
})

@Injectable()
export class OrdersComponent {

  private myOrders: IOrder[];
  private orders: IOrder[];
  private products: IProduct[];
  private cart: any[] = [];
  private orderNumber: string;
  private customer: string;
  private message: string;

  constructor(private http: HttpClient, private hub: SignalRService) {

    this.customer = localStorage.getItem("customerName");

    this.http.get<IOrder[]>('http://localhost:63754/api/v1/order/' + this.customer).subscribe(result => {
      this.orders = result;

    }, error => console.error(error));
  }

  public onModify(selectedItem: any) {

    this.http.get<IProduct[]>('http://localhost:63754/api/v1/order?orderNumber=' + selectedItem.orderNumber).subscribe(result => {
      this.products = result;

      if (this.products.length > 0) {
        this.products.forEach((value, index, array) => {
          
          if (value.quantity > 0) {
            this.cart.push(value);
          }

        });
      }

    }, error => console.error(error));

    this.orderNumber = selectedItem.orderNumber;
    
    

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

  public onSelectUpdate(selectedItem: any) {

    this.cart.forEach((value, index, array) => {

      if (value.productId === selectedItem.productId) {
        array.splice(index, 1);
      }

    });


    this.cart.push(selectedItem);
  }

  public submit() {

    let request: any = {
      orderNumber: this.orderNumber,
      customerId: this.customer,
      products: this.cart
    };


    this.http.put('http://localhost:63754/api/v1/order/update', JSON.stringify(request),
      {
        headers: new HttpHeaders({
          'Content-Type': 'application/json'
        })
      }).subscribe(result => {

      }, error => console.error(error));

    this.cart = [];
    this.products = null;
  }

  public cancelSubmit() {
      this.cart = [];
      this.products = null;
  }

  ngOnInit(): void {

    this.hub.conn.on("BroadcastMessage", (type: string, payload: string) => {

      this.http.get<IOrder[]>('http://localhost:63754/api/v1/order/' + this.customer).subscribe(result => {
        this.orders = result;
      }, error => console.error(error));

      this.message = payload;

    });
  }


}

//interfaces
interface IOrder {
  orderId: number;
  customer: string;
  orderNumber: string;
  orders: any[];
  orderDate: any;
  status: string;
}

interface IProduct {
  productId: number;
  name: string;
  description: string;
  quantity: number;
}
