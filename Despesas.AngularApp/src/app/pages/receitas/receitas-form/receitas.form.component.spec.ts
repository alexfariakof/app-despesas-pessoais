import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed, fakeAsync, flush } from '@angular/core/testing';
import { ReactiveFormsModule, FormBuilder } from '@angular/forms';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepicker, MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelect, MatSelectModule } from '@angular/material/select';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import * as dayjs from 'dayjs';
import { CurrencyMaskModule } from 'ng2-currency-mask';
import { from, throwError, of } from 'rxjs';
import { AlertComponent, AlertType } from 'src/app/shared/components';
import { ICategoria, IReceita, IAction } from 'src/app/shared/models';
import { AuthService } from 'src/app/shared/services';
import { ReceitaService } from 'src/app/shared/services/api';
import { ReceitasFormComponent } from './receitas.form.component';

describe('Unit Test ReceitasFormComponent', () => {
  let component: ReceitasFormComponent;
  let fixture: ComponentFixture<ReceitasFormComponent>;
  let receitaService: ReceitaService;
  let mockReceitas: IReceita[] = [
    { id: 1, data: dayjs(), descricao: 'Teste Receitas 1', valor: 1.05, categoria: { id: 1, descricao: 'Categoria 1', idTipoCategoria: 2 }},
    { id: 2, data: dayjs(), descricao: 'Teste Receitas 2', valor: 2.05, categoria: { id: 2, descricao: 'Categoria 2', idTipoCategoria: 2 }},
    { id: 3, data: dayjs(), descricao: 'Teste Receitas 3', valor: 3.05, categoria: { id: 4, descricao: 'Categoria 4', idTipoCategoria: 2 }},
  ];
  let mockCategorias: ICategoria[] = [
    { id: 1, descricao: 'Teste Categoria Recaita 1', idTipoCategoria: 2},
    { id: 2, descricao: 'Teste Categoria Receita 2', idTipoCategoria: 2}
  ];

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ReceitasFormComponent, MatDatepicker, MatSelect],
      imports: [ReactiveFormsModule, HttpClientTestingModule, MatFormFieldModule, MatInputModule, MatSelectModule, MatDatepickerModule, MatNativeDateModule, BrowserAnimationsModule, CurrencyMaskModule],
      providers: [FormBuilder, AlertComponent, NgbActiveModal, ReceitaService ]
    });
    fixture = TestBed.createComponent(ReceitasFormComponent);
    component = fixture.componentInstance;
    receitaService = TestBed.inject(ReceitaService);
    fixture.detectChanges();
  });

  it('should create', () => {
    // Assert
    expect(component).toBeTruthy();
  });

  it('should getCategoriasReceutasw ', fakeAsync(() => {
    // Arrange
    const getCategoriasSpy = spyOn(receitaService, 'getReceitasCategorias').and.returnValue(from(Promise.resolve(mockCategorias)));

    // Act
    component.ngOnInit();
    component.getCatgeoriasFromReceitas();
    flush();
    fixture.detectChanges();
    // Assert
    expect(getCategoriasSpy).toHaveBeenCalled();
    expect(component.categorias.length).toBeGreaterThan(1);
  }));

  it('should thorws errro when call getCategorias and open modal alert ', () => {
    // Arrange
    const errorMessage = 'Fake Error Message';
    const getCategoriasSpy = spyOn(receitaService, 'getReceitasCategorias').and.returnValue(throwError(errorMessage));
    const alertOpenSpy = spyOn(TestBed.inject(AlertComponent), 'open');

    // Act
    component.getCatgeoriasFromReceitas();

    // Assert
    expect(getCategoriasSpy).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalledWith(AlertComponent, errorMessage, AlertType.Warning);
  });

  it('should saveCreateReceita onSaveClick and show successfully message', fakeAsync(() => {
    // Arrange
    const receita: IReceita = {
      id: 0,
      data: dayjs(),
      descricao: 'Teste Create Receitas',
      valor: 10.23,
      categoria: { id: 1, descricao: 'Categoria 1', idTipoCategoria: 2 }
    };
    const receitaPostServiceSpy = spyOn(receitaService, 'postReceita').and.returnValue(of(true));
    const modalCloseSpy = spyOn(component.activeModal, 'close').and.callThrough();
    const alertOpenSpy = spyOn(TestBed.inject(AlertComponent), 'open').and.callThrough();
    spyOn(component, 'onSaveClick').and.callThrough();

    // Act
    component.ngOnInit();
    component.action = IAction.Create;
    component.refresh = () => { console.log('Fake Refresh Receitas'); };
    component.receitaForm.patchValue(receita);
    component.onSaveClick();
    flush();

    // Assert
    expect(receitaPostServiceSpy).toHaveBeenCalledWith(jasmine.objectContaining(receita));
    expect(modalCloseSpy).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalledWith(AlertComponent, 'Receita cadastrada com Sucesso.', AlertType.Success);
  }));

  it('should throws error when try to saveCreateReceita and show error message', () => {
    // Arrange
    const errorMessage = 'Fake Error Message Create Receita';
    const receita: IReceita = {
      id: 0,
      data: dayjs(),
      descricao: 'Teste Create Receitas',
      valor: 200.99,
      categoria: { id: 1, descricao: 'Categoria 1', idTipoCategoria: 2 }
    };
    const receitaPostServiceSpy = spyOn(receitaService, 'postReceita').and.returnValue(throwError(errorMessage));
    const alertOpenSpy = spyOn(TestBed.inject(AlertComponent), 'open').and.callThrough();
    spyOn(component, 'onSaveClick').and.callThrough();

    // Act
    component.ngOnInit();
    component.action = IAction.Create;
    component.refresh = () => { console.log('Fake Refresh Receitas'); };
    component.receitaForm.patchValue(receita);
    component.onSaveClick();

    // Assert
    expect(receitaPostServiceSpy).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalledWith(AlertComponent, errorMessage, AlertType.Warning);
  });

  it('should saveEditReceita onSaveClick', fakeAsync(() => {
    // Arrange
    const mockReceita: IReceita = {
      id: 1,
      data: dayjs().format('YYYY-MM-DD'),
      descricao: 'Teste Edit Receitas',
      valor: 10.58,
      categoria: { id: 1, descricao: 'Categoria 1', idTipoCategoria: 2 }
    };
    const receitaPutServiceSpy = spyOn(receitaService, 'putReceita').and.returnValue(of(mockReceita));
    const modalCloseSpy = spyOn(component.activeModal, 'close').and.callThrough();
    const alertOpenSpy = spyOn(TestBed.inject(AlertComponent), 'open').and.callThrough();
    spyOn(component, 'onSaveClick').and.callThrough();

    // Act
    component.ngOnInit();
    component.action = IAction.Edit;
    component.refresh = () => { console.log('Fake Refresh Receitas'); };
    component.receitaForm.patchValue(mockReceita);
    component.onSaveClick();
    flush();

    // Assert
    expect(receitaPutServiceSpy).toHaveBeenCalledWith(jasmine.objectContaining(mockReceita));
    expect(modalCloseSpy).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalledWith(AlertComponent, 'Receita alterada com Sucesso.', AlertType.Success);
  }));

  it('should throws error when try to saveEditReceita and show error message', () => {
    // Arrange
    const errorMessage = 'Fake Error Message Edit Receita';
    const receita: IReceita = {
      id: 1,
      data: dayjs().format('YYYY-MM-DD'),
      descricao: 'Teste Edit Receitas',
      valor: 20.87,
      categoria: { id: 2, descricao: 'Categoria 2', idTipoCategoria: 1 }
    };
    const receitaPutServiceSpy = spyOn(receitaService, 'putReceita').and.returnValue(throwError(errorMessage));
    const alertOpenSpy = spyOn(TestBed.inject(AlertComponent), 'open').and.callThrough();
    spyOn(component, 'onSaveClick').and.callThrough();

    // Act
    component.ngOnInit();
    component.action = IAction.Edit;
    component.refresh = () => { console.log('Fake Refresh Receitas'); };
    component.receitaForm.patchValue(receita);
    component.onSaveClick();

    // Assert
    expect(receitaPutServiceSpy).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalledWith(AlertComponent, errorMessage, AlertType.Warning);
  });

  it('should show error message when onClickSave', () => {
    // Arrange
    const errorMessage = Error('Ação não pode ser realizada.');
    const alertOpenSpy = spyOn(TestBed.inject(AlertComponent), 'open').and.callThrough();

    // Act
    component.ngOnInit();
    component.action = undefined;
    component.onSaveClick();

    // Assert
    expect(alertOpenSpy).toHaveBeenCalledWith(AlertComponent, errorMessage.message, AlertType.Warning);
  });

  it('should execute editReceita and SetFormReceita ', fakeAsync(() => {
    // Arrange
    const mockReceita: IReceita = mockReceitas[0];
    const mockResponse: IReceita = mockReceita;
    const getReceitasById = spyOn(receitaService, 'getReceitaById').and.returnValue(from(Promise.resolve(mockResponse)));
    const editReceita = spyOn(component, 'editReceita').and.callThrough();

    // Act
    component.editReceita(mockReceita.id);
    flush();

    // Assert
    expect(getReceitasById).toHaveBeenCalled();
    expect(editReceita).toHaveBeenCalled();
    expect(editReceita).toHaveBeenCalledWith(mockReceita.id);
  }));

  it('should throws error when try to editReceita', fakeAsync(() => {
    // Arrange
    const errorMessage = 'Fake Error Message Edit Receita';
    const mockReceita: IReceita = mockReceitas[1];
    const getReceitasById = spyOn(receitaService, 'getReceitaById').and.returnValue(throwError(errorMessage));
    const alertOpenSpy = spyOn(TestBed.inject(AlertComponent), 'open');

    // Act
    component.editReceita(mockReceita.id);
    flush();

    // Assert
    expect(getReceitasById).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalledWith(AlertComponent, errorMessage, AlertType.Warning);
  }));

  it('should deleteReceita and open modal alert success', fakeAsync(() => {
    // Arrange
    const mockResponse = true;
    const getDeleteReceita = spyOn(receitaService, 'deleteReceita').and.returnValue(from(Promise.resolve(mockResponse)));
    spyOn(component.modalAlert, 'open').and.callThrough();

    // Act
    component.deleteReceita(mockReceitas[1].id, () => { console.log('Fake Call Back'); });
    flush();

    // Assert
    expect(getDeleteReceita).toHaveBeenCalled();
    expect(component.modalAlert.open).toHaveBeenCalled();
    expect(component.modalAlert.open).toHaveBeenCalledWith(AlertComponent, 'Receita excluída com sucesso', AlertType.Success);
  }));

  it('should try to deleteReceita and open modal alert warning', fakeAsync(() => {
    // Arrange
    const mockResponse = false;
    const getDeleteReceita = spyOn(receitaService, 'deleteReceita').and.returnValue(from(Promise.resolve(mockResponse)));
    spyOn(component.modalAlert, 'open').and.callThrough();

    // Act
    component.deleteReceita(mockReceitas[2].id, () => { console.log('Fake Call Back'); });
    flush();

    // Assert
    expect(getDeleteReceita).toHaveBeenCalled();
    expect(component.modalAlert.open).toHaveBeenCalled();
    expect(component.modalAlert.open).toHaveBeenCalledWith(AlertComponent, 'Erro ao excluír receita', AlertType.Warning);
  }));

  it('should throws error when try to deleteReceita and open modal alert warning', fakeAsync(() => {
    // Arrange
    const errorMessage = 'Fake Error Message Delete Receita';
    const spyOnDeleteReceita = spyOn(receitaService, 'deleteReceita').and.returnValue(throwError(errorMessage));
    const alertOpenSpy = spyOn(TestBed.inject(AlertComponent), 'open');

    // Act
    component.deleteReceita(mockReceitas[1].id, () => { console.log('Fake Call Back'); });
    flush();

    // Assert
    expect(spyOnDeleteReceita).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalledWith(AlertComponent, errorMessage, AlertType.Warning);
  }));
});
