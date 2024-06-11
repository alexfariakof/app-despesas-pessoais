import { MenuService } from 'src/app/shared/services';
import { Component, OnInit } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { IUsuario } from 'src/app/shared/models';
import { AlertComponent, AlertType } from 'src/app/shared/components';
import { UsuarioService } from 'src/app/shared/services/api';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {
  prefilFrom: FormGroup & IUsuario = this.formbuilder.group({}) as FormGroup & IUsuario;

  constructor(
    private menuService: MenuService,
    public formbuilder: FormBuilder,
    public modalAlert: AlertComponent,
    public usuarioService: UsuarioService
  ) { this.menuService.setMenuSelecionado(6); }

  ngOnInit(): void {
    this.prefilFrom = this.formbuilder.group({
      id: 0,
      email: ['', [Validators.required, Validators.email]],
      nome: ['', [Validators.required]],
      sobreNome: '',
      telefone: ['', [Validators.required]],
    }) as FormGroup & IUsuario;
    this.initialize();
  }

  initialize = () => {
    this.usuarioService.getUsuario()
      .subscribe({
        next: (response: IUsuario) => {
          if (response && response !== undefined && response !== null) {
            this.prefilFrom.patchValue(response);
          }
        },
        error: (errorMessage: string) => {
          this.modalAlert.open(AlertComponent, errorMessage, AlertType.Warning);
        }
      });
  }

  onSaveClick() {
    this.usuarioService.putUsuario(this.prefilFrom.getRawValue() as IUsuario)
      .subscribe({
        next: (response: IUsuario) => {
          if (response && response !== undefined && response !== null) {
            this.modalAlert.open(AlertComponent, 'Dados atualizados com Sucesso.', AlertType.Success);
          }
        },
        error: (errorMessage: string) => {
          this.modalAlert.open(AlertComponent, errorMessage, AlertType.Warning);
        }
      });
  }
}
