import { Router } from "@angular/router";

export class MenuService {
    constructor() { }

    menuSelecionado: number = 0;
    menu: string = '';

    selectMenu(menu: number, router : Router) {
      switch (menu) {
          case 0:
            router.navigate(['/login']);
            break;

          case 1:
              router.navigate(['/dashboard']);
              this.menu = "Home";
              break;

          case 2:
              router.navigate(['/categoria']);
              this.menu = "Categorias";
              break;

          case 3:
              router.navigate(['/despesa']);
              this.menu = "Despesas";
              break;

          case 4:
              router.navigate(['/receita']);
              this.menu = "Receitas";
              break;

          case 5:
              router.navigate(['/lancamento']);
              this.menu = "Lançamentos";
              break;

          case 6:
              router.navigate(['/perfil']);
              this.menu = "Perfil";
              break;

          case 7:
              router.navigate(['/configuracoes']);
              this.menu = "Configurações";
              break;

          default:
              router.navigate(['/dashboard']);
              break;
      }

      this.menuSelecionado = menu;
  }


  setMenuSelecionado(menu: number) {
    switch (menu) {
        case 0:
          break;

        case 1:
            this.menu = "Home";
            break;

        case 2:
            this.menu = "Categorias";
            break;

        case 3:
            this.menu = "Despesas";
            break;

        case 4:
            this.menu = "Receitas";
            break;

        case 5:
            this.menu = "Lançamentos";
            break;

        case 6:
            this.menu = "Perfil";
            break;

        case 7:
            this.menu = "Configurações";
            break;

        default:
            break;
    }

    this.menuSelecionado = menu;
}


}
