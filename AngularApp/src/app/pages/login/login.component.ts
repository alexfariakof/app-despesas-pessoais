import { Component, OnInit } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { map, catchError } from "rxjs";
import { AlertComponent, AlertType } from "../../shared/components";
import { ILogin, IAuth } from "../../shared/models";
import { AuthService } from "../../shared/services";
import { ControleAcessoService } from "../../shared/services/api";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit{
  loginForm: FormGroup & ILogin;
  showPassword = false;
  eyeIconClass: string = 'bi-eye';

  constructor(
    private formbuilder: FormBuilder,
    public router: Router,
    public controleAcessoService: ControleAcessoService,
    public authProviderService: AuthService,
    public modalALert: AlertComponent ){  }

  ngOnInit(): void{
    this.loginForm = this.formbuilder.group({
        email: ['teste@teste.com', [Validators.required, Validators.email]],
        senha: ['12345T!', [Validators.required, Validators.nullValidator]]
    }) as FormGroup & ILogin;
  }

  onLoginClick() {
    let login: ILogin = this.loginForm.getRawValue();

    this.controleAcessoService.signIn(login).pipe(
      map((response: IAuth) => {
        if (response.authenticated) {
          return this.authProviderService.createAccessToken(response);
        }
        else {
          throw (response);
        }
      }),
      catchError((error) => {
        if (error && typeof error.message === 'string'){
          throw (error.message);
        }
        throw (error);
      })
    )
    .subscribe({
      next: (response: boolean) => {
        if (response)
          this.router.navigate(['/dashboard']);
      },
      error :(errorMessage: string) =>  {
        this.modalALert.open(AlertComponent, errorMessage, AlertType.Warning);
      }
    });
  }

  onTooglePassword() {
    this.showPassword = !this.showPassword;
    this.eyeIconClass = (this.eyeIconClass === 'bi-eye') ? 'bi-eye-slash' : 'bi-eye';
  }
}
