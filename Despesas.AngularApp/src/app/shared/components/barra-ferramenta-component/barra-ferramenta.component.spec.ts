import { ComponentFixture, TestBed } from '@angular/core/testing';
import { BarraFerramentaComponent } from './barra-ferramenta.component';
import { FormsModule } from '@angular/forms';
import { SaldoComponent } from '../saldo/saldo.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('BarraFerramentaComponent', () => {
  let component: BarraFerramentaComponent;
  let fixture: ComponentFixture<BarraFerramentaComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BarraFerramentaComponent],
      imports: [FormsModule, SaldoComponent, HttpClientTestingModule]
    });
    fixture = TestBed.createComponent(BarraFerramentaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    // Assert
    expect(component).toBeTruthy();
  });

  it('should call onClickNovo when clickBtnNovo is called', () => {
    // Arrange
    const onClickNovoSpy = jasmine.createSpy('onClickNovo');
    component.onClickNovo = onClickNovoSpy;

    // Act
    component.clickBtnNovo();

    // Assert
    expect(onClickNovoSpy).toHaveBeenCalled();
  });

  it('should call window.history.back when clickBtnVoltar is called', () => {
    // Arrange
    const spy = spyOn(window.history, 'back');

    // Act
    component.clickBtnVoltar();

    // Assert
    expect(spy).toHaveBeenCalled();
  });
});
