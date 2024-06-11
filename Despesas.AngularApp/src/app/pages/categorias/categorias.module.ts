import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { MdbFormsModule } from "mdb-angular-ui-kit/forms";
import { CategoriaService } from "src/app/shared/services/api";
import { SharedModule } from "src/app/shared/shared.module";
import { CategoriasFormComponent } from "./categorias-form/categorias.form.component";
import { CategoriasComponent } from "./categorias.component";
import { CategoriaRoutingModule } from "./categorias.routing.module";
@NgModule({
    declarations: [CategoriasComponent, CategoriasFormComponent ],
    imports: [CommonModule, CategoriaRoutingModule,  ReactiveFormsModule, MdbFormsModule, SharedModule],
    providers: [CategoriaService]
})

export class CategoriasModule {}
