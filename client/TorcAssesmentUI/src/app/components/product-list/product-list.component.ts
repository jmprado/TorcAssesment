import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/models/product';
import { GlobalService } from 'src/app/services/global-service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  constructor(private productService: ProductService, private globalService: GlobalService) { }
  listProducts: Product[] = [];

  displayedColumns: string[] = ['id', 'name', 'price'];
  dataSource = this.listProducts;


  ngOnInit(): void {
    if (this.globalService.loggedUser.value.isLogged) {
      this.listProducts = this.fecthProducts();
    }
  }

  fecthProducts(): any {
    this.productService.listProducts().then(response => {
      return response.data;
    });
  }


}
