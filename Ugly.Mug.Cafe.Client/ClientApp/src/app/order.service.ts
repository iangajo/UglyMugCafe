import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable()
export class OrderService {

  public orders: IOrder[];
  constructor(private http: HttpClient) { }

  public getOrders() {

    this.http.get<IOrder[]>('http://localhost:63754/api/v1/order/1').subscribe(result => {
      this.orders = result;

    }, error => console.error(error));

    return this.orders;
  }
}

interface IOrder {
  orderId: number;
  customerId: number;
  orderNumber: string;
  orders: any[];
  orderDate: any;
  isProcessed: boolean;
}
