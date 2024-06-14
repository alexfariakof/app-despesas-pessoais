import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Dayjs } from 'dayjs';
import { AbstractService } from '../base/AbstractService';

@Injectable({
  providedIn: 'root'
})

export class LancamentoService extends AbstractService {

  constructor(public httpClient: HttpClient) {
    const ROUTE = 'Lancamento';
    super(ROUTE);
  }

  getLancamentosByMesAno(mesAno: Dayjs): any {
    return this.httpClient.get(`${ this.routeUrl }/${mesAno}`);
  }
}
