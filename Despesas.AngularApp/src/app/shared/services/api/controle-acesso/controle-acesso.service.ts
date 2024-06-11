import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ILogin } from '../../../models/ILogin';
import { IControleAcesso } from 'src/app/shared/models/IControleAcesso';
import { AbstractService } from '../base/AbstractService';

  @Injectable({
      providedIn: 'root'
  })

  export class ControleAcessoService extends AbstractService {
    constructor(private httpClient: HttpClient) {
      super();
      this.urlPath = 'ControleAcesso';
    }

    signIn(login: ILogin): any {
      return this.httpClient.post<ILogin>(`${ this.urlPath }/SignIn`, login);
    }

    createUsuario(controleAcesso: IControleAcesso) : any {
      return this.httpClient.post<IControleAcesso>(`${ this.urlPath }`, controleAcesso);
    }

    changePassword(login: ILogin): any {
      return this.httpClient.post<ILogin>(`${ this.urlPath }/ChangePassword`, login);
    }

  }
