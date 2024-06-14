import { TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { CategoriaService } from './categoria.service';
import { environment } from '../../../../../environments/environment';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { CustomInterceptor } from '../../../../../interceptors/http.interceptor.service';
import { ICategoria } from '../../../models';

describe('Unit Test CategoriaService', () => {

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers:[CategoriaService,
        { provide: HTTP_INTERCEPTORS, useClass: CustomInterceptor, multi: true, }
      ]
    });
  });

  it('should be created', inject([CategoriaService], (service: CategoriaService) => {
    expect(service).toBeTruthy();
  }));

  it('should send a getCategorias request to the Categoria endpoint', inject(
    [CategoriaService, HttpTestingController],
    (service: CategoriaService, httpMock: HttpTestingController) => {

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

      service.getCategorias().subscribe((response: any) => {
        expect(response).toBeTruthy();
      });

      const expectedUrl = `${environment.BASE_URL}/${environment.API_VERSION}/Categoria`;
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('GET');

      req.flush(mockResponse);
      httpMock.verify();
    }
  ));

  it('should send a getCategoriaById request to the Categoria endpoint', inject(
    [CategoriaService, HttpTestingController],
    (service: CategoriaService, httpMock: HttpTestingController) => {

      const idCategoria = 1;
      const mockResponse: ICategoria =
      {
          id: idCategoria,
          descricao: "Teste categoria despesa",
          idTipoCategoria: 1
      };

      service.getCategoriaById(1).subscribe((response: any) => {
        expect(response).toBeTruthy();
      });

      const expectedUrl = `${environment.BASE_URL}/${environment.API_VERSION}/Categoria/GetById/${idCategoria}`;
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('GET');

      req.flush(mockResponse);
      httpMock.verify();
    }
  ));

  it('should send a postCategoria request to the Categoria endpoint', inject(
    [CategoriaService, HttpTestingController],
    (service: CategoriaService, httpMock: HttpTestingController) => {
      const categoria : ICategoria = {
        id: 0,
        descricao: "Teste categoria",
        idTipoCategoria: 2
      };

      const mockResponse = { message: true, Categoria: categoria  };
      service.postCategoria(categoria).subscribe((response: any) => {
        expect(response).toBeTruthy();
      });

      const expectedUrl = `${environment.BASE_URL}/${environment.API_VERSION}/Categoria`;
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('POST');

      req.flush(mockResponse);
      httpMock.verify();
    }
  ));

  it('should send a putCategoria request to the Categoria endpoint', inject(
    [CategoriaService, HttpTestingController],
    (service: CategoriaService, httpMock: HttpTestingController) => {
      const categoria : ICategoria = {
        id: 1,
        descricao: "Teste categoria",
        idTipoCategoria: 2
      };

      const mockResponse = { message: true, Categoria: categoria  };
      service.putCategoria(categoria).subscribe((response: any) => {
        expect(response).toBeTruthy();
      });

      const expectedUrl = `${environment.BASE_URL}/${environment.API_VERSION}/Categoria`;
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('PUT');

      req.flush(mockResponse);
      httpMock.verify();
    }
  ));

  it('should send a deleteCategoria request to the Categoria endpoint', inject(
    [CategoriaService, HttpTestingController],
    (service: CategoriaService, httpMock: HttpTestingController) => {

      const idCategoria = 1;
      const mockResponse = true;

      service.deleteCategoria(1).subscribe((response: any) => {
        expect(response).toBeTruthy();
      });

      const expectedUrl = `${environment.BASE_URL}/${environment.API_VERSION}/Categoria/${idCategoria}`;
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('DELETE');

      req.flush(mockResponse);
      httpMock.verify();
    }
  ));

});
