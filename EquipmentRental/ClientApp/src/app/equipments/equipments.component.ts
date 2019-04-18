import { Component, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from "@angular/router";

@Component({
  selector: 'app-equipments',
  templateUrl: './equipments.component.html'
})
export class EquipmentsComponent {
  public inventories: IInventory[];
  form: FormGroup;
  transaction = <ITransaction>{};

  customerId: string = "042f780d-8b17-42f3-8c73-486d63f87e98";

  constructor(private router: Router, private fb: FormBuilder, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.createForm();
    http.get<IInventory[]>(baseUrl + 'api/inventories').subscribe(result => {
      this.inventories = result;
    }, error => console.error(error));
  }

  createForm() {
    this.form = this.fb.group({
      
      Days: ['', Validators.required]
     
    });
  }

  onSubmit() {
    // build a temporary user object from form values
    this.transaction.days = this.form.value.Days;
    //this.transaction.equipmentId = this.form.value.EquipmentId;
    //this.transaction.type = this.form.value.EquipmentType;
    
    var url = this.baseUrl + "api/transactions/" + this.customerId + "/transaction";
    this.http.post<ITransaction>(url, this.transaction)
      .subscribe(res => {
        if (res) {
          var v = res;
          // redirect to Invoice page
          this.router.navigate(["invoice"]);
        }
        else {
          // registration failed
          this.form.setErrors({
            "Transaction": "Transactionn failed."
          });
        }
      }, error => console.log(error));
  }
  onBack() {
    this.router.navigate([""]);
  }

  onSelect(inventory: IInventory) {
    this.transaction.equipmentId = inventory.inventoryID;
    this.transaction.type = inventory.type;

    alert('You choosed one item, please Enter the number of days below');
  }

  // retrieve a FormControl
  getFormControl(name: string) {
    return this.form.get(name);
  }
  // returns TRUE if the FormControl is valid
  isValid(name: string) {
    var e = this.getFormControl(name);
    return e && e.valid;
  }
  // returns TRUE if the FormControl has been changed
  isChanged(name: string) {
    var e = this.getFormControl(name);
    return e && (e.dirty || e.touched);
  }
  // returns TRUE if the FormControl is invalid after user changes
  hasError(name: string) {
    var e = this.getFormControl(name);
    return e && (e.dirty || e.touched) && !e.valid;
  }
}

interface IInventory {
  inventoryID: number;
  name: number;
  type: string;
}
