import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Dayjs } from 'dayjs';
import { AbstractService } from '../base/AbstractService';

@Injectable({
  providedIn: 'root'
})

export class SaldoService  extends AbstractService {
  constructor(public httpClient: HttpClient) {
    const ROUTE = 'Saldo';
    super(ROUTE);
  }

  getSaldo(): any {
    return this.httpClient.get(`${ this.routeUrl }`);
  }

  getSaldoAnual(ano: Dayjs): any {
    return this.httpClient.get(`${ this.routeUrl }/ByAno/${ano}`);
  }

  getSaldoByMesANo(mesAno: Dayjs): any {
    return this.httpClient.get(`${ this.routeUrl }/ByMesAno/${mesAno}`);
  }
}
