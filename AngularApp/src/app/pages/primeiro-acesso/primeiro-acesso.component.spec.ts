import { HttpClientTestingModule } from "@angular/common/http/testing";
import { ComponentFixture, TestBed, fakeAsync, flush } from "@angular/core/testing";
import { ReactiveFormsModule } from "@angular/forms";
import { Router } from "@angular/router";
import { RouterTestingModule } from "@angular/router/testing";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { MdbFormsModule } from "mdb-angular-ui-kit/forms";
import { of, throwError } from "rxjs";
import { AlertComponent, AlertType } from "../../shared/components";
import { IControleAcesso } from "../../shared/models";
import { PrimeiroAcessoComponent } from "./primeiro-acesso.component";

describe('PrimeiroAcessoComponent', () => {
  let component: PrimeiroAcessoComponent;
  let fixture: ComponentFixture<PrimeiroAcessoComponent>;
  let mockRouter: jasmine.SpyObj<Router>;

  beforeEach(() => {
    mockRouter = jasmine.createSpyObj('Router', ['navigate']);
    TestBed.configureTestingModule({
      declarations: [PrimeiroAcessoComponent],
      imports: [ReactiveFormsModule,  RouterTestingModule, HttpClientTestingModule, MdbFormsModule ],
      providers: [AlertComponent, NgbActiveModal,
        { provide: Router, useValue: mockRouter }
      ]
    });
    fixture = TestBed.createComponent(PrimeiroAcessoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });


  it('should create', () => {
    // Assert
    expect(component).toBeTruthy();
  });

  it('onSaveClick should open a modal with success message', fakeAsync(() => {
    // Arrange
    const controleAcesso: IControleAcesso = {
      nome: 'Teste Usuário',
      sobreNome: 'Usuário',
      telefone: '(21) 9999-9999',
      email: 'teste@teste.com',
      senha: '!12345',
      confirmaSenha: '!12345'
    };
    const response = { message: true };

    spyOn(component.modalALert, 'open').and.callThrough();
    spyOn(component.controleAcessoService, 'createUsuario').and.returnValue(of(response));
    spyOn(component, 'onSaveClick').and.callThrough();

    // Act
    component.createAccountFrom.patchValue(controleAcesso);
    component.onSaveClick();

    // Assert
    expect(component.controleAcessoService.createUsuario).toHaveBeenCalledWith(controleAcesso);
    expect(component.onSaveClick).toHaveBeenCalled();
    expect(component.modalALert.open).toHaveBeenCalled();
    //expect(component.router.navigate).toHaveBeenCalledWith(['/dashboard']);
  }));

  it('should open modal when when error comes from api ', () => {
    // Arrange
    const controleAcesso: IControleAcesso = {
      nome: 'Teste Usuário',
      sobreNome: 'Usuário',
      telefone: '(21) 9999-9999',
      email: 'teste@teste.com',
      senha: '!12345',
      confirmaSenha: '!12345'
    };

    const response = {  message: "Teste Erro Message From API" };
    spyOn(component.modalALert, 'open').and.callThrough();
    spyOn(component.controleAcessoService, 'createUsuario').and.returnValue(of(response));
    spyOn(component, 'onSaveClick').and.callThrough();

    // Act
    component.createAccountFrom.patchValue(controleAcesso);
    component.onSaveClick();

    // Asssert
    expect(component.modalALert.open).toHaveBeenCalled();
  });

  it('should open modal when when throws error ', () => {
    // Arrange
    const controleAcesso: IControleAcesso = {
      nome: 'Teste Usuário',
      sobreNome: 'Usuário',
      telefone: '(21) 9999-9999',
      email: 'teste@teste.com',
      senha: '!12345',
      confirmaSenha: '!12345'
    };

    const response = {  message: "Teste Throws Error" };
    spyOn(component.modalALert, 'open').and.callThrough();
    spyOn(component.controleAcessoService, 'createUsuario').and.returnValue(of(response)).and.throwError;
    spyOn(component, 'onSaveClick').and.callThrough();

    // Act
    component.createAccountFrom.patchValue(controleAcesso);
    component.onSaveClick();

    // Asssert
    expect(component.modalALert.open).toHaveBeenCalled();
  });


  it('should toggle senha visibility and update eye icon class', () => {
    // Arrange
    component.ngOnInit();
    component.showSenha = false;
    component.eyeIconClass = 'bi-eye';

    // Act
    component.onToogleSenha();

    // Assert
    expect(component.showSenha).toBe(true);
    expect(component.eyeIconClass).toBe('bi-eye-slash');

    // Act
    component.onToogleSenha();

    // Assert
    expect(component.showSenha).toBe(false);
    expect(component.eyeIconClass).toBe('bi-eye');
  });

  it('should toggle confirma senha visibility and update eye icon class', () => {
    // Arrange
    component.ngOnInit();
    component.showConfirmaSenha = false;
    component.eyeIconClassConfirmaSenha = 'bi-eye';

    // Act
    component.onToogleConfirmaSenha();

    // Assert
    expect(component.showConfirmaSenha).toBe(true);
    expect(component.eyeIconClassConfirmaSenha).toBe('bi-eye-slash');

    // Act
    component.onToogleConfirmaSenha();

    // Assert
    expect(component.showConfirmaSenha).toBe(false);
    expect(component.eyeIconClassConfirmaSenha).toBe('bi-eye');
  });

  it('should open modal with validation error message when API returns 400 with validation errors', fakeAsync(() => {
    // Arrange
    const controleAcesso: IControleAcesso = {
      nome: 'Teste Usuário',
      sobreNome: 'Usuário',
      telefone: '(21) 9999-9999',
      email: 'teste@teste.com',
      senha: '!12345',
      confirmaSenha: '!12345'
    };
    const errorResponse = {
      status: 400,
      errors: {
        email: ['Email inválido']
      }
    };
    spyOn(component.modalALert, 'open').and.callThrough();
    spyOn(component.controleAcessoService, 'createUsuario').and.returnValue(throwError(errorResponse));
    spyOn(component, 'onSaveClick').and.callThrough();

    // Act
    component.createAccountFrom.patchValue(controleAcesso);
    component.onSaveClick();
    flush();

    // Assert
    expect(component.modalALert.open).toHaveBeenCalledWith(AlertComponent, 'Email inválido', AlertType.Warning);
  }));
});
