import { TestBed, inject } from '@angular/core/testing';
import { HttpTestingController, provideHttpClientTesting } from "@angular/common/http/testing";
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { CustomInterceptor } from '../../../../../interceptors/http.interceptor.service';
import { DashboardService } from "./dashboard.service";

describe('Unit Test DashboardService', () => {

  beforeEach(() => {
    TestBed.configureTestingModule({
    imports: [],
    providers: [DashboardService,
        { provide: HTTP_INTERCEPTORS, useClass: CustomInterceptor, multi: true, }, provideHttpClient(withInterceptorsFromDi()), provideHttpClientTesting()]
});
  });
  it('should be created', inject([DashboardService], (service: DashboardService) => {
    // Assert
    expect(service).toBeTruthy();
  }));
});
