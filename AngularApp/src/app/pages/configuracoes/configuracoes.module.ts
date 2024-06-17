import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConfiguracoesRoutingModule } from './configuracoes.routing.module';
import { ConfiguracoesComponent } from './configuracoes.component';
import { ChangeAvatarComponent } from './change-avatar/change-avatar.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { MdbFormsModule } from 'mdb-angular-ui-kit/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../../shared/shared.module';
@NgModule({
  declarations : [ConfiguracoesComponent, ChangeAvatarComponent, ChangePasswordComponent],
  imports: [CommonModule, ReactiveFormsModule, ConfiguracoesRoutingModule, SharedModule, MdbFormsModule]
})
export class ConfiguracoesModule {}
