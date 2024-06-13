import { ComponentFixture, TestBed, fakeAsync, flush } from '@angular/core/testing';
import { SaldoComponent } from './saldo.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { from, throwError } from 'rxjs';
import { SaldoService } from '../../services/api';
import * as dayjs from 'dayjs';
import { ISaldo } from '../../models';

describe('Unit Test SaldoComponent', () => {
  let component: SaldoComponent;
  let fixture: ComponentFixture<SaldoComponent>;
  let saldoService: SaldoService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [SaldoComponent, HttpClientTestingModule],
      providers: [SaldoService]
    });
    fixture = TestBed.createComponent(SaldoComponent);
    component = fixture.componentInstance;
    saldoService = TestBed.inject(SaldoService);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize saldo component', fakeAsync(() => {
    // Arrange
    const mockResponse: ISaldo = { saldo: 2.0};
    const spyOnGetSaldoAnual = spyOn(saldoService, 'getSaldoAnual').and.returnValue(from(Promise.resolve(mockResponse)));

    // Act
    component.initialize();
    flush();

    // Assert
    expect(spyOnGetSaldoAnual).toHaveBeenCalled();
    expect(spyOnGetSaldoAnual).toHaveBeenCalledWith(dayjs(dayjs().format('YYYY-01-01')));
    expect(component.saldoAnual).toEqual(mockResponse.saldo.toLocaleString('pt-br', {
      style: 'currency',
      currency: 'BRL',
      minimumFractionDigits: 2,
      maximumFractionDigits: 2
    }));
  }));

  it('should try initialize and throws error and set saldo = 0', fakeAsync(() => {
    // Arrange
    const mockResponse: ISaldo = { saldo: 4.0 } ;
    const spyOnGetSaldoAnual = spyOn(saldoService, 'getSaldoAnual').and.returnValue(throwError('Erro initialize saldo'));

    // Act
    component.initialize();
    flush();

    // Assert
    expect(spyOnGetSaldoAnual).toHaveBeenCalled();
    expect(spyOnGetSaldoAnual).toHaveBeenCalledWith(dayjs(dayjs().format('YYYY-01-01')));
    expect(component.saldoAnual).toEqual(0);
  }));

  it('should handleSaldoMesAno and update saldoMensal ', fakeAsync(() => {
    // Arrange
    const mockResponse: ISaldo =  { saldo : 4.55};
    const mockMesAno = dayjs();
    const spyOnGetSaldoByMesAno = spyOn(saldoService, 'getSaldoByMesANo').and.returnValue(from(Promise.resolve(mockResponse)));

    // Act
    component.handleSaldoMesAno(mockMesAno);
    flush();

    // Assert
    expect(spyOnGetSaldoByMesAno).toHaveBeenCalled();
    expect(spyOnGetSaldoByMesAno).toHaveBeenCalledWith(mockMesAno);
    expect(component.saldoMensal).toEqual(mockResponse.saldo.toLocaleString('pt-br', {
      style: 'currency',
      currency: 'BRL',
      minimumFractionDigits: 2,
      maximumFractionDigits: 2
    }));
  }));

  it('should try handleSaldoMesAno throws error and set saldoMensal = 0', fakeAsync(() => {
    // Arrange
    const mockMesAno = dayjs();
    const spyOnGetSaldoByMesAno = spyOn(saldoService, 'getSaldoByMesANo').and.returnValue(throwError('Erro handleSaldoMesAno'));

    // Act
    component.handleSaldoMesAno(mockMesAno);
    flush();

    // Assert
    expect(spyOnGetSaldoByMesAno).toHaveBeenCalled();
    expect(spyOnGetSaldoByMesAno).toHaveBeenCalledWith(mockMesAno);
    expect(component.saldoMensal).toEqual(0);
  }));

  it('shouldhandleSelectMes ', () => {
    // Arrange
    const mes: string = '02' as string;
    const spyOnHandleSaldoMesano = spyOn(component, 'handleSaldoMesAno').and.callThrough();

    // Act
    component.handleSelectMes(mes);

    // Assert
    expect(spyOnHandleSaldoMesano).toHaveBeenCalled();
    expect(spyOnHandleSaldoMesano).toHaveBeenCalledWith(dayjs(dayjs().format(`YYYY-${mes}-01`)));
    expect(component.filterMesService.selectMonth).toEqual(Number(mes));
  });
});
