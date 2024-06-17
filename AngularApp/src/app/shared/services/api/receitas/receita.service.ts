import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IReceita } from '../../../models/IReceita';
import { AbstractService } from '../base/AbstractService';

@Injectable({
  providedIn: 'root'
})

export class ReceitaService extends AbstractService {
  constructor(public httpClient: HttpClient) {
    const ROUTE = 'Receita';
    super(ROUTE);
  }

  getReceitas(): any {
    return this.httpClient.get(`${ this.routeUrl }`);
  }

  getReceitaById(idReceita: number): any {
    return this.httpClient.get(`${ this.routeUrl }/GetById/${idReceita}`);
  }

  getReceitasCategorias(): any {
    return this.httpClient.get(`${ this.routeUrl.replace('Receita', 'Categoria') }/GetByTipoCategoria/2`);
  }

  postReceita(despesa: IReceita): any {
    return this.httpClient.post<IReceita>(`${ this.routeUrl }`, despesa);
  }

  putReceita(despesa: IReceita): any {
    return this.httpClient.put<IReceita>(`${ this.routeUrl }`, despesa);
  }

  deleteReceita(idReceita: number): any {
    return this.httpClient.delete(`${ this.routeUrl }/${idReceita}`);
  }
}
