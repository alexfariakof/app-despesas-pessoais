import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IUsuario } from 'src/app/shared/models';
import { AbstractService } from '../base/AbstractService';

@Injectable({
  providedIn: 'root'
})

export class UsuarioService extends AbstractService {
  constructor(public httpClient: HttpClient) {
    super('Usuario');
  }

  getUsuario(): any {
    return this.httpClient.get(`${ this.routeUrl }/GetUsuario`);
  }

  postUsuario(usuario: IUsuario): any {
    return this.httpClient.post<IUsuario>(`${ this.routeUrl }`, usuario);
  }

  putUsuario(usuario: IUsuario): any {
    return this.httpClient.put<IUsuario>(`${ this.routeUrl }`, usuario);
  }

  deleteUsuario(usuario: IUsuario): any {
    return this.httpClient.delete<IUsuario>(`${ this.routeUrl }`, { body: usuario });
  }
}
