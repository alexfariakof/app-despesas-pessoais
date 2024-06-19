import { TestBed, inject } from '@angular/core/testing';
import { HttpTestingController, provideHttpClientTesting } from "@angular/common/http/testing";
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { CustomInterceptor } from '../../../../../interceptors/http.interceptor.service';
import  dayjs from "dayjs";
import { SaldoService } from './saldo.service';
import { environment } from '../../../../../environments/environment';

describe('Unit Test SaldoService', () => {

  beforeEach(() => {
    TestBed.configureTestingModule({
    imports: [],
    providers: [SaldoService,
        { provide: HTTP_INTERCEPTORS, useClass: CustomInterceptor, multi: true, }, provideHttpClient(withInterceptorsFromDi()), provideHttpClientTesting()]
});
  });
  it('should be created', inject([SaldoService], (service: SaldoService) => {
    expect(service).toBeTruthy();
  }));

  it('should send a getSaldo request to the Saldo endpoint', inject(
    [SaldoService, HttpTestingController],
    (service: SaldoService, httpMock: HttpTestingController) => {

      const mockResponse: number = 989.9;

      service.getSaldo().subscribe((response: any) => {
        expect(response).toBeTruthy();
      });

      const expectedUrl = `${environment.BASE_URL}/${environment.API_VERSION}/Saldo`;
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('GET');

      req.flush(mockResponse);
      httpMock.verify();
    }
  ));

  it('should send a getSaldoAnual request to the Saldo endpoint', inject(
    [SaldoService, HttpTestingController],
    (service: SaldoService, httpMock: HttpTestingController) => {

      const mockResponse: number = 2500.25;
      const mockAno = dayjs();
      service.getSaldoAnual(mockAno).subscribe((response: any) => {
        expect(response).toBeTruthy();
      });

      const expectedUrl = `${environment.BASE_URL}/${environment.API_VERSION}/Saldo/ByAno/${mockAno}`;
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('GET');

      req.flush(mockResponse);
      httpMock.verify();
    }
  ));

  it('should send a getSaldo request to the Saldo endpoint', inject(
    [SaldoService, HttpTestingController],
    (service: SaldoService, httpMock: HttpTestingController) => {

      const mockResponse: number = 84980.09;
      const mockMesAno = dayjs();

      service.getSaldoByMesANo(mockMesAno).subscribe((response: any) => {
        expect(response).toBeTruthy();
      });

      const expectedUrl = `${environment.BASE_URL}/${environment.API_VERSION}/Saldo/ByMesAno/${mockMesAno}`;
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('GET');

      req.flush(mockResponse);
      httpMock.verify();
    }
  ));

});
