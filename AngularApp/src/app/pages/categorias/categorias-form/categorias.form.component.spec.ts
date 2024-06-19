import { provideHttpClientTesting } from "@angular/common/http/testing";
import { ComponentFixture, TestBed, fakeAsync, flush } from "@angular/core/testing";
import { ReactiveFormsModule, FormBuilder } from "@angular/forms";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { from, of, throwError } from "rxjs";
import { CategoriasFormComponent } from "./categorias.form.component";
import { AlertComponent, AlertType } from "../../../shared/components";
import { ICategoria, IAction } from "../../../shared/models";
import { CategoriaService } from "../../../shared/services/api";
import { provideHttpClient, withInterceptorsFromDi } from "@angular/common/http";


describe('Unit Test CategoriasFormComponent', () => {
  let component: CategoriasFormComponent;
  let fixture: ComponentFixture<CategoriasFormComponent>;
  let categoriaService: CategoriaService;
  let alertComponent: AlertComponent;

  beforeEach(() => {
    TestBed.configureTestingModule({
    declarations: [],
    imports: [ReactiveFormsModule],
    providers: [FormBuilder, AlertComponent, NgbActiveModal, CategoriaService, provideHttpClient(withInterceptorsFromDi()), provideHttpClientTesting()]
});
    fixture = TestBed.createComponent(CategoriasFormComponent);
    component = fixture.componentInstance;
    categoriaService = TestBed.inject(CategoriaService);
    alertComponent = TestBed.inject(AlertComponent);
    fixture.detectChanges();
  });

  it('should create', () => {
    // Assert
    expect(component).toBeTruthy();
  });

  it('should create categoria and show successfully message', () => {
    // Arrange
    const categoria: ICategoria = { id: 0, descricao: 'Teste categoria Despesa.', idTipoCategoria: 1 };
    const categoriaServiceSpy = spyOn(categoriaService, 'postCategoria').and.returnValue(of({ message: true }));
    const modalCloseSpy = spyOn(component.activeModal, 'close').and.callThrough();
    const spyRefresh = spyOn(component, "setRefresh");
    const alertOpenSpy = spyOn(alertComponent, 'open').and.callThrough();
    spyOn(component, 'onSaveClick').and.callThrough();

    // Act
    component.ngOnInit();
    component.setAction(IAction.Create);
    component.setRefresh(() => { });
    component.categoriatForm.patchValue(categoria);
    component.onSaveClick();

    // Assert
    expect(categoriaServiceSpy).toHaveBeenCalledWith(jasmine.objectContaining(categoria));
    expect(modalCloseSpy).toHaveBeenCalled();
    expect(spyRefresh).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalledWith(AlertComponent, 'Categoria cadastrada com Sucesso.', AlertType.Success);
  });

  it('should edit categoria and show successfully message', fakeAsync(() => {
    // Arrange
    const categoria: ICategoria = { id: 1, descricao: "Teste categoria", idTipoCategoria: 2 };
    const categoriaServiceSpy = spyOn(categoriaService, 'putCategoria').and.returnValue(from(Promise.resolve({ message: true, categoria: categoria })));
    const modalCloseSpy = spyOn(component.activeModal, 'close').and.callThrough();;
    const spyRefresh = spyOn(component, "setRefresh");
    const alertOpenSpy = spyOn(alertComponent, 'open').and.callThrough();
    spyOn(component, 'onSaveClick').and.callThrough();

    // Act
    component.ngOnInit();
    component.setAction(IAction.Edit);
    component.setRefresh(() => { });
    component.categoriatForm.patchValue(categoria);
    component.onSaveClick();
    flush();

    // Assert
    expect(categoriaServiceSpy).toHaveBeenCalledWith(jasmine.objectContaining(categoria));
    expect(modalCloseSpy).toHaveBeenCalled();
    expect(spyRefresh).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalledWith(AlertComponent, 'Categoria alterada com Sucesso.', AlertType.Success);
  }));

  it('should call try create categoria throw error and show error message', fakeAsync(() => {
    // Arrange
    const categoria: ICategoria = { id: 0, descricao: "Teste categoria", idTipoCategoria: 2 };
    const errorMessage = 'Fake Error Message';
    spyOn(categoriaService, 'postCategoria').and.returnValue(throwError(() => { throw errorMessage}));
    const alertOpenSpy = spyOn(alertComponent, 'open');

    // Act
    component.ngOnInit();
    component.categoriatForm.patchValue(categoria);
    component.onSaveClick();

    // Assert
    expect(alertOpenSpy).toHaveBeenCalledWith(AlertComponent, errorMessage, AlertType.Warning);
  }));

  it('should return FormGroup', () => {
    // Arrange
    const categoria: ICategoria = { id: 1, descricao: "Teste categoria", idTipoCategoria: 2 };
    const spyFormGroup = spyOn(component, 'setCategoria').and.callThrough();;

    // Act
    component.ngOnInit();
    const formGroup = component.setCategoria(categoria);

    // Assert
    expect(spyFormGroup).toHaveBeenCalled();
  });

  it('should throw error when try to postCategoria', fakeAsync(() => {
    // Arrange
    const categoria: ICategoria = { id: 0, descricao: "Teste categoria", idTipoCategoria: 2 };
    const errorMessage = 'Fake Error Message';
    spyOn(categoriaService, 'postCategoria').and.returnValue(throwError(errorMessage));
    const alertOpenSpy = spyOn(alertComponent, 'open');

    // Act
    component.ngOnInit();
    component.categoriatForm.patchValue(categoria);
    component.onSaveClick();

    // Assert
    expect(alertOpenSpy).toHaveBeenCalled();
  }));

  it('should throw error when try to putCategoria', fakeAsync(() => {
    // Arrange
    const categoria: ICategoria = { id: 0, descricao: "Teste categoria", idTipoCategoria: 2 };
    const errorMessage = 'Fake Error Message';
    spyOn(categoriaService, 'putCategoria').and.returnValue(throwError(errorMessage));
    const alertOpenSpy = spyOn(alertComponent, 'open');

    // Act
    component.ngOnInit();
    component.categoriatForm.patchValue(categoria);
    component.setAction(IAction.Edit);
    component.onSaveClick();

    // Assert
    expect(alertOpenSpy).toHaveBeenCalled();
  }));
});
