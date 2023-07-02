/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { BaseService as __BaseService } from '../base-service';
import { ApiConfiguration as __Configuration } from '../api-configuration';
import { StrictHttpResponse as __StrictHttpResponse } from '../strict-http-response';
import { Observable as __Observable } from 'rxjs';
import { map as __map, filter as __filter } from 'rxjs/operators';

import { TodoListDTO } from '../models/todo-list-dto';
import { TodoListCreateDTO } from '../models/todo-list-create-dto';
import { TodoListUpdateDTO } from '../models/todo-list-update-dto';
import { TodoListPatchDTO } from '../models/todo-list-patch-dto';
@Injectable({
  providedIn: 'root',
})
class ListsService extends __BaseService {
  static readonly GetAllListsPath = '/api/lists';
  static readonly CreateListPath = '/api/lists';
  static readonly GetListPath = '/api/lists/{id}';
  static readonly UpdateListPath = '/api/lists/{id}';
  static readonly PatchListPath = '/api/lists/{id}';
  static readonly RemoveListPath = '/api/lists/{id}';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }
  GetAllListsResponse(): __Observable<__StrictHttpResponse<Array<TodoListDTO>>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/lists`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<Array<TodoListDTO>>;
      })
    );
  }  GetAllLists(): __Observable<Array<TodoListDTO>> {
    return this.GetAllListsResponse().pipe(
      __map(_r => _r.body as Array<TodoListDTO>)
    );
  }

  /**
   * @param model undefined
   */
  CreateListResponse(model: TodoListCreateDTO): __Observable<__StrictHttpResponse<TodoListDTO>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = model;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/lists`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<TodoListDTO>;
      })
    );
  }
  /**
   * @param model undefined
   */
  CreateList(model: TodoListCreateDTO): __Observable<TodoListDTO> {
    return this.CreateListResponse(model).pipe(
      __map(_r => _r.body as TodoListDTO)
    );
  }

  /**
   * @param id undefined
   */
  GetListResponse(id: string): __Observable<__StrictHttpResponse<TodoListDTO>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/lists/${encodeURIComponent(String(id))}`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<TodoListDTO>;
      })
    );
  }
  /**
   * @param id undefined
   */
  GetList(id: string): __Observable<TodoListDTO> {
    return this.GetListResponse(id).pipe(
      __map(_r => _r.body as TodoListDTO)
    );
  }

  /**
   * @param params The `ListsService.UpdateListParams` containing the following parameters:
   *
   * - `model`:
   *
   * - `id`:
   */
  UpdateListResponse(params: ListsService.UpdateListParams): __Observable<__StrictHttpResponse<TodoListDTO>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = params.model;

    let req = new HttpRequest<any>(
      'PUT',
      this.rootUrl + `/api/lists/${encodeURIComponent(String(params.id))}`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<TodoListDTO>;
      })
    );
  }
  /**
   * @param params The `ListsService.UpdateListParams` containing the following parameters:
   *
   * - `model`:
   *
   * - `id`:
   */
  UpdateList(params: ListsService.UpdateListParams): __Observable<TodoListDTO> {
    return this.UpdateListResponse(params).pipe(
      __map(_r => _r.body as TodoListDTO)
    );
  }

  /**
   * @param params The `ListsService.PatchListParams` containing the following parameters:
   *
   * - `model`:
   *
   * - `id`:
   */
  PatchListResponse(params: ListsService.PatchListParams): __Observable<__StrictHttpResponse<TodoListDTO>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = params.model;

    let req = new HttpRequest<any>(
      'PATCH',
      this.rootUrl + `/api/lists/${encodeURIComponent(String(params.id))}`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<TodoListDTO>;
      })
    );
  }
  /**
   * @param params The `ListsService.PatchListParams` containing the following parameters:
   *
   * - `model`:
   *
   * - `id`:
   */
  PatchList(params: ListsService.PatchListParams): __Observable<TodoListDTO> {
    return this.PatchListResponse(params).pipe(
      __map(_r => _r.body as TodoListDTO)
    );
  }

  /**
   * @param id undefined
   */
  RemoveListResponse(id: string): __Observable<__StrictHttpResponse<Blob>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    let req = new HttpRequest<any>(
      'DELETE',
      this.rootUrl + `/api/lists/${encodeURIComponent(String(id))}`,
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
  RemoveList(id: string): __Observable<Blob> {
    return this.RemoveListResponse(id).pipe(
      __map(_r => _r.body as Blob)
    );
  }
}

module ListsService {

  /**
   * Parameters for UpdateList
   */
  export interface UpdateListParams {
    model: TodoListUpdateDTO;
    id: string;
  }

  /**
   * Parameters for PatchList
   */
  export interface PatchListParams {
    model: TodoListPatchDTO;
    id: string;
  }
}

export { ListsService }
