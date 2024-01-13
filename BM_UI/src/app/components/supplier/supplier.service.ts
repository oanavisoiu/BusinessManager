import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Supplier } from 'src/app/shared/models/supplier/supplier.model';
import { SupplierUpdate } from 'src/app/shared/models/supplier/supplier-update.model';
import { CompanySupplier } from 'src/app/shared/models/supplier/company-supplier.model';
@Injectable({
  providedIn: 'root'
})
export class SupplierService {

  baseApiUrl:string =environment.baseApiUrl;
  constructor(private http:HttpClient) { }

  getAllSuppliers(companyId:string) : Observable<Supplier[]>{
    return this.http.get<Supplier[]>(this.baseApiUrl + '/api/companysupplier/get-company-suppliers-by-company/'+companyId);
  }
  getSupplier(id:string){
    return this.http.get<Supplier>(this.baseApiUrl + '/api/supplier/get-supplier/'+id);
  }
  updateSupplier(id:string, supplier:SupplierUpdate){
    return this.http.put<Supplier>(this.baseApiUrl + '/api/supplier/update-supplier/'+id,supplier);
  }
  addSupplier(supplier:Supplier):Observable<Supplier>{
    supplier.id='00000000-0000-0000-0000-000000000000';
    return this.http.post<Supplier>(this.baseApiUrl + '/api/supplier/add-supplier',supplier);
  }
  addCompanySupplier(companySupplier:CompanySupplier) : Observable<CompanySupplier>{
    companySupplier.id='00000000-0000-0000-0000-000000000000';
    return this.http.post<CompanySupplier>(this.baseApiUrl + '/api/companysupplier/add-company-supplier', companySupplier);
  }
  deleteSupplier(supplierId:string){
    return this.http.delete<Supplier>(this.baseApiUrl + '/api/supplier/delete-supplier/'+supplierId);
  }
}
