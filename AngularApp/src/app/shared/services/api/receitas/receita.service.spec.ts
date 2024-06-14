import { TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ReceitaService } from './receita.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { CustomInterceptor } from '../../../../../interceptors/http.interceptor.service';
import  dayjs from 'dayjs';
import { environment } from '../../../../../environments/environment';
import { IReceita, ICategoria } from '../../../models';

describe('Unit Test ReceitaService', () => {

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers:[ReceitaService,
        { provide: HTTP_INTERCEPTORS, useClass: CustomInterceptor, multi: true, }
      ]
    });
  });

  it('should be created', inject([ReceitaService], (service: ReceitaService) => {
    expect(service).toBeTruthy();
  }));

  it('should send a getReceitas request to the Receita endpoint', inject(
    [ReceitaService, HttpTestingController],
    (service: ReceitaService, httpMock: HttpTestingController) => {

      const mockResponse : IReceita[] = [
        { id: 1, data: dayjs(), descricao: 'Teste Receitas 1', valor: 1.05, categoria: { id: 1, descricao: 'Categoria 1', idTipoCategoria: 2 } },
        { id: 2, data: dayjs(), descricao: 'Teste Receitas 2', valor: 2.05, categoria: { id: 1, descricao: 'Categoria 2', idTipoCategoria: 2 } },
        { id: 3, data: dayjs(), descricao: 'Teste Receitas 3', valor: 3.05, categoria: { id: 1, descricao: 'Categoria 2', idTipoCategoria: 2 } },
      ];

      service.getReceitas().subscribe((response: any) => {
        expect(response).toBeTruthy();
      });

      const expectedUrl = `${environment.BASE_URL}/${environment.API_VERSION}/Receita`;
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('GET');

      req.flush(mockResponse);
      httpMock.verify();
    }
  ));

  it('should send a getReceitaById request to the Receita endpoint', inject(
    [ReceitaService, HttpTestingController],
    (service: ReceitaService, httpMock: HttpTestingController) => {

      const idReceita = 1;
      const mockResponse: IReceita = {
        id: 1,
        data: dayjs(),
        descricao: 'Teste Receitas 1',
        valor: 2.05,
        categoria: { id: 1, descricao: 'Categoria 1', idTipoCategoria: 2 }
      };

      service.getReceitaById(1).subscribe((response: any) => {
        expect(response).toBeTruthy();
      });

      const expectedUrl = `${environment.BASE_URL}/${environment.API_VERSION}/Receita/GetById/${idReceita}`;
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('GET');

      req.flush(mockResponse);
      httpMock.verify();
    }
  ));

  it('should send a getReceitasCategorias request to the Categoria endpoint', inject(
    [ReceitaService, HttpTestingController],
    (service: ReceitaService, httpMock: HttpTestingController) => {

      const mockResponse : ICategoria[] = [
        {
          id: 1,
          descricao: "Teste categoria despesa",
          idTipoCategoria: 1
        },
        {
          id: 2,
          descricao: "Teste categoria receita",
          idTipoCategoria: 2
        }
      ];

      service.getReceitasCategorias().subscribe((response: any) => {
        expect(response).toBeTruthy();
      });

      const expectedUrl = `${environment.BASE_URL}/${environment.API_VERSION}/Categoria/GetByTipoCategoria/2`;
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('GET');

      req.flush(mockResponse);
      httpMock.verify();
    }
  ));

  it('should send a postReceita request to the Receita endpoint', inject(
    [ReceitaService, HttpTestingController],
    (service: ReceitaService, httpMock: HttpTestingController) => {
      const despesa : IReceita = {
        id: 1,
        data: dayjs(),
        descricao: 'Teste Receitas 1',
        valor: 4.25,
        categoria: { id: 1, descricao: 'Categoria 1', idTipoCategoria: 2 }
      };

      const mockResponse = { message: true, Receita: despesa  };
      service.postReceita(despesa).subscribe((response: any) => {
        expect(response).toBeTruthy();
      });

      const expectedUrl = `${environment.BASE_URL}/${environment.API_VERSION}/Receita`;
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('POST');

      req.flush(mockResponse);
      httpMock.verify();
    }
  ));

  it('should send a putReceita request to the Receita endpoint', inject(
    [ReceitaService, HttpTestingController],
    (service: ReceitaService, httpMock: HttpTestingController) => {
      const despesa : IReceita = {
        id: 1,
        data: dayjs(),
        descricao: 'Teste Receitas 1',
        valor: 50.50,
        categoria: { id: 2, descricao: 'Categoria 1', idTipoCategoria: 2 }
      };

      const mockResponse = { message: true, Receita: despesa  };
      service.putReceita(despesa).subscribe((response: any) => {
        expect(response).toBeTruthy();
      });

      const expectedUrl = `${environment.BASE_URL}/${environment.API_VERSION}/Receita`;
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('PUT');

      req.flush(mockResponse);
      httpMock.verify();
    }
  ));

  it('should send a deleteReceita request to the Receita endpoint', inject(
    [ReceitaService, HttpTestingController],
    (service: ReceitaService, httpMock: HttpTestingController) => {

      const idReceita = 1;
      const mockResponse = true;

      service.deleteReceita(1).subscribe((response: any) => {
        expect(response).toBeTruthy();
      });

      const expectedUrl = `${environment.BASE_URL}/${environment.API_VERSION}/Receita/${ idReceita }`;
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('DELETE');

      req.flush(mockResponse);
      httpMock.verify();
    }
  ));


});
