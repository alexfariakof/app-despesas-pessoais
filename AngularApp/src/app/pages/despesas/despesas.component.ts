import { Component, OnInit, ViewChild } from "@angular/core";
import dayjs from "dayjs";
import { DespesasFormComponent } from "./despesas-form/despesas.form.component";
import { BarraFerramentaClass, DataTableComponent, AlertComponent, ModalFormComponent, ModalConfirmComponent, AlertType } from "../../shared/components";
import { DespesaDataSet, DespesaColumns } from "../../shared/datatable-config/despesas";
import { IDespesa, IAction } from "../../shared/models";
import { MenuService } from "../../shared/services";
import { DespesaService } from "../../shared/services/api";
@Component({
  selector: 'app-despesas',
  templateUrl: './despesas.component.html',
  styleUrls: ['./despesas.component.scss']
})
export class DespesasComponent implements BarraFerramentaClass, OnInit {
  @ViewChild(DataTableComponent) dataTable: DataTableComponent;
  despesasData: DespesaDataSet[] = [];
  columns = DespesaColumns;

  constructor(
    private menuService: MenuService,
    public modalAlert: AlertComponent,
    public modalForm: ModalFormComponent,
    public modalConfirm: ModalConfirmComponent,
    public despesaService: DespesaService,
    private despesasFormComponent: DespesasFormComponent) { }

  ngOnInit() {
    this.menuService.setMenuSelecionado(3);
    this.initializeDataTable();
  }

  initializeDataTable = () => {
    this.despesaService.getDespesas()
      .subscribe({
        next: (result: IDespesa[]) => {
          if (result) {
            this.despesasData = this.parseToDespesasData(result);
            this.dataTable.loadData(this.despesasData);
            this.dataTable.rerender();
          }
        },
        error: (errorMessage: string) => {
          this.modalAlert.open(AlertComponent, errorMessage, AlertType.Warning);
        }
      });
  }

  updateDatatable = () => {
    this.despesaService.getDespesas()
      .subscribe({
        next: (result: any) => {
          if (result) {
            this.despesasData = this.parseToDespesasData(result);
            this.dataTable.rerender();
          }
        },
        error: (errorMessage: string) => {
          this.modalAlert.open(AlertComponent, errorMessage, AlertType.Warning);
        }
      });
  }

  parseToDespesasData(despesas: IDespesa[]): DespesaDataSet[] {
    return despesas.map((despesa: IDespesa) => ({
      id: despesa.id,
      data: dayjs(despesa.data).format('DD/MM/YYYY'),
      categoria: despesa.categoria.descricao,
      descricao: despesa.descricao,
      valor: `${despesa.valor.toLocaleString('pt-BR', {
        style: 'currency',
        currency: 'BRL',
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
      })}`,
      dataVencimento: (despesa.dataVencimento && dayjs(despesa.dataVencimento).isValid()) ? dayjs(despesa.dataVencimento).format('DD/MM/YYYY') : null
    }));
  }

  onClickNovo = () => {
    const modalRef = this.modalForm.modalService.open(DespesasFormComponent, { centered: true });
    modalRef.shown.subscribe(() => {
      modalRef.componentInstance.action = IAction.Create;
      modalRef.componentInstance.setRefresh(this.updateDatatable);
    });
  }

  onClickEdit = (idDespesa: number) => {
    const modalRef = this.modalForm.modalService.open(DespesasFormComponent, { centered: true });
    modalRef.shown.subscribe(() => {
      modalRef.componentInstance.action = IAction.Edit;
      modalRef.componentInstance.setRefresh(this.updateDatatable);
      modalRef.componentInstance.editDespesa(idDespesa);
    });
  }

  onClickDelete = (idDespesa: number) => {
    const modalRef = this.modalConfirm.open(ModalConfirmComponent, `Deseja excluir a despesa ${this.dataTable.row.descricao} ?`);
    modalRef.shown.subscribe(() => {
      modalRef.componentInstance.setConfirmButton(() => this.despesasFormComponent.deleteDespesa(idDespesa, this.updateDatatable));
    });
  }
}
