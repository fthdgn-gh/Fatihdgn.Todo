/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { BaseService as __BaseService } from '../base-service';
import { ApiConfiguration as __Configuration } from '../api-configuration';
import { StrictHttpResponse as __StrictHttpResponse } from '../strict-http-response';
import { Observable as __Observable } from 'rxjs';
import { map as __map, filter as __filter } from 'rxjs/operators';

import { AuthLoginResponseDTO } from '../models/auth-login-response-dto';
import { AuthLoginDTO } from '../models/auth-login-dto';
import { AuthRegisterDTO } from '../models/auth-register-dto';
import { AuthRefreshTokenResponseDTO } from '../models/auth-refresh-token-response-dto';
import { AuthRefreshTokenDTO } from '../models/auth-refresh-token-dto';
@Injectable({
  providedIn: 'root',
})
class AuthService extends __BaseService {
  static readonly AuthLoginPath = '/login';
  static readonly AuthRegisterPath = '/register';
  static readonly AuthRefreshTokenPath = '/refresh';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * @param model undefined
   */
  AuthLoginResponse(model: AuthLoginDTO): __Observable<__StrictHttpResponse<AuthLoginResponseDTO>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = model;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/login`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<AuthLoginResponseDTO>;
      })
    );
  }
  /**
   * @param model undefined
   */
  AuthLogin(model: AuthLoginDTO): __Observable<AuthLoginResponseDTO> {
    return this.AuthLoginResponse(model).pipe(
      __map(_r => _r.body as AuthLoginResponseDTO)
    );
  }

  /**
   * @param model undefined
   */
  AuthRegisterResponse(model: AuthRegisterDTO): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = model;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/register`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<null>;
      })
    );
  }
  /**
   * @param model undefined
   */
  AuthRegister(model: AuthRegisterDTO): __Observable<null> {
    return this.AuthRegisterResponse(model).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * @param model undefined
   */
  AuthRefreshTokenResponse(model: AuthRefreshTokenDTO): __Observable<__StrictHttpResponse<AuthRefreshTokenResponseDTO>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = model;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/refresh`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<AuthRefreshTokenResponseDTO>;
      })
    );
  }
  /**
   * @param model undefined
   */
  AuthRefreshToken(model: AuthRefreshTokenDTO): __Observable<AuthRefreshTokenResponseDTO> {
    return this.AuthRefreshTokenResponse(model).pipe(
      __map(_r => _r.body as AuthRefreshTokenResponseDTO)
    );
  }
}

module AuthService {
}

export { AuthService }
