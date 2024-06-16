import { TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from "@angular/common/http/testing";
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { CustomInterceptor } from '../../../../../interceptors/http.interceptor.service';
import { DashboardService } from "./dashboard.service";

describe('Unit Test DashboardService', () => {

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers:[DashboardService,
        { provide: HTTP_INTERCEPTORS, useClass: CustomInterceptor, multi: true, }
      ]
    });
  });
  it('should be created', inject([DashboardService], (service: DashboardService) => {
    // Assert
    expect(service).toBeTruthy();
  }));
});
