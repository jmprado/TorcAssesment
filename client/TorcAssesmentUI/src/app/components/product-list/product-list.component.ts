import { AfterViewInit, Component, OnInit } from '@angular/core';
import { Product } from 'src/app/models/product';
import { GlobalService } from 'src/app/services/global-service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit, AfterViewInit {
  constructor(private productService: ProductService, private globalService: GlobalService) { }

  displayedColumns: string[] = ['id', 'name', 'price'];
  dataSource: Product[] = [];
  isLogged: boolean = false;

  ngOnInit(): void {
    this.globalService.loggedUser.subscribe(
      c => this.isLogged = c.isLogged
    )
  }

  ngAfterViewInit(): void {
    if (this.isLogged)
      this.fecthProducts();
  }

  fecthProducts(): any {
    this.productService.listProducts().then(response => {
      this.dataSource = response.data;
    });
  }
}
