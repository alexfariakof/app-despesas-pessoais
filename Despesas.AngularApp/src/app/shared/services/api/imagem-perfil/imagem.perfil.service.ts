import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AbstractService } from '../base/AbstractService';

@Injectable({
  providedIn: 'root'
})

export class ImagemPerfilService extends AbstractService {
  constructor(public httpClient: HttpClient) {
    super();
    this.urlPath = 'Usuario/ImagemPerfil';
  }

  getImagemPerfilUsuario(): any {
    return this.httpClient.get(`${ this.urlPath }`);
  }

  createImagemPerfilUsuario(file: File): any {
    const formData = new FormData();
    formData.append('file', file);
    return this.httpClient.post(`${ this.urlPath }`, formData);
  }

  updateImagemPerfilUsuario(file: File): any {
    const formData = new FormData();
    formData.append('file', file);
    return this.httpClient.put(`${ this.urlPath }`, formData);
  }

  deleteImagemPerfilUsuario(): any {
    return this.httpClient.delete(`${ this.urlPath }`);
  }
}
