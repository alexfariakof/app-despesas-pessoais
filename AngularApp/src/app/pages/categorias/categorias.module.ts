import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { CategoriasFormComponent } from "./categorias-form/categorias.form.component";
import { CategoriasComponent } from "./categorias.component";
import { CategoriaRoutingModule } from "./categorias.routing.module";
import { CategoriaService } from "../../shared/services/api";
import { SharedModule } from "../../shared/shared.module";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from "@angular/material/icon";
import { MatInputModule } from "@angular/material/input";
import { MatSelectModule } from "@angular/material/select";

@NgModule({
  declarations: [CategoriasComponent, CategoriasFormComponent],
  imports: [CommonModule, CategoriaRoutingModule, ReactiveFormsModule,
    MatIconModule, MatInputModule, MatFormFieldModule, MatSelectModule, SharedModule],
  providers: [CategoriaService]
})

export class CategoriasModule { }
