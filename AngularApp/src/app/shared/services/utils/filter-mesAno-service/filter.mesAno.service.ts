import { Injectable } from '@angular/core';
import dayjs from 'dayjs';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FilterMesAnoService {
  private _dataMesAno: BehaviorSubject<string>;

  constructor() {
    const storedMonthYear = localStorage.getItem('selectedMonthYear');
    const initialMonthYear = storedMonthYear || dayjs().format('YYYY-MM');
    this._dataMesAno = new BehaviorSubject<string>(initialMonthYear);
  }

  get dataMesAno$() {
    return this._dataMesAno.asObservable();
  }

  get dataMesAno(): string {
    return this._dataMesAno.getValue();
  }

  set dataMesAno(value: string) {
    this._dataMesAno.next(value);
    localStorage.setItem('selectedMonthYear', value);
  }
}
