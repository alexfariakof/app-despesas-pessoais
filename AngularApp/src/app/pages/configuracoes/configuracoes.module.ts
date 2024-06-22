import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConfiguracoesRoutingModule } from './configuracoes.routing.module';
import { ConfiguracoesComponent } from './configuracoes.component';
import { ChangeAvatarComponent } from './change-avatar/change-avatar.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../../shared/shared.module';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
@NgModule({
  declarations : [ConfiguracoesComponent, ChangeAvatarComponent, ChangePasswordComponent],
  imports: [CommonModule, ReactiveFormsModule, ConfiguracoesRoutingModule, MatIconModule, MatFormFieldModule, MatInputModule, SharedModule]
})
export class ConfiguracoesModule {}
