import { Component, Inject } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-equipments',
  templateUrl: './equipments.component.html'
})
export class EquipmentsComponent {
  public inventories: IInventory[];
  form: FormGroup;
  customerId: string = "042f780d-8b17-42f3-8c73-486d63f87e98";

  constructor(private fb: FormBuilder, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.createForm();
    http.get<IInventory[]>(baseUrl + 'api/inventories').subscribe(result => {
      this.inventories = result;
    }, error => console.error(error));
  }

  createForm() {
    this.form = this.fb.group({
      EquipmentId: ['', Validators.required],
      Days: ['', Validators.required],
      EquipmentType: ['', Validators.required]
    });
  }

  onSubmit() {
    // build a temporary user object from form values
    var transaction = <ITransaction>{};
    transaction.days = this.form.value.Days;
    transaction.equipmentId = this.form.value.EquipmentId;
    transaction.type = this.form.value.EquipmentType;
    
    var url = this.baseUrl + "api/transactions/" + this.customerId + "/transaction";
    this.http.post<ITransaction>(url, transaction)
      .subscribe(res => {
        if (res) {
          var v = res;
          alert("Transaction " + v.equipmentId + " has been created.");
          // redirect to login page
          //this.router.navigate(["login"]);
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
    //this.router.navigate([""]);
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
