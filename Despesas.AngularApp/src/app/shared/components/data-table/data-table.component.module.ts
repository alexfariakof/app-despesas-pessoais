import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DataTableComponent } from './data-table.component';
import { DataTablesModule } from 'angular-datatables';

@NgModule({
  declarations: [DataTableComponent],
  exports: [DataTableComponent],
  imports: [CommonModule, DataTablesModule ]
})
export class DataTableModule { }

