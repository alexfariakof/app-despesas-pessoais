import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { AlertComponent, AlertType } from '../../../shared/components';
import { ICategoria, IAction } from '../../../shared/models';
import { CategoriaService } from '../../../shared/services/api';

@Component({
  selector: 'app-categorias-form',
  templateUrl: './categorias.form.component.html',
  styleUrls: ['./categorias.form.component.scss']
})

export class CategoriasFormComponent implements OnInit {
  categoriatForm: FormGroup & ICategoria;
  setCategoria(categoria): void {
    this.categoriatForm.patchValue(categoria);
  }

  private action: IAction = IAction.Create;
  setAction(_action: IAction) {
    this.action = _action;
  }

  private refresh: Function = () => { };
  setRefresh(_refresh: Function) {
    this.refresh = _refresh;
  }

  constructor(
    public formbuilder: FormBuilder,
    public modalAlert: AlertComponent,
    public activeModal: NgbActiveModal,
    public categoriaService: CategoriaService
  ) { }

  ngOnInit(): void {
    this.categoriatForm = this.formbuilder.group({
      id: [0],
      descricao: ['', Validators.required],
      idTipoCategoria: [null, Validators.required]
    }) as FormGroup & ICategoria;
  }

  onSaveClick = () => {
    let rawValue = this.categoriatForm.getRawValue();
    let categoria: ICategoria = {
      ...rawValue,
      idTipoCategoria: Number(rawValue.idTipoCategoria)
    };

    try {
      if (this.action === IAction.Create) {

        this.categoriaService.postCategoria(categoria)
          .subscribe({
            next: (response: ICategoria) => {
              if (response) {
                this.activeModal.close();
                this.refresh();
                this.modalAlert.open(AlertComponent, "Categoria cadastrada com Sucesso.", AlertType.Success);
              }
            },
            error: (error: any) => {
              this.modalAlert.open(AlertComponent, error, AlertType.Warning);
            }
          });
      }
      else if (this.action === IAction.Edit) {
        this.categoriaService.putCategoria(categoria)
          .subscribe({
            next: (response: any) => {
              if (response !== undefined && response !== null) {
                this.activeModal.close();
                this.refresh();
                this.modalAlert.open(AlertComponent, "Categoria alterada com Sucesso.", AlertType.Success);
              }
            },
            error: (error: any) => {
              this.modalAlert.open(AlertComponent, error, AlertType.Warning);
            }
          });
      }
    }
    catch (error) {
      this.modalAlert.open(AlertComponent, error, AlertType.Warning);
    }
  }
}
