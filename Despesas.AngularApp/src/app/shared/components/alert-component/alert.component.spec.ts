import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import { AlertComponent, AlertType } from './alert.component';
import { AlertModule } from './alert.component.module';
import { MockAlertComponent } from '__mock__/mock.alert.component';

describe('Test AlertComponent', () => {
  let component: AlertComponent;
  let fixture: ComponentFixture<AlertComponent>;
  let modalService: NgbModal;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AlertComponent, MockAlertComponent],
      imports: [ AlertModule ],
      providers: [NgbModalConfig,  NgbModal],
    });

    fixture = TestBed.createComponent(AlertComponent);
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
    const content =  MockAlertComponent;
    spyOn(modalService, 'open');

    // Act
    component.modalService.open(content);

    // Assert
    expect(modalService.open).toHaveBeenCalled();
    expect(modalService.open).toHaveBeenCalledWith(content);
  });

  it('should close modal', () => {
    // Arrange
    const content = MockAlertComponent;
    spyOn(modalService, 'open').and.returnValue(modalService.open(content));
    spyOn(modalService, 'dismissAll').and.returnValue(modalService.dismissAll());

    // Act
    component.close();

    // Assert
    expect(modalService.dismissAll).toHaveBeenCalled();
  });

  it('should open SuccessAlertComponent and message been seted', () => {
    // Arrange
    const modalRefMock = jasmine.createSpyObj('NgbModalRef', ['componentInstance']);
    const expectedMessage = 'Teste Mensaagem';
    const openSpy = spyOn(component, 'open').and.returnValue(modalRefMock);

    // Act
    modalRefMock.componentInstance.message = expectedMessage;
    component.open(MockAlertComponent, expectedMessage, AlertType.Warning);

    // Assert
    expect(openSpy).toHaveBeenCalledWith(MockAlertComponent, expectedMessage, AlertType.Warning);
    expect(modalRefMock.componentInstance.message).toBe(expectedMessage);
  });

  it('should open and close', () => {
    // Arrange
    const modalRefMock = jasmine.createSpyObj('NgbModalRef', ['componentInstance']);
    const openSpy = spyOn(component, 'open').and.returnValue(modalRefMock);
    const closeSpy = spyOn(component, 'close').and.returnValue(modalRefMock);

    // Act
    component.open(MockAlertComponent, "Test Alert Component", AlertType.Success);
    component.close();

    // Assert
    expect(openSpy).toHaveBeenCalledWith(MockAlertComponent, "Test Alert Component", AlertType.Success);
    expect(closeSpy).toHaveBeenCalledWith();
  });
});
