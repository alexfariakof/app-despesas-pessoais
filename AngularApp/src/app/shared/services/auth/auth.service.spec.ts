import { TestBed, inject } from '@angular/core/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { IAuth } from '../../models/IAuth';
import { AuthService } from './auth.service';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('Unit Test AuthService', () => {
  let authService: AuthService;

  beforeEach(() => {
    TestBed.configureTestingModule({
    imports: [],
    providers: [AuthService, provideHttpClient(withInterceptorsFromDi()), provideHttpClientTesting()]
});
    authService = TestBed.inject(AuthService);
  });

  it('should be created', inject([AuthService], (service: AuthService) => {
    // Assert
    expect(service).toBeTruthy();
  }));

  it('should set and get access token', () => {
    // Arrange
    const fakeAuth: IAuth = {
      accessToken: 'fakeToken',
      refreshToken: 'fakeRefreshToken',
      expiration: '2023-01-01T00:00:00Z',
      authenticated: true,
      created: '2023-01-01T00:00:00Z',
      message: 'OK'
    };

    // Act
    authService.createAccessToken(fakeAuth);

    // Assert
    expect(authService.isAuthenticated()).toBe(true);
  });

  it('should clear local storage', () => {
    // Act
    authService.clearSessionStorage();

    // Assert
    expect(authService.isAuthenticated()).toBeFalsy();
  });

  it('should catch error on creating access token', () => {
    // Arrange
    spyOn(sessionStorage, 'setItem').and.throwError('Fake error');

    const fakeAuth: IAuth = {
      accessToken: undefined,
      refreshToken: undefined,
      expiration: '2023-01-01T00:00:00Z',
      authenticated: false,
      created: '',
      message: ''
    };

    // Act
    const result = authService.createAccessToken(fakeAuth);

    // Assert
    expect(result).toBeFalsy();
  });
});

