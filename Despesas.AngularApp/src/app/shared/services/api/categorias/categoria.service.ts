import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ICategoria } from '../../../models/ICategoria';
import { AbstractService } from '../base/AbstractService';
@Injectable({
  providedIn: 'root'
})

export class CategoriaService extends AbstractService {
  constructor(public httpClient: HttpClient) {
    super('Categoria');
  }

  getCategorias() : any {
    return this.httpClient.get(`${ this.routeUrl }`);
  }

  getCategoriaById(idCategoria: Number) : any {
    return this.httpClient.get(`${ this.routeUrl }/GetById/${idCategoria}`);
  }

  postCategoria(categoria: ICategoria): any {
    return this.httpClient.post<ICategoria>(`${ this.routeUrl }`, categoria);
  }

  putCategoria(categoria: ICategoria): any {
    return this.httpClient.put<ICategoria>(`${ this.routeUrl }`, categoria);
  }

  deleteCategoria(idCategoria: Number): any {
    return this.httpClient.delete(`${ this.routeUrl }/${idCategoria}`);
  }
}
