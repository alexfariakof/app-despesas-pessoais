import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { LoginComponent } from './pages/login/login.component';
import { CommonModule } from '@angular/common';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app.routing.module';
import { PrimeiroAcessoComponent } from './pages/primeiro-acesso/primeiro-acesso.component';
import { AuthService } from './shared/services/auth/auth.service';
import { Router } from '@angular/router';
import { AuthProvider } from './shared/provider/auth.provider';
import { MatDatepickerIntl } from '@angular/material/datepicker';
import { DateAdapter } from '@angular/material/core';

describe('AppComponent', () => {
  let app: AppComponent;
  let fixture: ComponentFixture<AppComponent>;
  let mockRouter: jasmine.SpyObj<Router>;
  let mockAuthService: jasmine.SpyObj<AuthService>;
  let router: Router;

  beforeEach(() => {
    mockRouter = jasmine.createSpyObj('Router', ['navigate']);
    mockAuthService = jasmine.createSpyObj('AuthService', [ 'isAuthenticated']);
    TestBed.configureTestingModule({
    declarations: [AppComponent, LoginComponent],
    imports: [BrowserModule, AppRoutingModule, CommonModule, ReactiveFormsModule, FormsModule, PrimeiroAcessoComponent],
    providers: [AuthService, AuthProvider,
        { provide: Router, useValue: mockRouter },
        { provide: AuthService, useValue: mockAuthService },
        DateAdapter,
        MatDatepickerIntl, provideHttpClient(withInterceptorsFromDi())]
});
    fixture = TestBed.createComponent(AppComponent);
    app = fixture.componentInstance;
    router = TestBed.inject(Router);
  });

  it('should create the app', () => {
    expect(app).toBeTruthy();
  });

  it('should navigate to routes ', () => {
    mockAuthService.isAuthenticated.and.returnValue(true);
    router.navigate(['/dashboard']);
    expect(mockRouter.navigate).toHaveBeenCalledWith(['/dashboard']);
  });
});
