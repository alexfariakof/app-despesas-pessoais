import { TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from "@angular/common/http/testing";
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { CustomInterceptor } from '../../../../../interceptors/http.interceptor.service';
import { LancamentoService } from "./lancamento.service";
import dayjs from "dayjs";
import { environment } from '../../../../../environments/environment';
import { ILancamento } from '../../../models';

describe('Unit Test LancamentoService', () => {

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers:[LancamentoService,
        { provide: HTTP_INTERCEPTORS, useClass: CustomInterceptor, multi: true, }
      ]
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

      const expectedUrl = `${environment.BASE_URL}/${environment.API_VERSION}/Lancamento/${ dayjs() }`;
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('GET');

      req.flush(mockResponse);
      httpMock.verify();
    }
  ));
});
