import { Injectable } from '@angular/core';
import dayjs from 'dayjs';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FilterMesService {
  private _selectMonth: BehaviorSubject<number>;
  private _selectMonthExt: string;
  private _dayJs: dayjs.Dayjs;

  constructor() {
    const storedMonth = sessionStorage.getItem('selectedMonth');
    const initialMonth = storedMonth ? Number(storedMonth) : Number(dayjs().format('MM'));

    this._selectMonth = new BehaviorSubject<number>(initialMonth);
    this._selectMonthExt = dayjs().set('month', initialMonth - 1).format('MMMM') as string;
    this._dayJs = dayjs(dayjs().format(`YYYY-${initialMonth}-01`));
  }

  get selectMonth$() {
    return this._selectMonth.asObservable();
  }

  get selectMonth(): number {
    return this._selectMonth.getValue();
  }

  set selectMonth(value: number) {
    this._selectMonth.next(value);
    this._selectMonthExt = dayjs().set('month', value - 1).format('MMMM') as string;
    this._dayJs = dayjs(dayjs().format(`YYYY-${value}-01`));

    sessionStorage.setItem('selectedMonth', value.toString());
  }

  get selectMonthExt(): string {
    return this._selectMonthExt;
  }

  get dayJs(): dayjs.Dayjs {
    return this._dayJs;
  }
}
