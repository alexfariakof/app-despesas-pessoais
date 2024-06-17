import { TestBed } from '@angular/core/testing';

export class MockSessionStorage {
  private storage: { [key: string]: string } = {};

  constructor() {
    const getItemSpy = jasmine.createSpy('getItem').and.callFake((key: string) => this.storage[key] || null);
    const setItemSpy = jasmine.createSpy('setItem').and.callFake((key: string, value: string) => this.storage[key] = value);
    const removeItemSpy = jasmine.createSpy('removeItem').and.callFake((key: string) => delete this.storage[key]);
    const clearSpy = jasmine.createSpy('clear').and.callFake(() => this.storage = {});

    spyOn(sessionStorage, 'getItem').and.callFake(getItemSpy);
    spyOn(sessionStorage, 'setItem').and.callFake(setItemSpy);
    spyOn(sessionStorage, 'removeItem').and.callFake(removeItemSpy);
    spyOn(sessionStorage, 'clear').and.callFake(clearSpy);
    this.setup();
  }

  private setup() {
    TestBed.configureTestingModule({
      providers: [
        { provide: Storage, useValue: sessionStorage }
      ]
    });
  }

  cleanup() {
    Object.keys(this.storage).forEach(key => delete this.storage[key]);
  }

  instance = (): jasmine.SpyObj<Storage> =>  {
    return sessionStorage as jasmine.SpyObj<Storage>;
  }

  setItem = (key: any, value: any): void => {
    (sessionStorage as jasmine.SpyObj<Storage>).setItem(key, value);
  }
}
