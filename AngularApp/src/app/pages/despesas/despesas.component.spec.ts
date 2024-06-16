import { CommonModule } from "@angular/common";
import { HttpClientTestingModule } from "@angular/common/http/testing";
import { ComponentFixture, TestBed, fakeAsync, flush } from "@angular/core/testing";
import { MatNativeDateModule } from "@angular/material/core";
import { MatDatepicker, MatDatepickerModule } from "@angular/material/datepicker";
import { MatSelect, MatSelectModule } from "@angular/material/select";
import { RouterTestingModule } from "@angular/router/testing";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import dayjs from "dayjs";
import { from, throwError } from "rxjs";
import { AlertComponent, ModalFormComponent, ModalConfirmComponent, DataTableComponent, AlertType } from "../../shared/components";
import { DespesaDataSet } from "../../shared/datatable-config/despesas";
import { IDespesa } from "../../shared/models";
import { MenuService } from "../../shared/services";
import { DespesaService } from "../../shared/services/api";
import { SharedModule } from "../../shared/shared.module";
import { DespesasFormComponent } from "./despesas-form/despesas.form.component";
import { DespesasComponent } from "./despesas.component";


describe('Unit Test DespesasComponent', () => {
  let component: DespesasComponent;
  let fixture: ComponentFixture<DespesasComponent>;
  let despesaService: DespesaService;
  let mockDespesas: IDespesa[] = [
    { id: 1, data: dayjs(), descricao: 'Teste Despesas 1', valor: 1.05, dataVencimento: dayjs(), categoria: { id: 1, descricao: 'Categoria 1', idTipoCategoria: 1 }},
    { id: 2, data: dayjs(), descricao: 'Teste Despesas 2', valor: 2.05, dataVencimento: dayjs(), categoria: { id: 2, descricao: 'Categoria 2', idTipoCategoria: 1 }},
    { id: 3, data: dayjs(), descricao: 'Teste Despesas 3', valor: 3.05, dataVencimento: null, categoria: { id: 4, descricao: 'Categoria 4', idTipoCategoria: 1 }},
    { id: 4, data: dayjs(), descricao: 'Teste Despesas 4', valor: 4.44, dataVencimento: null, categoria: { id: 5, descricao: 'Categoria 5', idTipoCategoria: 1 }},
    { id: 5, data: dayjs(), descricao: 'Teste Despesas 5', valor: 5.55, dataVencimento: null, categoria: { id: 6, descricao: 'Categoria 6', idTipoCategoria: 1 }},
  ];
  let mockDespesasData: DespesaDataSet[] = [
    { id: 1, data: dayjs().format('DD/MM/YYYY'), descricao: 'Teste Despesas 1', valor: 'R$ 1.05', dataVencimento: dayjs().format('DD/MM/YYY'), categoria: 'Categoria 1' },
    { id: 2, data: dayjs().format('DD/MM/YYYY'), descricao: 'Teste Despesas 2', valor: 'R$ 2.05', dataVencimento: dayjs().format('DD/MM/YYY'), categoria: 'Categoria 2' },
    { id: 3, data: dayjs().format('DD/MM/YYYY'), descricao: 'Teste Despesas 3', valor: 'R$ 3.05', dataVencimento: null, categoria: 'Categroia 3' }
  ];

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DespesasComponent, DespesasFormComponent, MatDatepicker, MatSelect],
      imports: [CommonModule, RouterTestingModule, SharedModule, HttpClientTestingModule, MatSelectModule, MatDatepickerModule, MatNativeDateModule],
      providers: [MenuService, AlertComponent, ModalFormComponent, ModalConfirmComponent, NgbActiveModal, DespesaService, DespesasFormComponent ]
    });
    fixture = TestBed.createComponent(DespesasComponent);
    component = fixture.componentInstance;
    component.dataTable = TestBed.inject(DataTableComponent);
    component.despesasData = mockDespesasData;
    despesaService = TestBed.inject(DespesaService);
    fixture.detectChanges();
  });

  it('should create', () => {
    // Assert
    expect(component).toBeTruthy();
  });

  it('should initializeDataTable', fakeAsync(() => {
    // Arrange
    const spyOnGetDespesas = spyOn(despesaService, 'getDespesas').and.returnValue(from(Promise.resolve(mockDespesas)));

    // Act
    component.initializeDataTable();
    flush();

    // Assert
    expect(spyOnGetDespesas).toHaveBeenCalled();
    expect(component.despesasData.length).toBeGreaterThan(1);
  }));

  it('should initializeDataTable and return empty Datatable', fakeAsync(() => {
    // Arrange
    const errorMessage = 'Fake Message Datatable Despesas Empty';
    const spyOnGetDespesas = spyOn(despesaService, 'getDespesas').and.returnValue(throwError(errorMessage));
    const alertOpenSpy = spyOn(TestBed.inject(AlertComponent), 'open');

    // Act
    component.initializeDataTable();
    flush();

    // Assert
    expect(spyOnGetDespesas).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalledWith(AlertComponent, errorMessage, AlertType.Warning);
  }));

  it('should throw error when try to initializeDataTable', () => {
    // Arrange
    const errorMessage = 'Fake Error Message';
    const spyOnGetDespesas = spyOn(despesaService, 'getDespesas').and.returnValue(throwError(errorMessage));
    const alertOpenSpy = spyOn(TestBed.inject(AlertComponent), 'open');

    // Act
    component.initializeDataTable();

    // Assert
    expect(spyOnGetDespesas).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalledWith(AlertComponent, errorMessage, AlertType.Warning);
  });

  it('should updateDatatable when is called', fakeAsync(() => {
    // Arrange
    const spyOnGetDespesas = spyOn(despesaService, 'getDespesas').and.returnValue(from(Promise.resolve(mockDespesas)));

    // Act
    component.updateDatatable();
    flush();

    // Assert
    expect(spyOnGetDespesas).toHaveBeenCalled();
    expect(component.despesasData.length).toBeGreaterThan(0);
  }));

  it('should throw error when try to updateDataTable', () => {
    // Arrange
    const errorMessage = 'Fake Error Message';
    const spyOnGetDespesas = spyOn(despesaService, 'getDespesas').and.returnValue(throwError(errorMessage));
    const alertOpenSpy = spyOn(TestBed.inject(AlertComponent), 'open');

    // Act
    component.updateDatatable();

    // Assert
    expect(spyOnGetDespesas).toHaveBeenCalled();
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

  it('should open ModalForm onClickEdit', fakeAsync(() => {
    // Arrange
    spyOn(component.modalForm.modalService, 'open').and.callThrough();

    // Act
    component.onClickEdit(mockDespesas[1].id);
    flush();

    // Assert
    expect(component.modalForm.modalService.open).toHaveBeenCalled();
  }));

  it('should open Modal Confirm when onClickDelete', () => {
    // Arrange
    spyOn(component.modalConfirm, 'open').and.callThrough();

    // Act
    component.onClickDelete(mockDespesas[0].id);

    // Assert
    expect(component.modalConfirm.open).toHaveBeenCalled();
  });
});
