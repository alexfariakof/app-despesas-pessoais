import { TestBed, inject } from '@angular/core/testing';
import { ControleAcessoService } from './controle-acesso.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ILogin } from '../../../models/ILogin';
import { environment } from '../../../../../environments/environment';
import { IControleAcesso } from '../../../models/IControleAcesso';
import { CustomInterceptor } from '../../../../../interceptors/http.interceptor.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

describe('Unit Test ControleAcessoService', () => {

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers:[ControleAcessoService,
        { provide: HTTP_INTERCEPTORS, useClass: CustomInterceptor, multi: true, }
      ]
    });
  });

  it('should be created', inject([ControleAcessoService], (service: ControleAcessoService) => {
    expect(service).toBeTruthy();
  }));

  it('should send a POST request to the ControleAcesso/SignIn endpoint', inject(
    [ControleAcessoService, HttpTestingController],
    (service: ControleAcessoService, httpMock: HttpTestingController) => {
      const loginData: ILogin = {
        email: 'teste@teste.com',
        senha: 'teste',
      };

      const mockResponse = { message: true };
      service.signIn(loginData).subscribe((response: any) => {
        expect(response).toBeTruthy();
      });
      const expectedUrl = `${environment.BASE_URL}/${environment.API_VERSION}/ControleAcesso/SignIn`;
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('POST');
      req.flush(mockResponse);
      httpMock.verify();
    }
  ));

  it('should send a POST request to the /ControleAcesso endpoint', inject(
    [ControleAcessoService, HttpTestingController],
    (service: ControleAcessoService, httpMock: HttpTestingController) => {
      const controleAcessoData: IControleAcesso = {
        nome: 'Teste ',
        sobreNome: 'Usuario',
        telefone: '(21) 9999-9999',
        email: 'teste@teste.com',
        senha: '12345',
        confirmaSenha: '12345'
      };

      const mockResponse = { message: true };
      service.createUsuario(controleAcessoData).subscribe((response: any) => {
        expect(response).toBeTruthy();
      });
      const expectedUrl = `${environment.BASE_URL}/${environment.API_VERSION}/ControleAcesso`;
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('POST');
      req.flush(mockResponse);
      httpMock.verify();
    }
  ));

  it('should send a POST request to the /ControleAcesso/ChangePassword endpoint', inject(
    [ControleAcessoService, HttpTestingController],
    (service: ControleAcessoService, httpMock: HttpTestingController) => {
      const login: ILogin = {
        senha: '12345',
        confirmaSenha: '12345'
      };

      const mockResponse = { message: true };
      service.changePassword(login).subscribe((response: any) => {
        expect(response).toBeTruthy();
      });

      const expectedUrl = `${environment.BASE_URL}/${environment.API_VERSION}/ControleAcesso/ChangePassword`;
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('POST');
      req.flush(mockResponse);
      httpMock.verify();
    }
  ));
});
