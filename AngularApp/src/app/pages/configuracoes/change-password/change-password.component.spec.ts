import { CommonModule } from "@angular/common";
import { provideHttpClientTesting } from "@angular/common/http/testing";
import { ComponentFixture, TestBed, fakeAsync, flush } from "@angular/core/testing";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { from, throwError } from "rxjs";
import { AlertComponent, AlertType } from "../../../shared/components";
import { ILogin } from "../../../shared/models";
import { ControleAcessoService } from "../../../shared/services/api";
import { ChangePasswordComponent } from "./change-password.component";
import { provideHttpClient, withInterceptorsFromDi } from "@angular/common/http";


describe('ChangePasswordComponent', () => {
  let component: ChangePasswordComponent;
  let fixture: ComponentFixture<ChangePasswordComponent>;
  let controleAcessoService: ControleAcessoService;
  beforeEach(() => {
    TestBed.configureTestingModule({
    declarations: [ChangePasswordComponent],
    imports: [CommonModule, FormsModule, ReactiveFormsModule],
    providers: [AlertComponent, ControleAcessoService, NgbActiveModal, provideHttpClient(withInterceptorsFromDi()), provideHttpClientTesting()]
});
    fixture = TestBed.createComponent(ChangePasswordComponent);
    component = fixture.componentInstance;
    controleAcessoService = TestBed.inject(ControleAcessoService);
    fixture.detectChanges();
  });

  it('should create and execute NgOnInit', () => {
    // Arrange
    const spyOnNgOnInt = spyOn(component, 'ngOnInit');

    // Assert
    component.ngOnInit();
    expect(component).toBeTruthy();
    expect(spyOnNgOnInt).toHaveBeenCalled();
  });

  it('should initialize correctly', () => {
    // Arrange
    const spyOnInitialize  = spyOn(component, 'initialize').and.callThrough();

    // Act
    component.initialize();

    // Assert
    expect(spyOnInitialize).toHaveBeenCalled();
    expect(component.changePasswordFrom.value.senha).toBe('');
    expect(component.changePasswordFrom.value.confirmaSenha).toBe('');
  });

  it('should change password onSaveClick and open Modal Alert Success', fakeAsync(() => {
    // Arrange
    let mockForm: ILogin = {
      senha: '12345!',
      confirmaSenha: '12345!'
    }
    const spyOnControleAcessoService = spyOn(controleAcessoService, 'changePassword').and.returnValue(from(Promise.resolve({ message:  true})));
    const spyOnInitialize  = spyOn(component, 'initialize').and.callThrough();
    const alertOpenSpy = spyOn(TestBed.inject(AlertComponent), 'open');

    // Act
    component.changePasswordFrom.patchValue(mockForm);
    component.onSaveClick();
    flush();

    // Assert
    expect(spyOnControleAcessoService).toHaveBeenCalled();
    expect(spyOnControleAcessoService).toHaveBeenCalledWith(mockForm);
    expect(spyOnInitialize).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalledWith(AlertComponent, 'Senha alterada com sucesso!', AlertType.Success);
  }));

  it('should try change password onSaveClick and thrwos error', fakeAsync(() => {
    // Arrange
    const errorMessage = 'Fake Error Message on Change Passawrod ';
    const spyOnControleAcessoService = spyOn(controleAcessoService, 'changePassword').and.returnValue(throwError(errorMessage));
    const alertOpenSpy = spyOn(TestBed.inject(AlertComponent), 'open');

    // Act
    component.onSaveClick();
    flush();

    // Assert
    expect(spyOnControleAcessoService).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalledWith(AlertComponent, errorMessage, AlertType.Warning);
  }));


  it('should toggle senha visibility ', () => {
    // Arrange
    component.ngOnInit();
    component.showSenha = false;

    // Act
    component.onToogleSenha();

    // Assert
    expect(component.showSenha).toBe(true);

    // Act
    component.onToogleSenha();

    // Assert
    expect(component.showSenha).toBe(false);
  });

  it('should toggle confirma senha visibility', () => {
    // Arrange
    component.ngOnInit();
    component.showConfirmaSenha = false;

    // Act
    component.onToogleConfirmaSenha();

    // Assert
    expect(component.showConfirmaSenha).toBe(true);

    // Act
    component.onToogleConfirmaSenha();

    // Assert
    expect(component.showConfirmaSenha).toBe(false);
  });

});
