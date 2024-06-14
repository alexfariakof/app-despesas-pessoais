import { AbstractControl, FormGroup, ValidationErrors } from '@angular/forms';
export class CustomValidators {
  static isGreaterThanZero(control: AbstractControl): ValidationErrors | null {
    let value = control.value;
    if (value !== null && value > 0) {
      return null;
    } else {
      return { greaterThanZero: true };
    }
  }

  static isValidPassword(group: FormGroup): ValidationErrors | null {
    const senha = group.get('senha');
    const confirmaSenha = group.get('confirmaSenha');

    if (senha && confirmaSenha && senha.value !== confirmaSenha.value) {
      confirmaSenha.setErrors({ isValidPassword: true });
      return { isValidPassword: true };
    } else {
      confirmaSenha.setErrors(null);
      return null;
    }
  }
}
