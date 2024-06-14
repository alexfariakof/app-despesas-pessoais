import { ComponentFixture, TestBed, fakeAsync, flush } from '@angular/core/testing';
import { LayoutComponent } from './layout.component';
import { CommonModule } from '@angular/common';
import { MenuService } from '../../services/utils/menu-service/menu.service';
import { AuthService } from '../../services/auth/auth.service';
import { Router } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ImagemPerfilService } from '../../services/api';
import { from, throwError } from 'rxjs';

describe('Unit Test LayoutComponent', () => {
  let component: LayoutComponent;
  let fixture: ComponentFixture<LayoutComponent>;
  let mockAuthService: jasmine.SpyObj<AuthService>;
  let imagemPerfilService: ImagemPerfilService;
  let router: Router;

  beforeEach(() => {
    mockAuthService = jasmine.createSpyObj('AuthService', ['clearSessionStorage']);
    mockAuthService.clearSessionStorage.and.callThrough();
    TestBed.configureTestingModule({
      declarations: [LayoutComponent],
      imports: [CommonModule, RouterTestingModule, HttpClientTestingModule],
      providers: [MenuService,
        { provide: AuthService, useValue: mockAuthService }
      ]
    });
    fixture = TestBed.createComponent(LayoutComponent);
    component = fixture.componentInstance;
    router = TestBed.inject(Router);
    imagemPerfilService = TestBed.inject(ImagemPerfilService);
    fixture.detectChanges();
  });

  it('should create', () => {
    // Assert
    expect(component).toBeTruthy();
  });

  it('should initialize correctlly', fakeAsync(() => {
    // Arrange
    let mockResponse =  { url: 'http://testeimagemperfil.png' };
    const spyOnImagemPerfilService = spyOn(imagemPerfilService, 'getImagemPerfilUsuario').and.returnValue(from(Promise.resolve(mockResponse)));

    // Act
    component.initialize();
    flush();

    // Assert
    expect(spyOnImagemPerfilService).toHaveBeenCalled();
    expect(component.urlPerfilImage).toEqual(mockResponse.url);
  }));

  it('should throws error and fill with default path imagemPerfil', fakeAsync(() => {
    // Arrange
    const spyOnImagemPerfilService = spyOn(imagemPerfilService, 'getImagemPerfilUsuario').and.returnValue(throwError('Imagem não encontrada!'));

    // Act
    component.initialize();
    flush();

    // Assert
    expect(spyOnImagemPerfilService).toHaveBeenCalled();
    expect(component.urlPerfilImage).toEqual('../../../../assets/perfil_static.png');
  }));


  it('should navigate to dashboard when menu is 1', () => {
    // Arrange
    spyOn(router, 'navigate');

    // Act
    component.selectMenu(1);

    // Assert
    expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
  });

  it('should navigate to categoria when menu is 2', () => {
    // Arrange
    spyOn(router, 'navigate');

    // Act
    component.selectMenu(2);

    // Assert
    expect(router.navigate).toHaveBeenCalledWith(['/categoria']);
  });

  it('should navigate to despesa when menu is 3', () => {
    // Arrange
    spyOn(router, 'navigate');

    // Act
    component.selectMenu(3);

    // Assert
    expect(router.navigate).toHaveBeenCalledWith(['/despesa']);
  });

  it('should navigate to receita when menu is 4', () => {
    // Arrange
    spyOn(router, 'navigate');

    // Act
    component.selectMenu(4);

    // Assert
    expect(router.navigate).toHaveBeenCalledWith(['/receita']);
  });

  it('should navigate to lancamento when menu is 5', () => {
    // Arrange
    spyOn(router, 'navigate');

    // Act
    component.selectMenu(5);

    // Assert
    expect(router.navigate).toHaveBeenCalledWith(['/lancamento']);
  });

  it('should navigate to perfil when menu is 6', () => {
    // Arrange
    spyOn(router, 'navigate');

    // Act
    component.selectMenu(6);

    // Assert
    expect(router.navigate).toHaveBeenCalledWith(['/perfil']);
  });

  it('should navigate to configuracoes when menu is 7', () => {
    // Arrange
    spyOn(router, 'navigate');

    // Act
    component.selectMenu(7);

    // Assert
    expect(router.navigate).toHaveBeenCalledWith(['/configuracoes']);
  });

  it('should navigate to Dashboard when an invalid menu is selected', () => {
    // Arrange
    spyOn(router, 'navigate');

    // Act
    component.selectMenu(8);

    // Assert
    expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
  });

  it('should navigate to Initial Page when button logout is clicked ', () => {
    // Arrange
    spyOn(router, 'navigate');

    // Act
    component.onLogoutClick();

    // Assert
    expect(router.navigate).toHaveBeenCalledWith(['/']);
  });
});
