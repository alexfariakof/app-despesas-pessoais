import { NgModule } from '@angular/core';
import { BarraFerramentaComponent } from './barra-ferramenta.component';
import { CommonModule } from '@angular/common';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { SaldoComponent } from '../saldo/saldo.component';
@NgModule({
  declarations: [BarraFerramentaComponent],
  exports: [BarraFerramentaComponent, SaldoComponent],
  imports: [FormsModule, CommonModule, MatInputModule, MatFormFieldModule, MatNativeDateModule, MatDatepickerModule, SaldoComponent]
})
export class BarraFerramentaModule { }
