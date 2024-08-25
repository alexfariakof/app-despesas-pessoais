import { TestBed } from '@angular/core/testing';

export class MockLocalStorage {
  private storage: { [key: string]: string } = {};

  constructor() {
    const getItemSpy = jasmine.createSpy('getItem').and.callFake((key: string) => this.storage[key] || null);
    const setItemSpy = jasmine.createSpy('setItem').and.callFake((key: string, value: string) => this.storage[key] = value);
    const removeItemSpy = jasmine.createSpy('removeItem').and.callFake((key: string) => delete this.storage[key]);
    const clearSpy = jasmine.createSpy('clear').and.callFake(() => this.storage = {});

    spyOn(localStorage, 'getItem').and.callFake(getItemSpy);
    spyOn(localStorage, 'setItem').and.callFake(setItemSpy);
    spyOn(localStorage, 'removeItem').and.callFake(removeItemSpy);
    spyOn(localStorage, 'clear').and.callFake(clearSpy);
    this.setup();
  }

  private setup() {
    TestBed.configureTestingModule({
      providers: [
        { provide: Storage, useValue: localStorage }
      ]
    });
  }

  cleanup() {
    Object.keys(this.storage).forEach(key => delete this.storage[key]);
  }

  instance = (): jasmine.SpyObj<Storage> =>  {
    return localStorage as jasmine.SpyObj<Storage>;
  }

  setItem = (key: any, value: any): void => {
    (localStorage as jasmine.SpyObj<Storage>).setItem(key, value);
  }
}
