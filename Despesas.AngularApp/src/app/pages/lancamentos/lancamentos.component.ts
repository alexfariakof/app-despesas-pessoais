import { Component, OnInit, ViewChild } from "@angular/core";
import { AlertComponent, AlertType, BarraFerramentaComponent, DataTableComponent, ModalConfirmComponent, ModalFormComponent } from "src/app/shared/components";
import { FilterMesAnoService, MenuService } from "src/app/shared/services";
import { ReceitasFormComponent } from '../receitas/receitas-form/receitas.form.component';
import { DespesasFormComponent } from './../despesas/despesas-form/despesas.form.component';
import { LancamentoService } from "src/app/shared/services/api";
import * as dayjs from "dayjs";
import { LancamentoDataSet, LancamentoColumns } from "src/app/shared/datatable-config/lancamentos";
import { IAction, ILancamento } from "src/app/shared/models";
@Component({
  selector: 'app-lancamentos',
  templateUrl: './lancamentos.component.html',
  styleUrls: ['./lancamentos.component.scss']
})

export class LancamentosComponent implements OnInit {
  @ViewChild(BarraFerramentaComponent) barraFerramenta: BarraFerramentaComponent;
  @ViewChild(DataTableComponent) dataTable: DataTableComponent;
  lancamentosData: LancamentoDataSet[] = [];
  columns = LancamentoColumns;

  constructor(
    private menuService: MenuService,
    public modalAlert: AlertComponent,
    public modalForm: ModalFormComponent,
    public modalConfirm: ModalConfirmComponent,
    public lancamentoservice: LancamentoService,
    private despesasFormComponent: DespesasFormComponent,
    private receitasFormComponent: ReceitasFormComponent,
    public filterMesAnoService: FilterMesAnoService
  ) { }

  ngOnInit() {
    this.menuService.setMenuSelecionado(5);
    this.initializeDataTable();
  }

  initializeDataTable = () => {
    this.lancamentoservice.getLancamentosByMesAno(dayjs(this.filterMesAnoService.dataMesAno))
      .subscribe({
        next: (response: ILancamento[]) => {
          if (response) {
            this.lancamentosData = this.parseToLancamentosData(response);
            this.dataTable.loadData(this.lancamentosData);
            this.barraFerramenta.setOnChangeDataMesAno(this.updateDatatable);
            this.dataTable.rerender();
          }
        },
        error: (errorMessage: string) => {
          this.modalAlert.open(AlertComponent, errorMessage, AlertType.Warning);
        }
      });
  }

  updateDatatable = () => {
    this.lancamentoservice.getLancamentosByMesAno(dayjs(this.filterMesAnoService.dataMesAno))
      .subscribe({
        next: (response: ILancamento[]) => {
          if (response) {
            this.lancamentosData = this.parseToLancamentosData(response);
            this.dataTable.rerender();
          }
        },
        error: (errorMessage: string) => {
          this.modalAlert.open(AlertComponent, errorMessage, AlertType.Warning);
        }
      });
  }

  parseToLancamentosData(lancamentos: ILancamento[]): LancamentoDataSet[] {
    return lancamentos.map((lancamento: ILancamento) => ({
      id: lancamento.idDespesa === 0 ? lancamento.idReceita : lancamento.idDespesa,
      data: lancamento.data,
      tipoCategoria: lancamento.tipoCategoria,
      categoria: lancamento.categoria,
      descricao: lancamento.descricao,
      valor: `${lancamento.valor.toLocaleString('pt-BR', {
        style: 'currency',
        currency: 'BRL',
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
      })}`
    }));
  }

  onClickEdit = (id: number, tipoCategoria: string) => {
    let modalRef;
    if (tipoCategoria === 'Despesa') {
      modalRef = this.modalForm.modalService.open(DespesasFormComponent, { centered: true });
      modalRef.shown.subscribe(() => {
        modalRef.componentInstance.action = IAction.Edit;
        modalRef.componentInstance.setRefresh(this.updateDatatable);
        modalRef.componentInstance.editDespesa(id);
      });
    }
    else {
      modalRef = this.modalForm.modalService.open(ReceitasFormComponent, { centered: true });
      modalRef.shown.subscribe(() => {
        modalRef.componentInstance.action = IAction.Edit;
        modalRef.componentInstance.setRefresh(this.updateDatatable);
        modalRef.componentInstance.editReceita(id);
      });
    }
  }

  onClickDelete = (id: number, tipoCategoria: string) => {
    let modalRef;
    if (tipoCategoria === 'Despesa') {
      modalRef = this.modalConfirm.open(ModalConfirmComponent, `Deseja excluir a despesa ${this.dataTable.row.descricao} ?`);
      modalRef.shown.subscribe(() => {
        modalRef.componentInstance.setConfirmButton(() => this.despesasFormComponent.deleteDespesa(id, this.updateDatatable));
      });
    }
    else {
      modalRef = this.modalConfirm.open(ModalConfirmComponent, `Deseja excluir a receita ${this.dataTable.row.descricao} ?`);
      modalRef.shown.subscribe(() => {
        modalRef.componentInstance.setConfirmButton(() => this.receitasFormComponent.deleteReceita(id, this.updateDatatable));
      });
    }
  }
}
