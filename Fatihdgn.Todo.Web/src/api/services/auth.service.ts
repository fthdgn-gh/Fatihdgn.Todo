/* tslint:disable */
/* eslint-disable */
import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';

import { BaseService } from '../base-service';
import { ApiConfiguration } from '../api-configuration';
import { StrictHttpResponse } from '../strict-http-response';
import { RequestBuilder } from '../request-builder';

import { AuthLoginDto } from '../models/auth-login-dto';
import { AuthLoginResponseDto } from '../models/auth-login-response-dto';
import { AuthRefreshTokenDto } from '../models/auth-refresh-token-dto';
import { AuthRefreshTokenResponseDto } from '../models/auth-refresh-token-response-dto';
import { AuthRegisterDto } from '../models/auth-register-dto';

@Injectable({ providedIn: 'root' })
export class AuthService extends BaseService {
  constructor(config: ApiConfiguration, http: HttpClient) {
    super(config, http);
  }

  /** Path part for operation `authLogin()` */
  static readonly AuthLoginPath = '/api/v1/auth/login';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `authLogin()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  authLogin$Response(
    params: {
      body: AuthLoginDto
    },
    context?: HttpContext
  ): Observable<StrictHttpResponse<AuthLoginResponseDto>> {
    const rb = new RequestBuilder(this.rootUrl, AuthService.AuthLoginPath, 'post');
    if (params) {
      rb.body(params.body, 'application/json');
    }

    return this.http.request(
      rb.build({ responseType: 'json', accept: 'application/json', context })
    ).pipe(
      filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<AuthLoginResponseDto>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `authLogin$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  authLogin(
    params: {
      body: AuthLoginDto
    },
    context?: HttpContext
  ): Observable<AuthLoginResponseDto> {
    return this.authLogin$Response(params, context).pipe(
      map((r: StrictHttpResponse<AuthLoginResponseDto>): AuthLoginResponseDto => r.body)
    );
  }

  /** Path part for operation `authRegister()` */
  static readonly AuthRegisterPath = '/api/v1/auth/register';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `authRegister()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  authRegister$Response(
    params: {
      body: AuthRegisterDto
    },
    context?: HttpContext
  ): Observable<StrictHttpResponse<void>> {
    const rb = new RequestBuilder(this.rootUrl, AuthService.AuthRegisterPath, 'post');
    if (params) {
      rb.body(params.body, 'application/json');
    }

    return this.http.request(
      rb.build({ responseType: 'text', accept: '*/*', context })
    ).pipe(
      filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return (r as HttpResponse<any>).clone({ body: undefined }) as StrictHttpResponse<void>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `authRegister$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  authRegister(
    params: {
      body: AuthRegisterDto
    },
    context?: HttpContext
  ): Observable<void> {
    return this.authRegister$Response(params, context).pipe(
      map((r: StrictHttpResponse<void>): void => r.body)
    );
  }

  /** Path part for operation `authRefreshToken()` */
  static readonly AuthRefreshTokenPath = '/api/v1/auth/refresh';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `authRefreshToken()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  authRefreshToken$Response(
    params: {
      body: AuthRefreshTokenDto
    },
    context?: HttpContext
  ): Observable<StrictHttpResponse<AuthRefreshTokenResponseDto>> {
    const rb = new RequestBuilder(this.rootUrl, AuthService.AuthRefreshTokenPath, 'post');
    if (params) {
      rb.body(params.body, 'application/json');
    }

    return this.http.request(
      rb.build({ responseType: 'json', accept: 'application/json', context })
    ).pipe(
      filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<AuthRefreshTokenResponseDto>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `authRefreshToken$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  authRefreshToken(
    params: {
      body: AuthRefreshTokenDto
    },
    context?: HttpContext
  ): Observable<AuthRefreshTokenResponseDto> {
    return this.authRefreshToken$Response(params, context).pipe(
      map((r: StrictHttpResponse<AuthRefreshTokenResponseDto>): AuthRefreshTokenResponseDto => r.body)
    );
  }

}
