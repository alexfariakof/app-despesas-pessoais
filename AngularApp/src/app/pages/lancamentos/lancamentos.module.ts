import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { LancamentosComponent } from "./lancamentos.component";
import { LancamentosRoutingModule } from "./lancamentos.routing.module";
import { ReceitasFormComponent } from "../receitas/receitas-form/receitas.form.component";
import { DespesasFormComponent } from "../despesas/despesas-form/despesas.form.component";
import { ReactiveFormsModule } from "@angular/forms";
import { MatNativeDateModule } from "@angular/material/core";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from "@angular/material/icon";
import { MatInputModule } from "@angular/material/input";
import { SharedModule } from "../../shared/shared.module";

@NgModule({
  declarations: [LancamentosComponent ],
  imports: [CommonModule, LancamentosRoutingModule, ReactiveFormsModule, SharedModule, MatIconModule, MatInputModule, MatFormFieldModule, MatNativeDateModule, MatDatepickerModule],
  providers: [DespesasFormComponent, ReceitasFormComponent]
})
export class LancamentosModule {}
