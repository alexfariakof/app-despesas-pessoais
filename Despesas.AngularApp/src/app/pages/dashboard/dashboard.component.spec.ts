import { CommonModule } from '@angular/common';
import { ComponentFixture, TestBed, fakeAsync, flush } from '@angular/core/testing';
import { NgChartsModule } from 'ng2-charts';
import { AlertComponent, AlertType, BarChartComponent } from 'src/app/shared/components';
import { AuthService, MenuService } from 'src/app/shared/services';
import { SharedModule } from 'src/app/shared/shared.module';
import { DashboardComponent } from './dashboard.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { DashboardService } from 'src/app/shared/services/api';
import { from, throwError } from 'rxjs';
import * as dayjs from 'dayjs';

describe('Unit Test DashboardComponent', () => {
  let component: DashboardComponent;
  let fixture: ComponentFixture<DashboardComponent>;
  let mockAuthService: jasmine.SpyObj<AuthService>;
  let mockDashboardService: DashboardService;

  let mockLabels: string[] = ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'];
  let mockDatasets: any[] = [
    { label: 'Despesas', data: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12] },
    { label: 'Receitas', data: [12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0] }
  ]

  beforeEach(() => {
    mockAuthService = jasmine.createSpyObj('AuthService', ['createAccessToken', 'isAuthenticated']);
    mockAuthService.createAccessToken.and.returnValue(true);
    mockAuthService.isAuthenticated.and.returnValue(true);

    TestBed.configureTestingModule({
      declarations: [DashboardComponent, BarChartComponent],
      imports: [CommonModule, SharedModule, NgChartsModule, HttpClientTestingModule],
      providers: [MenuService, AlertComponent, NgbActiveModal,
        { provide: AuthService, useValue: mockAuthService },
      ]
    });
    fixture = TestBed.createComponent(DashboardComponent);
    component = fixture.componentInstance;
    component.barChartLabels = mockLabels;
    component.barChartDatasets = mockDatasets;
    mockDashboardService = TestBed.inject(DashboardService);
    fixture.detectChanges();
  });

  it('should create', () => {
    // Assert
    expect(component).toBeTruthy();
  });

  it('should initializeChart', fakeAsync(() => {
    // Arrange
    let mockChartData = { labels: mockLabels, datasets: mockDatasets };
    const spyOnGetDataGraphicByYear = spyOn(mockDashboardService, 'getDataGraphicByYear').and.returnValue(from(Promise.resolve(mockChartData)));
    spyOn(component, 'updateChart').and.callThrough();

    // Act
    component.initializeChart();
    flush();

    // Assert
    expect(spyOnGetDataGraphicByYear).toHaveBeenCalled();
    expect(spyOnGetDataGraphicByYear).toHaveBeenCalledWith(dayjs(dayjs().format('YYYY-01-01')));
    expect(component.barChartDatasets.length).toBeGreaterThan(1);
    expect(component.barChartLabels.length).toBeGreaterThan(11);
  }));

  it('should throw error when try to initializeChart', () => {
    // Arrange
    const errorMessage = 'Fake Error Message Initialize Chart';
    const spyOnGetDataGraphicByYear = spyOn(mockDashboardService, 'getDataGraphicByYear').and.returnValue(throwError(errorMessage));
    const alertOpenSpy = spyOn(TestBed.inject(AlertComponent), 'open');

    // Act
    component.initializeChart();

    // Assert
    expect(spyOnGetDataGraphicByYear).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalledWith(AlertComponent, errorMessage, AlertType.Warning);
  });

  it('should updateChart', fakeAsync(() => {
    // Arrange
    let mockFilterAno = dayjs().format('YYYY');
    let mockChartData = { labels: mockLabels, datasets: mockDatasets };
    const spyOnGetDataGraphicByYear = spyOn(mockDashboardService, 'getDataGraphicByYear').and.returnValue(from(Promise.resolve(mockChartData)));
    spyOn(component, 'updateChart').and.callThrough();

    // Act
    component.barraFerramenta.filterAnoService.dataAno = mockFilterAno;
    component.updateChart();
    flush();

    // Assert
    expect(spyOnGetDataGraphicByYear).toHaveBeenCalled();
    expect(spyOnGetDataGraphicByYear).toHaveBeenCalledWith(dayjs(mockFilterAno));
    expect(component.barChartDatasets.length).toBeGreaterThan(1);
    expect(component.barChartLabels.length).toBeGreaterThan(11);
  }));

  it('should throw error when try to updateChart', () => {
    // Arrange
    const errorMessage = 'Fake Error Message Update Chart';
    const spyOnGetDataGraphicByYear = spyOn(mockDashboardService, 'getDataGraphicByYear').and.returnValue(throwError(errorMessage));
    const alertOpenSpy = spyOn(TestBed.inject(AlertComponent), 'open');

    // Act
    component.updateChart();

    // Assert
    expect(spyOnGetDataGraphicByYear).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalled();
    expect(alertOpenSpy).toHaveBeenCalledWith(AlertComponent, errorMessage, AlertType.Warning);
  });
});
