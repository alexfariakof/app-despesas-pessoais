import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NgbModalConfig, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalFormComponent } from './modal.form.component';

describe('ModalFormComponent', () => {
  let component: ModalFormComponent;
  let fixture: ComponentFixture<ModalFormComponent>;
  let modalService: NgbModal;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ModalFormComponent],
      providers: [NgbModalConfig, NgbModal ],
    });

    fixture = TestBed.createComponent(ModalFormComponent);
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
    const content = ['<div></div>'];
    spyOn(component.modalService, 'open');

    // Act
    component.open(content);

    // Assert
    expect(component.modalService.open).toHaveBeenCalled();
    expect(component.modalService.open).toHaveBeenCalledWith(content, { centered: true });
  });

  it('should close modal', () => {
    // Arrange
    spyOn(component.modalService, 'dismissAll');

    // Act
    component.close();

    // Assert
    expect(component.modalService.dismissAll).toHaveBeenCalled();
  });
});
