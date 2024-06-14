import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { BehaviorSubject, Observable, catchError, filter, finalize, switchMap, take, throwError } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { LoadingComponent } from '../app/shared/components';
import { AuthService, TokenStorageService } from '../app/shared/services';
import { IAuth } from '../app/shared/models';

@Injectable({
  providedIn: 'root'
})

export class CustomInterceptor implements HttpInterceptor {
  private isRefreshing = false;
  private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  constructor(private tokenService: TokenStorageService, private authService: AuthService, private modalService: NgbModal) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    const modalRef = this.modalService.open(LoadingComponent, { centered: true, fullscreen: true, windowClass: 'loading-modal' });
    const modifiedRequest = this.addTokenHeader(request);

    return next.handle(modifiedRequest).pipe(
      catchError((error: HttpErrorResponse) => {
        modalRef.close();

        if (error.ok === false && error.status === 0)
          return throwError(() => 'Erro de conexão tente mais tarde.');
        else if (error.status === 400) {
          return throwError(() => error.error );
        }
        else if (error.status === 401) {
          return this.handle401Error(request, next);
        }
        else if (error.status === 403) {
          return throwError(() => 'Acesso não Autorizado!');
        }
        console.log(error);
        return throwError(() => 'Erro tente atualizar a página ou realize novamente o login..');
      }),
      finalize(() => {
        modalRef.close();
      }));
  }

  private handle401Error(request: HttpRequest<any>, next: HttpHandler) {
    if (!this.isRefreshing) {
      this.isRefreshing = true;
      this.refreshTokenSubject.next(null);

      const auth = this.tokenService.getRefreshToken();
      if (auth)
        return this.authService.refreshToken(auth).pipe(
          switchMap((auth: IAuth) => {
            this.isRefreshing = false;
            this.tokenService.saveUser(auth);
            this.tokenService.saveToken(auth.accessToken);
            this.tokenService.saveRefreshToken(auth.refreshToken);
            this.refreshTokenSubject.next(auth.refreshToken);
            return next.handle(this.addTokenHeader(request));
          }),
          catchError((err) => {
            this.isRefreshing = false;
            sessionStorage.clear();
            this.tokenService.signOut();
            return throwError(() => 'Erro de autenticação, tente atualizar a página ou realize novamente o login.');

          })
        );
    }

    return this.refreshTokenSubject.pipe(
      filter(token => token !== null),
      take(1),
      switchMap((token) => next.handle(this.addTokenHeader(request)))
    );
  }

  private addTokenHeader(request: HttpRequest<any>) {
    return request.clone({
      setHeaders: {
        Authorization: `Bearer ${ this.tokenService.getToken() }`
      }
    });
  }
}
