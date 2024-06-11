import { ComponentFixture, TestBed } from '@angular/core/testing';
import { BarChartComponent } from './bar-chart.component';
import { NO_ERRORS_SCHEMA } from '@angular/core';

describe('BarChartComponent', () => {
  let component: BarChartComponent;
  let fixture: ComponentFixture<BarChartComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BarChartComponent],
      schemas: [NO_ERRORS_SCHEMA],

    });
    fixture = TestBed.createComponent(BarChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have the correct chart type', () => {
    expect(component.barChartType).toBe('bar');
  });

  it('should log event and active when chartClicked is called', () => {
    const active = [{ index: 0, datasetIndex: 0, x: 0, y: 0 }];

    spyOn(console, 'log');
    component.chartClicked({ active });

    expect(console.log).toHaveBeenCalledWith(undefined, active);
  });

  it('should log event and active when chartHovered is called', () => {
    const active = [{ index: 1, datasetIndex: 0, x: 10, y: 20 }];

    spyOn(console, 'log');
    component.chartHovered({ active });

    expect(console.log).toHaveBeenCalledWith(undefined, active);
  });
});
