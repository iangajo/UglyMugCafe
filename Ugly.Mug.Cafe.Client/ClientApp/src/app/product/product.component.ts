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
  private cartCount:number = 0;

  constructor(private http: HttpClient) {

    this.loadProducts();
    this.customerName = localStorage.getItem("customerName");
  }

  public loadProducts() {
    this.http.get<IProduct[]>('http://localhost:63754/api/v1/product/list').subscribe(result => {
      this.products = result;
    }, error => console.error(error));
  }


  public onSelect(selectedItem: any) {

    this.cart.forEach((value, index, array) => {

      if (value.productId === selectedItem.productId) {
        this.cartCount -= selectedItem.quantity;
        array.splice(index, 1);
      }

    });


    this.cart.push(selectedItem);
    this.cartCount += selectedItem.quantity;

  }

  public submit() {

    if (confirm('Are you sure you want to checkout these items?')) {
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
          this.cartCount = 0;
          this.loadProducts();
        }

        }, error => console.error(error));

      

    }
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

