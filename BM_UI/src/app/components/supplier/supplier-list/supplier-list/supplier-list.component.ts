import { Component, OnInit } from '@angular/core';
import { CompanyService } from 'src/app/components/company/company.service';
import { CompanySupplier } from 'src/app/shared/models/supplier/company-supplier.model';
import { SupplierService } from '../../supplier.service';
import CustomStore from 'devextreme/data/custom_store';
import { catchError, from, of } from 'rxjs';
import { Supplier } from 'src/app/shared/models/supplier/supplier.model';
import { SupplierUpdate } from 'src/app/shared/models/supplier/supplier-update.model';

@Component({
  selector: 'app-supplier-list',
  templateUrl: './supplier-list.component.html',
  styleUrls: ['./supplier-list.component.css']
})
export class SupplierListComponent implements OnInit {

  suppliers: any;
  companySupplier: CompanySupplier = {
                id:'',
                companyId: '',
                supplierId: ''
              };

  constructor(private supplierService: SupplierService, private companyService:CompanyService) {}

  ngOnInit(): void {
    this.operations();
  }

  operations() {
    this.companyService.company$.subscribe(
      (company) => {
        if (company) {
          const dataService=this.supplierService;
          this.suppliers = new CustomStore({
            key: 'id',
            load: () => {
              return dataService.getAllSuppliers(company.id).pipe(
                catchError((error) => {
                  console.error('Error loading suppliers:', error);
                  return of([]);
                })
              ).toPromise();
            },
            update: (key, values) => {
              const supplierId = key;
              return new Promise((resolve, reject) => {
                dataService.getSupplier(supplierId).subscribe({
                  next: (supplier) => {
                    Object.assign(supplier, values);
                    const { id, ...updatedValues } = supplier;
                    const updatedSupplier=updatedValues;
                    dataService.updateSupplier(id, updatedSupplier).subscribe({
                      next: (result) => {
                        resolve(supplier);
                      },
                      error: (error) => {
                        reject(error);
                      }
                    });
                  },
                  error: (error) => {
                    reject(error);
                  }
                });
              });
            },
            insert: (values:any) => {
              return new Promise<Supplier>((resolve, reject) => {
                const newEmployee: Supplier = { ...values };

                dataService.addSupplier(newEmployee).subscribe({
                  next: (sup: Supplier) => {
                    this.companySupplier.supplierId = sup.id;
                    this.companySupplier.companyId = company.id;

                    dataService.addCompanySupplier(this.companySupplier).subscribe({
                      next: (company) => {
                        resolve(sup);
                      },
                      error: (companyError) => {
                        reject(companyError);
                      },
                    });
                  },
                  error: (empError) => {
                    reject(empError);
                  },
                });
              });
            },
            remove: (key) => {
              return new Promise<void>((resolve, reject) => {
                dataService.deleteSupplier(key).subscribe({
                  next: () => {
                    resolve();
                  },
                  error: (err) => {
                    reject(err);
                  }
                });
              });
            }
           });
         }
      }
    );
  }

}
