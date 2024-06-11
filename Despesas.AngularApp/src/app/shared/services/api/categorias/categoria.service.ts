import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ICategoria } from '../../../models/ICategoria';
import { AbstractService } from '../base/AbstractService';
@Injectable({
  providedIn: 'root'
})

export class CategoriaService extends AbstractService {
  constructor(public httpClient: HttpClient) {
    super();
    this.urlPath = 'Categoria';
  }

  getCategorias() : any {
    return this.httpClient.get(`${ this.urlPath }`);
  }

  getCategoriaById(idCategoria: Number) : any {
    return this.httpClient.get(`${ this.urlPath }/GetById/${idCategoria}`);
  }

  postCategoria(categoria: ICategoria): any {
    return this.httpClient.post<ICategoria>(`${ this.urlPath }`, categoria);
  }

  putCategoria(categoria: ICategoria): any {
    return this.httpClient.put<ICategoria>(`${ this.urlPath }`, categoria);
  }

  deleteCategoria(idCategoria: Number): any {
    return this.httpClient.delete(`${ this.urlPath }/${idCategoria}`);
  }
}
