import { TestBed } from '@angular/core/testing';
import { MenuService } from './menu.service';
import { Router } from "@angular/router";
import { AppRoutingModule } from 'src/app/app.routing.module';

describe('MenuService', () => {
  let menuService: jasmine.SpyObj<MenuService>;
  let router: Router;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ AppRoutingModule],
      providers: [MenuService]
    });
    router = TestBed.inject(Router);
    menuService = TestBed.inject(MenuService) as jasmine.SpyObj<MenuService>;
  });

  it('should be created', () => {
    expect(menuService).toBeTruthy();
  });

  it('should navigate to dashboard for menu 0', () => {
    spyOn(router, 'navigate');
    menuService.selectMenu(0, router);
    expect(router.navigate).toHaveBeenCalledWith(['/login']);
    expect(menuService.menuSelecionado).toBe(0);
  });

  it('should navigate to dashboard for menu 1', () => {
    spyOn(router, 'navigate');
    menuService.selectMenu(1, router);
    expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
    expect(menuService.menuSelecionado).toBe(1);
  });

  it('should navigate to categoria for menu 2', () => {
    spyOn(router, 'navigate');
    menuService.selectMenu(2, router);
    expect(router.navigate).toHaveBeenCalledWith(['/categoria']);
    expect(menuService.menuSelecionado).toBe(2);
  });

  it('should navigate to despesa for menu 3', () => {
    spyOn(router, 'navigate');
    menuService.selectMenu(3, router);
    expect(router.navigate).toHaveBeenCalledWith(['/despesa']);
    expect(menuService.menuSelecionado).toBe(3);
  });

  it('should navigate to receita for menu 4', () => {
    spyOn(router, 'navigate');
    menuService.selectMenu(4, router);
    expect(router.navigate).toHaveBeenCalledWith(['/receita']);
    expect(menuService.menuSelecionado).toBe(4);
  });

  it('should navigate to lancamento for menu 5', () => {
    spyOn(router, 'navigate');
    menuService.selectMenu(5, router);
    expect(router.navigate).toHaveBeenCalledWith(['/lancamento']);
    expect(menuService.menuSelecionado).toBe(5);
  });

  it('should navigate to perfil for menu 6', () => {
    spyOn(router, 'navigate');
    menuService.selectMenu(6, router);
    expect(router.navigate).toHaveBeenCalledWith(['/perfil']);
    expect(menuService.menuSelecionado).toBe(6);
  });

  it('should navigate to configuracoes for menu 7', () => {
    spyOn(router, 'navigate');
    menuService.selectMenu(7, router);
    expect(router.navigate).toHaveBeenCalledWith(['/configuracoes']);
    expect(menuService.menuSelecionado).toBe(7);
  });

  it('should navigate to Dashboard when an invalid menu is selected', () => {
    spyOn(router, 'navigate');
    menuService.selectMenu(8, router);
    expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
  });
});
