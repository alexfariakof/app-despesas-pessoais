import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IDespesa } from '../../../models/IDespesa';
import { AbstractService } from '../base/AbstractService';
@Injectable({
  providedIn: 'root'
})

export class DespesaService extends AbstractService {
  constructor(public httpClient: HttpClient) {
    const ROUTE = 'Despesa';
    super(ROUTE);
  }

  getDespesas(): any {
    return this.httpClient.get(`${ this.routeUrl }`);
  }

  getDespesaById(idDespesa: number): any {
    return this.httpClient.get(`${ this.routeUrl }/GetById/${idDespesa}`);
  }

  getDespesasCategorias(): any {
    return this.httpClient.get(`${ this.routeUrl.replace('Despesa', 'Categoria') }/GetByTipoCategoria/1`);
  }

  postDespesa(despesa: IDespesa): any {
    return this.httpClient.post<IDespesa>(`${ this.routeUrl }`, despesa);
  }

  putDespesa(despesa: IDespesa): any {
    return this.httpClient.put<IDespesa>(`${ this.routeUrl }`, despesa);
  }

  deleteDespesa(idDespesa: number): any {
    return this.httpClient.delete(`${ this.routeUrl }/${idDespesa}`);
  }
}
