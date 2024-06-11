import { TestBed, inject } from '@angular/core/testing';
import { HttpTestingController, provideHttpClientTesting } from "@angular/common/http/testing";
import { environment } from "src/app/shared/environments/environment";
import { ILancamento } from "src/app/shared/models";
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { CustomInterceptor } from '../../../interceptors/http.interceptor.service';
import { LancamentoService } from "./lancamento.service";
import * as dayjs from "dayjs";

describe('Unit Test LancamentoService', () => {

  beforeEach(() => {
    TestBed.configureTestingModule({
    imports: [],
    providers: [LancamentoService,
        { provide: HTTP_INTERCEPTORS, useClass: CustomInterceptor, multi: true, }, provideHttpClient(withInterceptorsFromDi()), provideHttpClientTesting()]
});
  });

  it('should be created', inject([LancamentoService], (service: LancamentoService) => {
    // Assert
    expect(service).toBeTruthy();
  }));

  it('should send a getLancamentoByMesAno request to the Lancamento endpoint', inject(
    [LancamentoService, HttpTestingController],
    (service: LancamentoService, httpMock: HttpTestingController) => {
      const mockResponse : ILancamento[] = [ ];

      service.getLancamentosByMesAno(dayjs()).subscribe((response: any) => {
        expect(response).toBeTruthy();
      });

      const expectedUrl = `${environment.endPoint}/Lancamento/${ dayjs() }`;
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('GET');

      req.flush(mockResponse);
      httpMock.verify();
    }
  ));
});
