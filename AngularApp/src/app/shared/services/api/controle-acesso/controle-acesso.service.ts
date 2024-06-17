import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ILogin } from '../../../models/ILogin';
import { AbstractService } from '../base/AbstractService';
import { Observable } from 'rxjs';
import { IControleAcesso } from '../../../models';

  @Injectable({
      providedIn: 'root'
  })

  export class ControleAcessoService extends AbstractService {
    constructor(public httpClient: HttpClient) {
      const ROUTE = 'ControleAcesso';
      super(ROUTE);
    }

    signIn(login: ILogin): Observable<any> {
      return this.httpClient.post<ILogin>(`${ this.routeUrl }/SignIn`, login);
    }

    createUsuario(controleAcesso: IControleAcesso): Observable<any> {
      return this.httpClient.post<IControleAcesso>(`${ this.routeUrl }`, controleAcesso);
    }

    changePassword(login: ILogin): Observable<any> {
      return this.httpClient.post<ILogin>(`${ this.routeUrl }/ChangePassword`, login);
    }

  }
