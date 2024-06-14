import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { MatSelectModule } from "@angular/material/select";
import { CurrencyMaskModule } from "ng2-currency-mask";
import { LayoutComponent, BarraFerramentaComponent, DataTableComponent } from "./components";
import { BarraFerramentaModule } from "./components/barra-ferramenta-component/barra-ferramenta.component.module";
import { DataTableModule } from "./components/data-table/data-table.component.module";

@NgModule({
  declarations: [LayoutComponent],
  imports: [CommonModule, FormsModule, BarraFerramentaModule, CurrencyMaskModule],
  exports: [LayoutComponent, BarraFerramentaComponent, DataTableModule, CurrencyMaskModule, MatSelectModule],
  providers: [DataTableComponent]
})

export class SharedModule { }
