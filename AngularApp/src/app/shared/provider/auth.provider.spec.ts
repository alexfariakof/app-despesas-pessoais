import { TestBed, inject } from '@angular/core/testing';
import { AuthProvider } from './auth.provider';
import { AuthService } from '../services/auth/auth.service';
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot } from '@angular/router';

describe('Unit Test Auth Provider', () => {
  let authProvider: AuthProvider;
  let mockAuthService: jasmine.SpyObj<AuthService>;
  let mockRouter: Router;

  beforeEach(() => {
    mockAuthService = jasmine.createSpyObj('AuthService', ['isAuthenticated']);
    TestBed.configureTestingModule({
      imports: [],
      providers:[
        { provide: AuthService, useValue: mockAuthService }
      ]
    });
    authProvider = TestBed.inject(AuthProvider);
    mockRouter = TestBed.inject(Router);

  });

  it('should be created', inject([AuthService, Router], (provider: AuthService) => {
    // Assert
    expect(provider).toBeTruthy();
  }));

  it('should allow activation when user is authenticated', () => {
    // Arrange and Act
    const spyAuthService = mockAuthService.isAuthenticated.and.returnValue(true);
    const canActivate = authProvider.canActivate({} as ActivatedRouteSnapshot, {} as RouterStateSnapshot);

    // Assert
    expect(spyAuthService).toHaveBeenCalled();
    expect(canActivate).toBeTruthy();
  });

  it('should redirect to login page when user is not authenticated', inject([Router], (router: Router) => {
    // Arrange and Act
    const spyAuthService = mockAuthService.isAuthenticated.and.returnValue(false);
    spyOn(router, 'navigate');
    const canActivate = authProvider.canActivate({} as ActivatedRouteSnapshot, {} as RouterStateSnapshot);

    // Assert
    expect(spyAuthService).toHaveBeenCalled();
    expect(canActivate).toBeFalsy();
    expect(router.navigate).toHaveBeenCalledWith(['/login']);
  }));

});
