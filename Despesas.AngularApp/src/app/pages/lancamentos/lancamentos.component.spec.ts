import { LancamentoService } from './../../shared/services/api/lancamentos/lancamento.service';
import { CommonModule } from "@angular/common";
import { ComponentFixture, TestBed, fakeAsync, flush } from "@angular/core/testing";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { AlertComponent, AlertType, DataTableComponent, ModalConfirmComponent, ModalFormComponent } from "src/app/shared/components";
import { FilterMesAnoService, MenuService } from "src/app/shared/services";
import { SharedModule } from "src/app/shared/shared.module";
import { LancamentosComponent } from "./lancamentos.component";
import { DespesasFormComponent } from "../despesas/despesas-form/despesas.form.component";
import { ReceitasFormComponent } from "../receitas/receitas-form/receitas.form.component";
import { HttpClientTestingModule } from "@angular/common/http/testing";
import { RouterTestingModule } from "@angular/router/testing";
import { ILancamento } from 'src/app/shared/models';
import { LancamentoDataSet } from 'src/app/shared/datatable-config/lancamentos';
import { from, throwError } from 'rxjs';
import * as dayjs from 'dayjs';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatSelectModule } from '@angular/material/select';

describe('Unit Test LancamentosComponent', () => {
  let component: LancamentosComponent;
  let fixture: ComponentFixture<LancamentosComponent>;
  let lancamentoService: LancamentoService;
  let mockLancamentos: ILancamento[] = [
    { id: 1, idDespesa: 1, idReceita: 0, data: dayjs().format('YYYY-MM-DD'), tipoCategoria: 'Despesa', categoria: 'Tipo Catgeoria 1', descricao: 'Teste Descrição categoria 1', valor: 50.98 },
    { id: 2, idDespesa: 0, idReceita: 1, data: dayjs().format('YYYY-MM-DD'), tipoCategoria: 'Receita', categoria: 'Tipo Catgeoria 2', descricao: 'Teste Descrição categoria 2', valor: 100.41 },
  ];
  let mockLancamentosData: LancamentoDataSet[] = [
    { id: 1, data: dayjs().format('YYYY-MM-DD'), tipoCategoria: 'Despesa', categoria: 'Tipo Catgeoria 1', descricao: 'Teste Descrição categoria 1', valor: 'R$ 50.98' },
    { id: 1, data: dayjs().format('YYYY-MM-DD'), tipoCategoria: 'Receita', categoria: 'Tipo Catgeoria 2', descricao: 'Teste Descrição categoria 2', valor: 'R$ 100.41' },

  ];

  beforeEach(() => {
      TestBed.configureTestingModule({
      declarations: [LancamentosComponent, DespesasFormComponent, ReceitasFormComponent],
      imports: [CommonModule, SharedModule, RouterTestingModule, HttpClientTestingModule,
        MatFormFieldModule, MatSelectModule, MatDatepickerModule, MatNativeDateModule],
      providers: [MenuService, AlertComponent, NgbActiveModal, ModalFormComponent, ModalConfirmComponent,
        FilterMesAnoService, DespesasFormComponent, ReceitasFormComponent ]
    });
    fixture = TestBed.createComponent(LancamentosComponent);
    component = fixture.componentInstance;
    lancamentoService = TestBed.inject(LancamentoService);
    component.dataTable = TestBed.inject(DataTableComponent);
    component.lancamentosData = mockLancamentosData;
    fixture.detectChanges();
  });

  it('should create', () => {
    // Assert
    expect(component).toBeTruthy();
  });

  it('should initializeDataTable', fakeAsync(() => {
    // Arrange
    const getLancamntosByMesAnoSpy = spyOn(lancamentoService, 'getLancamentosByMesAno').and.returnValue(from(Promise.resolve(mockLancamentos)));

    // Act
    component.initializeDataTable();
    flush();

    // Assert
    expect(getLancamntosByMesAnoSpy).toHaveBeenCalled();
    console.log('Pode Ocorrer erro nesta assertividade por conta da data');
    expect(getLancamntosByMesAnoSpy).toHaveBeenCalledWith(dayjs(component.filterMesAnoService.dataMesAno));
    expect(component.lancamentosData.length).toBeGreaterThan(1);
  }));

  it('should throws error when initializeDataTable and show modal alert', fakeAsync(() => {
    // Arrange
    const errorMessage = 'Fake Error Message';
    const getLancamntosByMesAnoSpy = spyOn(lancamentoService, 'getLancamentosByMesAno').and.returnValue(throwError(errorMessage));
    const alertOpenSpy = spyOn(TestBed.inject(AlertComponent), 'open');

    // Act
    component.initializeDataTable();
    flush();

    // Assert
    expect(getLancamntosByMesAnoSpy).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalledWith(AlertComponent, errorMessage, AlertType.Warning);
  }));

  it('should updateDatatable when is called', fakeAsync(() => {
    // Arrange
    let mockDAtaMesAno = dayjs().toISOString();
    const getLancamntosByMesAnoSpy = spyOn(lancamentoService, 'getLancamentosByMesAno').and.returnValue(from(Promise.resolve(mockLancamentos)));

    // Act
    component.barraFerramenta.filterMesAnoService.dataMesAno = mockDAtaMesAno;
    component.updateDatatable();
    flush();

    // Assert
    expect(getLancamntosByMesAnoSpy).toHaveBeenCalled();
    expect(getLancamntosByMesAnoSpy).toHaveBeenCalledWith(dayjs(mockDAtaMesAno));
    expect(component.lancamentosData.length).toBeGreaterThan(0);
  }));

  it('should throw error when try to updateDataTable', () => {
    // Arrange
    const errorMessage = 'Fake Error Message Lançamentos UpdateDataTable';
    const getLancamntosByMesAnoSpy = spyOn(lancamentoService, 'getLancamentosByMesAno').and.returnValue(throwError(errorMessage));
    const alertOpenSpy = spyOn(TestBed.inject(AlertComponent), 'open');

    // Act
    component.updateDatatable();

    // Assert
    expect(getLancamntosByMesAnoSpy).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalledWith(AlertComponent, errorMessage, AlertType.Warning);
  });

  it('should open modalform Despesa onClickEdit', () => {
    // Arrange
    spyOn(component.modalForm.modalService, 'open').and.callThrough();

    // Act
    component.onClickEdit(1, 'Despesa');

    // Assert
    expect(component.modalForm.modalService.open).toHaveBeenCalled();
  });

  it('should open modalform Receita onClickEdit', () => {
    // Arrange
    spyOn(component.modalForm.modalService, 'open').and.callThrough();

    // Act
    component.onClickEdit(1, 'Receita');

    // Assert
    expect(component.modalForm.modalService.open).toHaveBeenCalled();
  });

  it('should open Modal Confirm Despesa when onClickDelete', () => {
    // Arrange
    spyOn(component.modalConfirm, 'open').and.callThrough();

    // Act
    component.onClickDelete(1, 'Despesa');

    // Assert
    expect(component.modalConfirm.open).toHaveBeenCalled();
  });

  it('should open Modal Confirm Receita when onClickDelete', () => {
    // Arrange
    spyOn(component.modalConfirm, 'open').and.callThrough();

    // Act
    component.onClickDelete(1, 'Receita');

    // Assert
    expect(component.modalConfirm.open).toHaveBeenCalled();
  });
});
