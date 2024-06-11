import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IReceita } from '../../../models/IReceita';
import { AbstractService } from '../base/AbstractService';
@Injectable({
  providedIn: 'root'
})

export class ReceitaService extends AbstractService {
  constructor(public httpClient: HttpClient) {
    super();
    this.urlPath = 'Receita';
  }

  getReceitas(): any {
    return this.httpClient.get(`${ this.urlPath }`);
  }

  getReceitaById(idReceita: number): any {
    return this.httpClient.get(`${ this.urlPath }/GetById/${idReceita}`);
  }

  getReceitasCategorias(): any {
    return this.httpClient.get(`Categoria/GetByTipoCategoria/2`);
  }

  postReceita(despesa: IReceita): any {
    return this.httpClient.post<IReceita>(`${ this.urlPath }`, despesa);
  }

  putReceita(despesa: IReceita): any {
    return this.httpClient.put<IReceita>(`${ this.urlPath }`, despesa);
  }

  deleteReceita(idReceita: number): any {
    return this.httpClient.delete(`${ this.urlPath }/${idReceita}`);
  }
}
