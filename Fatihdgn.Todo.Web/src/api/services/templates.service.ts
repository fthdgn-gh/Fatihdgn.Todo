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

import { TodoTemplateCreateDto } from '../models/todo-template-create-dto';
import { TodoTemplateDto } from '../models/todo-template-dto';
import { TodoTemplatePatchDto } from '../models/todo-template-patch-dto';
import { TodoTemplateUpdateDto } from '../models/todo-template-update-dto';

@Injectable({ providedIn: 'root' })
export class TemplatesService extends BaseService {
  constructor(config: ApiConfiguration, http: HttpClient) {
    super(config, http);
  }

  /** Path part for operation `getAllTemplates()` */
  static readonly GetAllTemplatesPath = '/api/v1/templates';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `getAllTemplates()` instead.
   *
   * This method doesn't expect any request body.
   */
  getAllTemplates$Response(
    params?: {
    },
    context?: HttpContext
  ): Observable<StrictHttpResponse<Array<TodoTemplateDto>>> {
    const rb = new RequestBuilder(this.rootUrl, TemplatesService.GetAllTemplatesPath, 'get');
    if (params) {
    }

    return this.http.request(
      rb.build({ responseType: 'json', accept: 'application/json', context })
    ).pipe(
      filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Array<TodoTemplateDto>>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `getAllTemplates$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  getAllTemplates(
    params?: {
    },
    context?: HttpContext
  ): Observable<Array<TodoTemplateDto>> {
    return this.getAllTemplates$Response(params, context).pipe(
      map((r: StrictHttpResponse<Array<TodoTemplateDto>>): Array<TodoTemplateDto> => r.body)
    );
  }

  /** Path part for operation `createTemplate()` */
  static readonly CreateTemplatePath = '/api/v1/templates';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `createTemplate()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  createTemplate$Response(
    params: {
      body: TodoTemplateCreateDto
    },
    context?: HttpContext
  ): Observable<StrictHttpResponse<TodoTemplateDto>> {
    const rb = new RequestBuilder(this.rootUrl, TemplatesService.CreateTemplatePath, 'post');
    if (params) {
      rb.body(params.body, 'application/json');
    }

    return this.http.request(
      rb.build({ responseType: 'json', accept: 'application/json', context })
    ).pipe(
      filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<TodoTemplateDto>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `createTemplate$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  createTemplate(
    params: {
      body: TodoTemplateCreateDto
    },
    context?: HttpContext
  ): Observable<TodoTemplateDto> {
    return this.createTemplate$Response(params, context).pipe(
      map((r: StrictHttpResponse<TodoTemplateDto>): TodoTemplateDto => r.body)
    );
  }

  /** Path part for operation `getTemplate()` */
  static readonly GetTemplatePath = '/api/v1/templates/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `getTemplate()` instead.
   *
   * This method doesn't expect any request body.
   */
  getTemplate$Response(
    params: {
      id: string;
    },
    context?: HttpContext
  ): Observable<StrictHttpResponse<TodoTemplateDto>> {
    const rb = new RequestBuilder(this.rootUrl, TemplatesService.GetTemplatePath, 'get');
    if (params) {
      rb.path('id', params.id, {});
    }

    return this.http.request(
      rb.build({ responseType: 'json', accept: 'application/json', context })
    ).pipe(
      filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<TodoTemplateDto>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `getTemplate$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  getTemplate(
    params: {
      id: string;
    },
    context?: HttpContext
  ): Observable<TodoTemplateDto> {
    return this.getTemplate$Response(params, context).pipe(
      map((r: StrictHttpResponse<TodoTemplateDto>): TodoTemplateDto => r.body)
    );
  }

  /** Path part for operation `updateTemplate()` */
  static readonly UpdateTemplatePath = '/api/v1/templates/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `updateTemplate()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  updateTemplate$Response(
    params: {
      id: string;
      body: TodoTemplateUpdateDto
    },
    context?: HttpContext
  ): Observable<StrictHttpResponse<TodoTemplateDto>> {
    const rb = new RequestBuilder(this.rootUrl, TemplatesService.UpdateTemplatePath, 'put');
    if (params) {
      rb.path('id', params.id, {});
      rb.body(params.body, 'application/json');
    }

    return this.http.request(
      rb.build({ responseType: 'json', accept: 'application/json', context })
    ).pipe(
      filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<TodoTemplateDto>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `updateTemplate$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  updateTemplate(
    params: {
      id: string;
      body: TodoTemplateUpdateDto
    },
    context?: HttpContext
  ): Observable<TodoTemplateDto> {
    return this.updateTemplate$Response(params, context).pipe(
      map((r: StrictHttpResponse<TodoTemplateDto>): TodoTemplateDto => r.body)
    );
  }

  /** Path part for operation `removeTemplate()` */
  static readonly RemoveTemplatePath = '/api/v1/templates/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `removeTemplate()` instead.
   *
   * This method doesn't expect any request body.
   */
  removeTemplate$Response(
    params: {
      id: string;
    },
    context?: HttpContext
  ): Observable<StrictHttpResponse<Blob>> {
    const rb = new RequestBuilder(this.rootUrl, TemplatesService.RemoveTemplatePath, 'delete');
    if (params) {
      rb.path('id', params.id, {});
    }

    return this.http.request(
      rb.build({ responseType: 'blob', accept: 'application/octet-stream', context })
    ).pipe(
      filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Blob>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `removeTemplate$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  removeTemplate(
    params: {
      id: string;
    },
    context?: HttpContext
  ): Observable<Blob> {
    return this.removeTemplate$Response(params, context).pipe(
      map((r: StrictHttpResponse<Blob>): Blob => r.body)
    );
  }

  /** Path part for operation `patchTemplate()` */
  static readonly PatchTemplatePath = '/api/v1/templates/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `patchTemplate()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  patchTemplate$Response(
    params: {
      id: string;
      body: TodoTemplatePatchDto
    },
    context?: HttpContext
  ): Observable<StrictHttpResponse<TodoTemplateDto>> {
    const rb = new RequestBuilder(this.rootUrl, TemplatesService.PatchTemplatePath, 'patch');
    if (params) {
      rb.path('id', params.id, {});
      rb.body(params.body, 'application/json');
    }

    return this.http.request(
      rb.build({ responseType: 'json', accept: 'application/json', context })
    ).pipe(
      filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<TodoTemplateDto>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `patchTemplate$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  patchTemplate(
    params: {
      id: string;
      body: TodoTemplatePatchDto
    },
    context?: HttpContext
  ): Observable<TodoTemplateDto> {
    return this.patchTemplate$Response(params, context).pipe(
      map((r: StrictHttpResponse<TodoTemplateDto>): TodoTemplateDto => r.body)
    );
  }

  /** Path part for operation `createTemplateByList()` */
  static readonly CreateTemplateByListPath = '/api/v1/templates/generate/lists/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `createTemplateByList()` instead.
   *
   * This method doesn't expect any request body.
   */
  createTemplateByList$Response(
    params: {
      id: string;
    },
    context?: HttpContext
  ): Observable<StrictHttpResponse<TodoTemplateDto>> {
    const rb = new RequestBuilder(this.rootUrl, TemplatesService.CreateTemplateByListPath, 'post');
    if (params) {
      rb.path('id', params.id, {});
    }

    return this.http.request(
      rb.build({ responseType: 'json', accept: 'application/json', context })
    ).pipe(
      filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<TodoTemplateDto>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `createTemplateByList$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  createTemplateByList(
    params: {
      id: string;
    },
    context?: HttpContext
  ): Observable<TodoTemplateDto> {
    return this.createTemplateByList$Response(params, context).pipe(
      map((r: StrictHttpResponse<TodoTemplateDto>): TodoTemplateDto => r.body)
    );
  }

}
