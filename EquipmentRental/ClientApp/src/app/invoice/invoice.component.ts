import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from "@angular/router";

@Component({
  selector: 'app-invoice-component',
  templateUrl: './invoice.component.html',
  styleUrls: ['./invoice.component.css']
})
export class InvoiceComponent {

  public invoice: Invoice;
  customerId: string = "042f780d-8b17-42f3-8c73-486d63f87e98";

  constructor(private router: Router, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

    http.get<Invoice>(baseUrl + "api/transactions/" + this.customerId + "/transaction").subscribe(result => {
      this.invoice = result;
    }, error => console.error(error));
  }

  onSave() {

    var url = this.baseUrl + "api/transactions/saveInvoice";
    this.http.post(url, this.invoice)
      .subscribe(res => {
        alert('The invoice stored as a text file and can be found here C:\\Invoices');
          this.router.navigate([""]);
      }, error => console.log(error));
  }

}
