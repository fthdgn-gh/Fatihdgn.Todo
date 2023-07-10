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

import { TodoItemCreateDto } from '../models/todo-item-create-dto';
import { TodoItemDto } from '../models/todo-item-dto';
import { TodoItemPatchDto } from '../models/todo-item-patch-dto';
import { TodoItemUpdateDto } from '../models/todo-item-update-dto';

@Injectable({ providedIn: 'root' })
export class ItemsService extends BaseService {
  constructor(config: ApiConfiguration, http: HttpClient) {
    super(config, http);
  }

  /** Path part for operation `getAllItemsByListId()` */
  static readonly GetAllItemsByListIdPath = '/api/v1/items/by/lists/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `getAllItemsByListId()` instead.
   *
   * This method doesn't expect any request body.
   */
  getAllItemsByListId$Response(
    params: {
      id: string;
    },
    context?: HttpContext
  ): Observable<StrictHttpResponse<Array<TodoItemDto>>> {
    const rb = new RequestBuilder(this.rootUrl, ItemsService.GetAllItemsByListIdPath, 'get');
    if (params) {
      rb.path('id', params.id, {});
    }

    return this.http.request(
      rb.build({ responseType: 'json', accept: 'application/json', context })
    ).pipe(
      filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Array<TodoItemDto>>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `getAllItemsByListId$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  getAllItemsByListId(
    params: {
      id: string;
    },
    context?: HttpContext
  ): Observable<Array<TodoItemDto>> {
    return this.getAllItemsByListId$Response(params, context).pipe(
      map((r: StrictHttpResponse<Array<TodoItemDto>>): Array<TodoItemDto> => r.body)
    );
  }

  /** Path part for operation `getItem()` */
  static readonly GetItemPath = '/api/v1/items/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `getItem()` instead.
   *
   * This method doesn't expect any request body.
   */
  getItem$Response(
    params: {
      id: string;
    },
    context?: HttpContext
  ): Observable<StrictHttpResponse<TodoItemDto>> {
    const rb = new RequestBuilder(this.rootUrl, ItemsService.GetItemPath, 'get');
    if (params) {
      rb.path('id', params.id, {});
    }

    return this.http.request(
      rb.build({ responseType: 'json', accept: 'application/json', context })
    ).pipe(
      filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<TodoItemDto>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `getItem$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  getItem(
    params: {
      id: string;
    },
    context?: HttpContext
  ): Observable<TodoItemDto> {
    return this.getItem$Response(params, context).pipe(
      map((r: StrictHttpResponse<TodoItemDto>): TodoItemDto => r.body)
    );
  }

  /** Path part for operation `updateItem()` */
  static readonly UpdateItemPath = '/api/v1/items/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `updateItem()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  updateItem$Response(
    params: {
      id: string;
      body: TodoItemUpdateDto
    },
    context?: HttpContext
  ): Observable<StrictHttpResponse<TodoItemDto>> {
    const rb = new RequestBuilder(this.rootUrl, ItemsService.UpdateItemPath, 'put');
    if (params) {
      rb.path('id', params.id, {});
      rb.body(params.body, 'application/json');
    }

    return this.http.request(
      rb.build({ responseType: 'json', accept: 'application/json', context })
    ).pipe(
      filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<TodoItemDto>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `updateItem$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  updateItem(
    params: {
      id: string;
      body: TodoItemUpdateDto
    },
    context?: HttpContext
  ): Observable<TodoItemDto> {
    return this.updateItem$Response(params, context).pipe(
      map((r: StrictHttpResponse<TodoItemDto>): TodoItemDto => r.body)
    );
  }

  /** Path part for operation `removeItem()` */
  static readonly RemoveItemPath = '/api/v1/items/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `removeItem()` instead.
   *
   * This method doesn't expect any request body.
   */
  removeItem$Response(
    params: {
      id: string;
    },
    context?: HttpContext
  ): Observable<StrictHttpResponse<Blob>> {
    const rb = new RequestBuilder(this.rootUrl, ItemsService.RemoveItemPath, 'delete');
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
   * To access the full response (for headers, for example), `removeItem$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  removeItem(
    params: {
      id: string;
    },
    context?: HttpContext
  ): Observable<Blob> {
    return this.removeItem$Response(params, context).pipe(
      map((r: StrictHttpResponse<Blob>): Blob => r.body)
    );
  }

  /** Path part for operation `patchItem()` */
  static readonly PatchItemPath = '/api/v1/items/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `patchItem()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  patchItem$Response(
    params: {
      id: string;
      body: TodoItemPatchDto
    },
    context?: HttpContext
  ): Observable<StrictHttpResponse<TodoItemDto>> {
    const rb = new RequestBuilder(this.rootUrl, ItemsService.PatchItemPath, 'patch');
    if (params) {
      rb.path('id', params.id, {});
      rb.body(params.body, 'application/json');
    }

    return this.http.request(
      rb.build({ responseType: 'json', accept: 'application/json', context })
    ).pipe(
      filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<TodoItemDto>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `patchItem$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  patchItem(
    params: {
      id: string;
      body: TodoItemPatchDto
    },
    context?: HttpContext
  ): Observable<TodoItemDto> {
    return this.patchItem$Response(params, context).pipe(
      map((r: StrictHttpResponse<TodoItemDto>): TodoItemDto => r.body)
    );
  }

  /** Path part for operation `createItem()` */
  static readonly CreateItemPath = '/api/v1/items';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `createItem()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  createItem$Response(
    params: {
      body: TodoItemCreateDto
    },
    context?: HttpContext
  ): Observable<StrictHttpResponse<TodoItemDto>> {
    const rb = new RequestBuilder(this.rootUrl, ItemsService.CreateItemPath, 'post');
    if (params) {
      rb.body(params.body, 'application/json');
    }

    return this.http.request(
      rb.build({ responseType: 'json', accept: 'application/json', context })
    ).pipe(
      filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<TodoItemDto>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `createItem$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  createItem(
    params: {
      body: TodoItemCreateDto
    },
    context?: HttpContext
  ): Observable<TodoItemDto> {
    return this.createItem$Response(params, context).pipe(
      map((r: StrictHttpResponse<TodoItemDto>): TodoItemDto => r.body)
    );
  }

}
