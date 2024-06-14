import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NgbModalConfig, NgbModal, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalConfirmComponent } from './modal.confirm.component';

describe('Unit Test ModalConfirmComponent', () => {
  let component: ModalConfirmComponent;
  let fixture: ComponentFixture<ModalConfirmComponent>;
  let modalService: NgbModal;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ModalConfirmComponent],
      providers: [NgbModalConfig, NgbModal, NgbActiveModal],
    });

    fixture = TestBed.createComponent(ModalConfirmComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
    modalService = TestBed.inject(NgbModal);
  });

  it('should create', () => {
    // Assert
    expect(component).toBeTruthy();
  });

  it('should open modal', () => {
    // Arrange
    const content = ModalConfirmComponent;
    spyOn(modalService, 'open');

    // Act
    component.modalService.open(content);

    // Assert
    expect(modalService.open).toHaveBeenCalled();
    expect(modalService.open).toHaveBeenCalledWith(content);

  });

  it('should close modal', () => {
    // Arrange
    const content = ModalConfirmComponent;
    spyOn(modalService, 'open').and.returnValue(modalService.open(content));
    spyOn(modalService, 'dismissAll').and.returnValue(modalService.dismissAll());

    // Act
    component.close();

    // Assert
    expect(modalService.dismissAll).toHaveBeenCalled();
  });

  it('should setConfirmButton', () => {
    // Arrange
    const content = ModalConfirmComponent;
    spyOn(modalService, 'open').and.returnValue(modalService.open(content));
    const spySetConfirmButton = spyOn(component, 'setConfirmButton').and.callThrough();

    // Act
    component.setConfirmButton(() => console.log('Fake ModalConfirm Function Confirm Button '))
    component.onClickConfirm();

    // Assert
    expect(spySetConfirmButton).toHaveBeenCalled();
  });


});
