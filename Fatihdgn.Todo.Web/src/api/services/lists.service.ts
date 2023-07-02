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

import { TodoListCreateDto } from '../models/todo-list-create-dto';
import { TodoListDto } from '../models/todo-list-dto';
import { TodoListPatchDto } from '../models/todo-list-patch-dto';
import { TodoListUpdateDto } from '../models/todo-list-update-dto';

@Injectable({ providedIn: 'root' })
export class ListsService extends BaseService {
  constructor(config: ApiConfiguration, http: HttpClient) {
    super(config, http);
  }

  /** Path part for operation `getAllLists()` */
  static readonly GetAllListsPath = '/api/v1/lists';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `getAllLists()` instead.
   *
   * This method doesn't expect any request body.
   */
  getAllLists$Response(
    params?: {
    },
    context?: HttpContext
  ): Observable<StrictHttpResponse<Array<TodoListDto>>> {
    const rb = new RequestBuilder(this.rootUrl, ListsService.GetAllListsPath, 'get');
    if (params) {
    }

    return this.http.request(
      rb.build({ responseType: 'json', accept: 'application/json', context })
    ).pipe(
      filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Array<TodoListDto>>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `getAllLists$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  getAllLists(
    params?: {
    },
    context?: HttpContext
  ): Observable<Array<TodoListDto>> {
    return this.getAllLists$Response(params, context).pipe(
      map((r: StrictHttpResponse<Array<TodoListDto>>): Array<TodoListDto> => r.body)
    );
  }

  /** Path part for operation `createList()` */
  static readonly CreateListPath = '/api/v1/lists';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `createList()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  createList$Response(
    params: {
      body: TodoListCreateDto
    },
    context?: HttpContext
  ): Observable<StrictHttpResponse<TodoListDto>> {
    const rb = new RequestBuilder(this.rootUrl, ListsService.CreateListPath, 'post');
    if (params) {
      rb.body(params.body, 'application/json');
    }

    return this.http.request(
      rb.build({ responseType: 'json', accept: 'application/json', context })
    ).pipe(
      filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<TodoListDto>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `createList$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  createList(
    params: {
      body: TodoListCreateDto
    },
    context?: HttpContext
  ): Observable<TodoListDto> {
    return this.createList$Response(params, context).pipe(
      map((r: StrictHttpResponse<TodoListDto>): TodoListDto => r.body)
    );
  }

  /** Path part for operation `getList()` */
  static readonly GetListPath = '/api/v1/lists/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `getList()` instead.
   *
   * This method doesn't expect any request body.
   */
  getList$Response(
    params: {
      id: string;
    },
    context?: HttpContext
  ): Observable<StrictHttpResponse<TodoListDto>> {
    const rb = new RequestBuilder(this.rootUrl, ListsService.GetListPath, 'get');
    if (params) {
      rb.path('id', params.id, {});
    }

    return this.http.request(
      rb.build({ responseType: 'json', accept: 'application/json', context })
    ).pipe(
      filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<TodoListDto>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `getList$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  getList(
    params: {
      id: string;
    },
    context?: HttpContext
  ): Observable<TodoListDto> {
    return this.getList$Response(params, context).pipe(
      map((r: StrictHttpResponse<TodoListDto>): TodoListDto => r.body)
    );
  }

  /** Path part for operation `updateList()` */
  static readonly UpdateListPath = '/api/v1/lists/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `updateList()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  updateList$Response(
    params: {
      id: string;
      body: TodoListUpdateDto
    },
    context?: HttpContext
  ): Observable<StrictHttpResponse<TodoListDto>> {
    const rb = new RequestBuilder(this.rootUrl, ListsService.UpdateListPath, 'put');
    if (params) {
      rb.path('id', params.id, {});
      rb.body(params.body, 'application/json');
    }

    return this.http.request(
      rb.build({ responseType: 'json', accept: 'application/json', context })
    ).pipe(
      filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<TodoListDto>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `updateList$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  updateList(
    params: {
      id: string;
      body: TodoListUpdateDto
    },
    context?: HttpContext
  ): Observable<TodoListDto> {
    return this.updateList$Response(params, context).pipe(
      map((r: StrictHttpResponse<TodoListDto>): TodoListDto => r.body)
    );
  }

  /** Path part for operation `removeList()` */
  static readonly RemoveListPath = '/api/v1/lists/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `removeList()` instead.
   *
   * This method doesn't expect any request body.
   */
  removeList$Response(
    params: {
      id: string;
    },
    context?: HttpContext
  ): Observable<StrictHttpResponse<Blob>> {
    const rb = new RequestBuilder(this.rootUrl, ListsService.RemoveListPath, 'delete');
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
   * To access the full response (for headers, for example), `removeList$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  removeList(
    params: {
      id: string;
    },
    context?: HttpContext
  ): Observable<Blob> {
    return this.removeList$Response(params, context).pipe(
      map((r: StrictHttpResponse<Blob>): Blob => r.body)
    );
  }

  /** Path part for operation `patchList()` */
  static readonly PatchListPath = '/api/v1/lists/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `patchList()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  patchList$Response(
    params: {
      id: string;
      body: TodoListPatchDto
    },
    context?: HttpContext
  ): Observable<StrictHttpResponse<TodoListDto>> {
    const rb = new RequestBuilder(this.rootUrl, ListsService.PatchListPath, 'patch');
    if (params) {
      rb.path('id', params.id, {});
      rb.body(params.body, 'application/json');
    }

    return this.http.request(
      rb.build({ responseType: 'json', accept: 'application/json', context })
    ).pipe(
      filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<TodoListDto>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `patchList$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  patchList(
    params: {
      id: string;
      body: TodoListPatchDto
    },
    context?: HttpContext
  ): Observable<TodoListDto> {
    return this.patchList$Response(params, context).pipe(
      map((r: StrictHttpResponse<TodoListDto>): TodoListDto => r.body)
    );
  }

}
