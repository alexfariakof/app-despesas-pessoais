import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AbstractService } from '../base/AbstractService';

@Injectable({
  providedIn: 'root'
})

export class ImagemPerfilService extends AbstractService {
  constructor(public httpClient: HttpClient) {
    const ROUTE = 'Usuario/ImagemPerfil';
    super(ROUTE);
  }

  getImagemPerfilUsuario(): any {
    return this.httpClient.get(`${ this.routeUrl }`);
  }

  createImagemPerfilUsuario(file: File): any {
    const formData = new FormData();
    formData.append('file', file);
    return this.httpClient.post(`${ this.routeUrl }`, formData);
  }

  updateImagemPerfilUsuario(file: File): any {
    const formData = new FormData();
    formData.append('file', file);
    return this.httpClient.put(`${ this.routeUrl }`, formData);
  }

  deleteImagemPerfilUsuario(): any {
    return this.httpClient.delete(`${ this.routeUrl }`);
  }
}
