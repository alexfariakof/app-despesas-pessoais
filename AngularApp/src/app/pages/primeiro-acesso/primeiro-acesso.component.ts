import { Component, OnInit } from "@angular/core";
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule, FormsModule } from "@angular/forms";
import { Router } from "@angular/router";
import { map, catchError } from "rxjs";
import { AlertComponent, AlertType } from "../../shared/components";
import { IControleAcesso } from "../../shared/models";
import { ControleAcessoService } from "../../shared/services/api";
import { CustomValidators } from "../../shared/validators";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { CommonModule } from "@angular/common";
import { MatIconModule } from "@angular/material/icon";

@Component({
  selector: 'app-primeiro-acesso',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule,  FormsModule, MatIconModule, MatFormFieldModule, MatInputModule ],
  templateUrl: './primeiro-acesso.component.html',
  styleUrls: ['./primeiro-acesso.component.scss'],

})
export class PrimeiroAcessoComponent  implements OnInit {
  createAccountFrom : FormGroup & IControleAcesso;
  eyeIconClass: string = 'bi-eye';
  eyeIconClassConfirmaSenha: string = 'bi-eye';
  showSenha = false;
  showConfirmaSenha = false;

  constructor(
    public formbuilder: FormBuilder,
    public router: Router,
    public controleAcessoService: ControleAcessoService,
    public modalALert: AlertComponent) {
  }

  ngOnInit(): void{
    this.createAccountFrom = this.formbuilder.group({
      email: ['', [Validators.required, Validators.email]],
      nome: ['', [Validators.required]],
      sobreNome: '',
      telefone: ['', [Validators.required]],
      senha: ['', [Validators.required]],
      confirmaSenha: ['', [Validators.required]]
    }, {
      validator: CustomValidators.isValidPassword
    })as FormGroup & IControleAcesso;
  }

  onSaveClick() {
    let controleAcesso: IControleAcesso = this.createAccountFrom.getRawValue();
    this.controleAcessoService.createUsuario(controleAcesso).pipe(
      map((response: boolean) => {
        if (response) {
          return response;
        } else {
          throw Error("Erro ao realizar cadastro.");
        }
      }),
      catchError((error) => {
        if (error.status === 400) {
          const validationErrors = error.errors;
          if (validationErrors) {
            Object.keys(validationErrors).forEach(field => {
              throw validationErrors[field][0];
            });
          }
        }
        throw (error);
      })
    )
    .subscribe({
      next: (result: boolean) => {
        if (result){
          this.modalALert.open(AlertComponent, "Cadastro realizado com sucesso!", AlertType.Success);
          this.router.navigate(['/login']);
        }
      },
      error: (errorMessage: string) =>  this.modalALert.open(AlertComponent, errorMessage, AlertType.Warning)
    });
  }

  onToogleSenha() {
    this.showSenha = !this.showSenha;
    this.eyeIconClass = (this.eyeIconClass === 'bi-eye') ? 'bi-eye-slash' : 'bi-eye';
  }

  onToogleConfirmaSenha(){
    this.showConfirmaSenha = !this.showConfirmaSenha;
    this.eyeIconClassConfirmaSenha = (this.eyeIconClassConfirmaSenha === 'bi-eye') ? 'bi-eye-slash' : 'bi-eye';
  }

}
