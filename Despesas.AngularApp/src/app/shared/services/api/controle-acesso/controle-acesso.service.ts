import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ILogin } from '../../../models/ILogin';
import { IControleAcesso } from 'src/app/shared/models/IControleAcesso';
import { AbstractService } from '../base/AbstractService';
import { Observable } from 'rxjs';

  @Injectable({
      providedIn: 'root'
  })

  export class ControleAcessoService extends AbstractService {
    constructor(private httpClient: HttpClient) {
      super('ControleAcesso');
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
