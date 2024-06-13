import { MenuService } from 'src/app/shared/services';
import { CommonModule } from "@angular/common";
import { HttpClientTestingModule } from "@angular/common/http/testing";
import { ComponentFixture, TestBed, fakeAsync, flush } from "@angular/core/testing";
import { MatNativeDateModule } from "@angular/material/core";
import { MatDatepicker, MatDatepickerModule } from "@angular/material/datepicker";
import { MatSelect, MatSelectModule } from "@angular/material/select";
import { RouterTestingModule } from "@angular/router/testing";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import * as dayjs from "dayjs";
import { from, throwError } from "rxjs";
import { AlertComponent, ModalFormComponent, ModalConfirmComponent, DataTableComponent, AlertType } from "src/app/shared/components";
import { ReceitaDataSet } from "src/app/shared/datatable-config/receitas";
import { IReceita } from "src/app/shared/models";
import { ReceitaService } from "src/app/shared/services/api";
import { SharedModule } from "src/app/shared/shared.module";
import { ReceitasFormComponent } from "./receitas-form/receitas.form.component";
import { ReceitasComponent } from "./receitas.component";

describe('Unit Test ReceitasComponent', () => {
  let component: ReceitasComponent;
  let fixture: ComponentFixture<ReceitasComponent>;
  let receitaService: ReceitaService;
  let mockReceitas: IReceita[] = [
    { id: 1, data: dayjs(), descricao: 'Teste Receitas 1', valor: 1.05, categoria: { id: 1, descricao: 'Categoria 1', idTipoCategoria: 2 } },
    { id: 2, data: dayjs(), descricao: 'Teste Receitas 2', valor: 2.05, categoria: { id: 2, descricao: 'Categoria 2', idTipoCategoria: 2 } },
    { id: 3, data: dayjs(), descricao: 'Teste Receitas 3', valor: 3.05, categoria: { id: 4, descricao: 'Categoria 4', idTipoCategoria: 2 } },
  ];
  let mockReceitasData: ReceitaDataSet[] = [
    { id: 1, data: dayjs().format('DD/MM/YYYY'), descricao: 'Teste Receitas 1', valor: 'R$ 1.05', categoria: 'Categoria 1' },
    { id: 2, data: dayjs().format('DD/MM/YYYY'), descricao: 'Teste Receitas 2', valor: 'R$ 2.05', categoria: 'Categoria 2' },
    { id: 3, data: dayjs().format('DD/MM/YYYY'), descricao: 'Teste Receitas 3', valor: 'R$ 3.05', categoria: 'Categroia 3' }
  ];

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ReceitasComponent, ReceitasFormComponent, MatDatepicker, MatSelect],
      imports: [CommonModule, RouterTestingModule, SharedModule, HttpClientTestingModule, MatSelectModule , MatDatepickerModule, MatNativeDateModule],
      providers: [MenuService, AlertComponent, ModalFormComponent, ModalConfirmComponent, NgbActiveModal, ReceitaService, ReceitasFormComponent ]
    });
    fixture = TestBed.createComponent(ReceitasComponent);
    component = fixture.componentInstance;
    component.dataTable = TestBed.inject(DataTableComponent);
    receitaService = TestBed.inject(ReceitaService);
    fixture.detectChanges();
  });

  it('should create', () => {
    // Assert
    expect(component).toBeTruthy();
  });

  it('should initializeDataTable', fakeAsync(() => {
    // Arrange
    const spyOnGetReceitas = spyOn(receitaService, 'getReceitas').and.returnValue(from(Promise.resolve(mockReceitas)));

    // Act
    component.initializeDataTable();
    flush();

    // Assert
    expect(spyOnGetReceitas).toHaveBeenCalled();
    expect(component.receitasData.length).toBeGreaterThan(1);
  }));

  it('should initializeDataTable and return empty Datatable', fakeAsync(() => {
    // Arrange
    const errorMessage = 'Fake Error Message Empty DataTable Receitas';
    const spyOnGetReceitas = spyOn(receitaService, 'getReceitas').and.returnValue(throwError(errorMessage));
    const alertOpenSpy = spyOn(TestBed.inject(AlertComponent), 'open');

    // Act
    component.initializeDataTable();
    flush();

    // Assert
    expect(spyOnGetReceitas).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalledWith(AlertComponent, errorMessage, AlertType.Warning);
  }));

  it('should throw error when try to initializeDataTable', () => {
    // Arrange
    const errorMessage = 'Fake Error Message initialize DataTable Receitas';
    const spyOnGetReceitas = spyOn(receitaService, 'getReceitas').and.returnValue(throwError(errorMessage));
    const alertOpenSpy = spyOn(TestBed.inject(AlertComponent), 'open');

    // Act
    component.initializeDataTable();

    // Assert
    expect(spyOnGetReceitas).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalledWith(AlertComponent, errorMessage, AlertType.Warning);
  });


  it('should updateDatatable when is called', fakeAsync(() => {
    // Arrange
    const spyOnGetReceitas = spyOn(receitaService, 'getReceitas').and.returnValue(from(Promise.resolve(mockReceitas)));


    // Act
    component.updateDatatable();
    flush();

    // Assert
    expect(spyOnGetReceitas).toHaveBeenCalled();
    expect(component.receitasData.length).toBeGreaterThan(0);
  }));

  it('should throw error when try to updateDataTable', () => {
    // Arrange
    const errorMessage = 'Fake Error Message Recitas UpdateDataTable';
    const spyOnGetReceitas = spyOn(receitaService, 'getReceitas').and.returnValue(throwError(errorMessage));
    const alertOpenSpy = spyOn(TestBed.inject(AlertComponent), 'open');

    // Act
    component.updateDatatable();

    // Assert
    expect(spyOnGetReceitas).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalledWith(AlertComponent, errorMessage, AlertType.Warning);
  });

  it('should open modalForm on onClickNovo', fakeAsync(() => {
    // Arrange
    spyOn(component.modalForm.modalService, 'open').and.callThrough();

    // Act
    component.onClickNovo();
    flush();

    // Assert
    expect(component.modalForm.modalService.open).toHaveBeenCalled();
  }));

  it('should open modalform onClickEdit', () => {
    // Arrange
    spyOn(component.modalForm.modalService, 'open').and.callThrough();

    // Act
    component.onClickEdit(mockReceitas[0].id);

    // Assert
    expect(component.modalForm.modalService.open).toHaveBeenCalled();
  });

  it('should open Modal Confirm when onClickDelete', () => {
    // Arrange
    spyOn(component.modalConfirm, 'open').and.callThrough();

    // Act
    component.onClickDelete(mockReceitas[0].id);

    // Assert
    expect(component.modalConfirm.open).toHaveBeenCalled();
  });

});
