import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Dayjs } from 'dayjs';
import { AbstractService } from '../base/AbstractService';

@Injectable({
  providedIn: 'root'
})

export class SaldoService  extends AbstractService {
  constructor(public httpClient: HttpClient) {
    super();
    this.urlPath = 'Saldo';
  }

  getSaldo(): any {
    return this.httpClient.get(`${ this.urlPath }`);
  }

  getSaldoAnual(ano: Dayjs): any {
    return this.httpClient.get(`${ this.urlPath }/ByAno/${ano}`);
  }

  getSaldoByMesANo(mesAno: Dayjs): any {
    return this.httpClient.get(`${ this.urlPath }/ByMesAno/${mesAno}`);
  }
}
