/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { BaseService as __BaseService } from '../base-service';
import { ApiConfiguration as __Configuration } from '../api-configuration';
import { StrictHttpResponse as __StrictHttpResponse } from '../strict-http-response';
import { Observable as __Observable } from 'rxjs';
import { map as __map, filter as __filter } from 'rxjs/operators';

import { TodoItemDTO } from '../models/todo-item-dto';
import { TodoItemCreateDTO } from '../models/todo-item-create-dto';
import { TodoItemUpdateDTO } from '../models/todo-item-update-dto';
import { TodoItemPatchDTO } from '../models/todo-item-patch-dto';
@Injectable({
  providedIn: 'root',
})
class ItemsService extends __BaseService {
  static readonly GetAllItemsPath = '/';
  static readonly CreateItemPath = '/';
  static readonly GetItemPath = '/{id}';
  static readonly UpdateItemPath = '/{id}';
  static readonly PatchItemPath = '/{id}';
  static readonly RemoveItemPath = '/{id}';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }
  GetAllItemsResponse(): __Observable<__StrictHttpResponse<Array<TodoItemDTO>>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<Array<TodoItemDTO>>;
      })
    );
  }  GetAllItems(): __Observable<Array<TodoItemDTO>> {
    return this.GetAllItemsResponse().pipe(
      __map(_r => _r.body as Array<TodoItemDTO>)
    );
  }

  /**
   * @param model undefined
   */
  CreateItemResponse(model: TodoItemCreateDTO): __Observable<__StrictHttpResponse<TodoItemDTO>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = model;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<TodoItemDTO>;
      })
    );
  }
  /**
   * @param model undefined
   */
  CreateItem(model: TodoItemCreateDTO): __Observable<TodoItemDTO> {
    return this.CreateItemResponse(model).pipe(
      __map(_r => _r.body as TodoItemDTO)
    );
  }

  /**
   * @param id undefined
   */
  GetItemResponse(id: string): __Observable<__StrictHttpResponse<TodoItemDTO>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/${encodeURIComponent(String(id))}`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<TodoItemDTO>;
      })
    );
  }
  /**
   * @param id undefined
   */
  GetItem(id: string): __Observable<TodoItemDTO> {
    return this.GetItemResponse(id).pipe(
      __map(_r => _r.body as TodoItemDTO)
    );
  }

  /**
   * @param params The `ItemsService.UpdateItemParams` containing the following parameters:
   *
   * - `model`:
   *
   * - `id`:
   */
  UpdateItemResponse(params: ItemsService.UpdateItemParams): __Observable<__StrictHttpResponse<TodoItemDTO>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = params.model;

    let req = new HttpRequest<any>(
      'PUT',
      this.rootUrl + `/${encodeURIComponent(String(params.id))}`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<TodoItemDTO>;
      })
    );
  }
  /**
   * @param params The `ItemsService.UpdateItemParams` containing the following parameters:
   *
   * - `model`:
   *
   * - `id`:
   */
  UpdateItem(params: ItemsService.UpdateItemParams): __Observable<TodoItemDTO> {
    return this.UpdateItemResponse(params).pipe(
      __map(_r => _r.body as TodoItemDTO)
    );
  }

  /**
   * @param params The `ItemsService.PatchItemParams` containing the following parameters:
   *
   * - `model`:
   *
   * - `id`:
   */
  PatchItemResponse(params: ItemsService.PatchItemParams): __Observable<__StrictHttpResponse<TodoItemDTO>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = params.model;

    let req = new HttpRequest<any>(
      'PATCH',
      this.rootUrl + `/${encodeURIComponent(String(params.id))}`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<TodoItemDTO>;
      })
    );
  }
  /**
   * @param params The `ItemsService.PatchItemParams` containing the following parameters:
   *
   * - `model`:
   *
   * - `id`:
   */
  PatchItem(params: ItemsService.PatchItemParams): __Observable<TodoItemDTO> {
    return this.PatchItemResponse(params).pipe(
      __map(_r => _r.body as TodoItemDTO)
    );
  }

  /**
   * @param id undefined
   */
  RemoveItemResponse(id: string): __Observable<__StrictHttpResponse<Blob>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    let req = new HttpRequest<any>(
      'DELETE',
      this.rootUrl + `/${encodeURIComponent(String(id))}`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'blob'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<Blob>;
      })
    );
  }
  /**
   * @param id undefined
   */
  RemoveItem(id: string): __Observable<Blob> {
    return this.RemoveItemResponse(id).pipe(
      __map(_r => _r.body as Blob)
    );
  }
}

module ItemsService {

  /**
   * Parameters for UpdateItem
   */
  export interface UpdateItemParams {
    model: TodoItemUpdateDTO;
    id: string;
  }

  /**
   * Parameters for PatchItem
   */
  export interface PatchItemParams {
    model: TodoItemPatchDTO;
    id: string;
  }
}

export { ItemsService }
