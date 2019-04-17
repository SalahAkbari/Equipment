import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-equipments',
  templateUrl: './equipments.component.html'
})
export class EquipmentsComponent {
  public inventories: IInventory[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<IInventory[]>(baseUrl + 'api/inventories').subscribe(result => {
      this.inventories = result;
    }, error => console.error(error));
  }
}

interface IInventory {
  inventoryID: number;
  name: number;
  type: string;
}
