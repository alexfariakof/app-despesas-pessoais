import { FilterMesService } from './../../services/utils/filter-mes.service/filter.mes.service';
import { Component, OnInit } from '@angular/core';
import { SaldoService } from '../../services/api';
import * as dayjs from 'dayjs';
import 'dayjs/locale/pt-br';
import { Dayjs } from 'dayjs';
import { CommonModule } from '@angular/common';
import { ISaldo } from '../../models';
dayjs.locale('pt-br');
@Component({
  selector: 'app-saldo',
  standalone: true,
  templateUrl: './saldo.component.html',
  styleUrls: ['./saldo.component.scss'],
  imports: [CommonModule]
})
export class SaldoComponent implements OnInit {
  saldoAnual: number | string;
  saldoMensal: number | string;
  saldoAnualNegativo: string = 'text-danger';
  saldoMensalNegativo: string = 'text-danger';

  constructor(private saldoService: SaldoService, public filterMesService: FilterMesService) { }

  ngOnInit(): void {
    this.initialize();
  }

  initialize = (): void => {
    this.saldoService.getSaldoAnual(dayjs(dayjs().format('YYYY-01-01')))
      .subscribe({
        next: (response: ISaldo) => {
          if (response && response !== undefined && response !== null) {
            this.saldoAnualNegativo = this.isSaldoNegativo(response.saldo) ? 'text-danger' : '';
            this.saldoAnual = response.saldo.toLocaleString('pt-br', {
              style: 'currency',
              currency: 'BRL',
              minimumFractionDigits: 2,
              maximumFractionDigits: 2
            });
          }
        },
        error: () => {
          this.saldoAnual = 0;
        }
      });

    this.handleSaldoMesAno(this.filterMesService.dayJs);
  }

  handleSaldoMesAno = (mes: Dayjs): void => {
    this.saldoService.getSaldoByMesANo(mes)
      .subscribe({
        next: (response: ISaldo) => {
          if (response && response !== undefined && response !== null) {
            this.saldoMensalNegativo = this.isSaldoNegativo(response.saldo) ? 'text-danger' : '';
            this.saldoMensal = response.saldo.toLocaleString('pt-br', {
              style: 'currency',
              currency: 'BRL',
              minimumFractionDigits: 2,
              maximumFractionDigits: 2
            });
          }
        },
        error: () => {
          this.saldoMensal = 0;
        }
      });
  }

  handleSelectMes = (mes: string): void => {
    this.filterMesService.selectMonth = Number(mes);
    this.handleSaldoMesAno(dayjs(dayjs().format(`YYYY-${mes}-01`)));

  }

  private isSaldoNegativo = (saldo: number): boolean => {
    return saldo < 0;
  }
}
