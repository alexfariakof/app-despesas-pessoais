import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import * as dayjs from 'dayjs';
import { AlertComponent, AlertType } from 'src/app/shared/components';
import { ICategoria, IReceita, IAction } from 'src/app/shared/models';
import { ReceitaService } from 'src/app/shared/services/api';
import { CustomValidators } from 'src/app/shared/validators';

@Component({
  selector: 'app-receitas-form',
  templateUrl: './receitas.form.component.html',
  styleUrls: ['./receitas.form.component.scss']
})

export class ReceitasFormComponent {
  categorias: ICategoria[] = [];
  receitaForm: FormGroup & IReceita;
  action: IAction = IAction.Create;
  refresh: Function = () => { };
  setRefresh(_refresh: Function): void {
    this.refresh = _refresh;
  }

  constructor(
    public formbuilder: FormBuilder,
    public modalAlert: AlertComponent,
    public activeModal: NgbActiveModal,
    public receitaService: ReceitaService
  ) { }

  ngOnInit(): void {
    this.getCatgeoriasFromReceitas();
    this.receitaForm = this.formbuilder.group({
      id: [0],
      categoria: [null, Validators.required],
      data: [dayjs().format('YYYY-MM-DD'), Validators.required],
      descricao: ['', Validators.required],
      valor: [0, [Validators.required, CustomValidators.isGreaterThanZero]],
    }) as FormGroup & IReceita;
  }

  onSaveClick = () => {
    switch (this.action) {
      case IAction.Create:
        this.saveCreateReceita();
        break;
      case IAction.Edit:
        this.saveEditReceita();
        break;
      default:
        this.modalAlert.open(AlertComponent, 'Ação não pode ser realizada.', AlertType.Warning);
    }
  }

  getCatgeoriasFromReceitas = () => {
    this.receitaService.getReceitasCategorias()
      .subscribe({
        next: (result: ICategoria[]) => {
          if (result)
            this.categorias = result;
        },
        error: (errorMessage: string) => {
          this.modalAlert.open(AlertComponent, errorMessage, AlertType.Warning);
        }
      });
  }

  saveCreateReceita = () => {
    this.receitaService.postReceita(this.receitaForm.getRawValue() as IReceita)
      .subscribe({
        next: (result: IReceita) => {
          if (result) {
            this.activeModal.close();
            this.refresh();
            this.modalAlert.open(AlertComponent, 'Receita cadastrada com Sucesso.', AlertType.Success);
          }
        },
        error: (errorMessage: string) => {
          this.modalAlert.open(AlertComponent, errorMessage, AlertType.Warning);
        }
      });
  }

  saveEditReceita = () => {
    this.receitaService.putReceita(this.receitaForm.getRawValue() as IReceita)
      .subscribe({
        next: (response: IReceita) => {
          if (response !== undefined && response !== null && response) {
            this.activeModal.close();
            this.refresh();
            this.modalAlert.open(AlertComponent, 'Receita alterada com Sucesso.', AlertType.Success);
          }
        },
        error: (errorMessage: string) => {
          this.modalAlert.open(AlertComponent, errorMessage, AlertType.Warning);
        }
      });
  }

  editReceita = (idReceita: number) => {
    this.receitaService.getReceitaById(idReceita)
      .subscribe({
        next: (response: IReceita) => {
          if (response && response !== undefined && response !== null){
            const receitaData = response
            this.receitaForm.patchValue(receitaData);
            const categoriaSelecionada = this.categorias.find(c => c.id === receitaData.categoria.id);
            if (categoriaSelecionada) {
              this.receitaForm.get('categoria')?.setValue(categoriaSelecionada);
            }
          }
        },
        error: (errorMessage: string) => {
          this.modalAlert.open(AlertComponent, errorMessage, AlertType.Warning);
        }
      });
  }

  deleteReceita = (idReceita: number, callBack: Function) => {
    this.receitaService.deleteReceita(idReceita)
      .subscribe({
        next: (response: boolean) => {
          if (response) {
            callBack();
            this.modalAlert.open(AlertComponent, 'Receita excluída com sucesso', AlertType.Success);
          }
          else {
            this.modalAlert.open(AlertComponent, 'Erro ao excluír receita', AlertType.Warning);
          }
        },
        error: (errorMessage: string) => {
          this.modalAlert.open(AlertComponent, errorMessage, AlertType.Warning);
        }
      });
  }
}
