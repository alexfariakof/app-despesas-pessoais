import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PerfilRoutingModule } from './perfil.routing.module';
import { PerfilComponent } from './perfil.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatNativeDateModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { NgxMaskDirective, NgxMaskPipe, provideNgxMask } from 'ngx-mask';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
  declarations: [PerfilComponent ],
  imports: [CommonModule, FormsModule, ReactiveFormsModule, PerfilRoutingModule, SharedModule,
            MatIconModule, MatInputModule, MatFormFieldModule, MatNativeDateModule, NgxMaskDirective, NgxMaskPipe],
  providers: [provideNgxMask()]

})
export class PerfilModule { }
