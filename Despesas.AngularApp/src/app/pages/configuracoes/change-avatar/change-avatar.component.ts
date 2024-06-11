import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AlertComponent, AlertType } from 'src/app/shared/components';
import { IImagemPerfil } from 'src/app/shared/models';
import { ImagemPerfilService } from 'src/app/shared/services/api';
@Component({
  selector: 'app-change-avatar',
  templateUrl: './change-avatar.component.html',
  styleUrls: ['./change-avatar.component.scss']
})
export class ChangeAvatarComponent implements OnInit {
  @Input() handleAvatarUploaded: (event: Event) => void;
  formAvatar: FormGroup;
  file: File | null = null;
  fileLoaded = false;
  imagemPerfilUsuario: IImagemPerfil = { url: '../../../../assets/perfil_static.png' };

  constructor(
    public imagemPerfilService: ImagemPerfilService,
    public modalAlert: AlertComponent
  ) {
    this.formAvatar = new FormGroup({
      uploadPhoto: new FormControl(null),
    });
  }

  ngOnInit(): void {
    this.initialize();
  }

  initialize = (): void => {
    this.imagemPerfilService.getImagemPerfilUsuario()
      .subscribe({
        next: (response: IImagemPerfil) => {
          if (response && response !== undefined && response!== null) {
            this.imagemPerfilUsuario = response;
          }
        },
        error: (errorMessage: string) => {
          this.modalAlert.open(AlertComponent, errorMessage, AlertType.Warning);
        }
      });;
  }

  handleAvatarUpload = (event: any): void => {
    const uploadedFile = event.target.files?.[0];
    if (uploadedFile) {
      this.file = uploadedFile;
      this.imagemPerfilUsuario.url = URL.createObjectURL(uploadedFile);
      this.fileLoaded = true;
    }
  }

  handleImagePerfil = (): void => {
    if (this.file !== null) {
      if (this.imagemPerfilUsuario.id === undefined || this.imagemPerfilUsuario.id === null) {
        this.imagemPerfilService.createImagemPerfilUsuario(this.file)
          .subscribe({
            next: (result: IImagemPerfil) => {
              if (result) {
                this.file = null;
                this.fileLoaded = false;
                this.imagemPerfilUsuario = result;
                this.modalAlert.open(AlertComponent, 'Imagem adicionada com sucesso!', AlertType.Success);
              }
            },
            error: (errorMessage: string) => {
              this.modalAlert.open(AlertComponent, errorMessage, AlertType.Warning);
            }
          });
      }
      else {
        this.imagemPerfilService.updateImagemPerfilUsuario(this.file)
          .subscribe({
            next: (result: IImagemPerfil) => {
              if (result) {
                this.file = null;
                this.fileLoaded = false;
                this.imagemPerfilUsuario = result;
                this.modalAlert.open(AlertComponent, 'Imagem de perfil usuário alterada com sucesso!', AlertType.Success);
              }
            },
            error: (errorMessage: string) => {
              this.modalAlert.open(AlertComponent, errorMessage, AlertType.Warning);
            }
          });
      }
    }
    else {
      this.modalAlert.open(AlertComponent, 'É preciso carregar uma nova imagem!', AlertType.Warning);
    }
  }

  handleDeleteImagePerfil = (): void => {
    this.imagemPerfilService.deleteImagemPerfilUsuario()
      .subscribe({
        next: (result: boolean) => {
          if (result) {
            const modalRef = this.modalAlert.open(AlertComponent, 'Imagem de perfil usuário excluída com sucesso!', AlertType.Success);
            modalRef.shown.subscribe(() => {
              this.file = null;
              this.fileLoaded = false;
              this.imagemPerfilUsuario = null;
              this.imagemPerfilUsuario = { url: '../../../../assets/perfil_static.png' }
            })
          }
        },
        error: (errorMessage: string) => {
          this.modalAlert.open(AlertComponent, errorMessage, AlertType.Warning);
        }
      });
  }
}
