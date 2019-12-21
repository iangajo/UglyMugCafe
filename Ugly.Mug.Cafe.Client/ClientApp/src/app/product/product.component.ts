import { Component, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';


@Component({
  selector: 'app-product',
  templateUrl: './product.component.html'
})

@Injectable()
export class ProductComponent {
  private cart: any[] = [];
  private products: IProduct[];
  private baseResult: IBaseResult;
  private customerName: string;

  constructor(private http: HttpClient) {
    http.get<IProduct[]>('http://localhost:63754/api/v1/product/list').subscribe(result => {
      this.products = result;
    }, error => console.error(error));

    this.customerName = localStorage.getItem("customerName");
  }

  public onSelect(selectedItem: any) {

    this.cart.forEach((value, index, array) => {

      if (value.productId === selectedItem.productId) {
        array.splice(index, 1);
      }

    });


    this.cart.push(selectedItem);
  }

  public submit() {

    let request: any = {
      customer: this.customerName,
      products: this.cart
    };


    this.http.post<IBaseResult>('http://localhost:63754/api/v1/order/add', JSON.stringify(request),
      {
        headers: new HttpHeaders({
          'Content-Type': 'application/json'
        })
      }).subscribe(result => {

        this.baseResult = result;
        //success
        if (this.baseResult.statusCode === 200) {
          this.cart = [];
        }

      }, error => console.error(error));

  }
}

interface IProduct {
  productId: number;
  name: string;
  description: string;
  quantity: number;
}

interface IBaseResult {
  statusCode: number;
  errorMessage: string;
  result: boolean;
}

