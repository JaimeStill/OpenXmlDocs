import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PageEvent } from '@angular/material/paginator';
import { QueryResult } from '../../models';

import {
  Subject,
  Subscription,
  ReplaySubject,
  merge,
  throwError
} from 'rxjs';

import {
  catchError,
  debounceTime,
  filter,
  switchMap
} from 'rxjs/operators';

@Injectable()
export class ApiQueryService<T> {
  private requestUrl = new Subject<string>();
  private forceRefreshUrl = new Subject<string>();

  protected queryResult = new ReplaySubject<QueryResult<T>>(1);
  protected lastQueryResult?: QueryResult<T>;

  queryResult$ = this.queryResult.asObservable();
  pageSizeOptions = [5, 10, 20, 50, 100];
  urlSub: Subscription;

  constructor(
    protected http: HttpClient
  ) {
    this.urlSub = merge(this.requestUrl, this.forceRefreshUrl)
      .pipe(
        debounceTime(0),
        switchMap((url: string) =>
          this.http.get<QueryResult<T>>(url).pipe(
            catchError(er => throwError(() => new Error(er)))
          )
        ),
        filter(r => r != null)
      )
      .subscribe(
        result => {
          this.lastQueryResult = result;
          this.queryResult.next(result);
        }
      );
  }

  private _baseUrl: string | null = null;
  get baseUrl(): string | null { return this._baseUrl; }
  set baseUrl(value: string | null) {
    if (value !== this._baseUrl) {
      this._baseUrl = value;
      this.refresh();
    }
  }

  private _page = 1;
  get page(): number { return this._page; }
  set page(value: number) {
    if (value !== this._page) {
      this._page = value;
      this.refresh();
    }
  }

  private _pageSize = 20;
  get pageSize(): number { return this._pageSize; }
  set pageSize(value: number) {
    if (value !== this._pageSize) {
      this._pageSize = value;
      this.refresh();
    }
  }

  private _sort: { propertyName: string, isDescending: boolean } | null = null;
  get sort(): { propertyName: string, isDescending: boolean } | null {
    return this._sort;
  }
  set sort(value: { propertyName: string, isDescending: boolean } | null) {
    this._sort = value;
    this.refresh();
  }

  private _search: string | null  = null;
  get search(): string | null {
    return this._search;
  }
  set search(value: string | null) {
    this._search = value;
    this.refresh();
  }

  private _additionalQueryParams: { [parameter: string]: string } = {};
  setQueryParameter(name: string, value: string) {
    this._additionalQueryParams[name] = value;
    this.refresh();
  }
  getQueryParameter(parameterName: string): string | null {
    return this._additionalQueryParams[parameterName] || null;
  }

  private refresh = () => {
    if (!this.baseUrl) return;

    const url = this.buildUrl();
    this.requestUrl.next(url);
  }

  private buildUrl = () => {
    let url = `${this.baseUrl}?page=${this.page}&pageSize=${this.pageSize}`;

    if (this.sort) {
      const sortParam = `sort=${this.sort.propertyName}_${this.sort.isDescending ? 'desc' : 'asc'}`;
      url += `&${sortParam}`;
    }

    if (this.search) {
      const searchParam = `search=${this.search}`;
      url += `&${searchParam}`;
    }

    for (const name in this._additionalQueryParams) {
      if (this._additionalQueryParams.hasOwnProperty(name)) {
        const value = this._additionalQueryParams[name];

        if (value) {
          url += `&${name}=${value}`;
        }
      }
    }

    return url;
  }

  forceRefresh = () => {
    const url = this.buildUrl();
    this.forceRefreshUrl.next(url);
  }

  clearSearch = () => this.search = null;

  onPage = (pageEvent: PageEvent) => {
    this.page = pageEvent.pageIndex + 1;
    this.pageSize = pageEvent.pageSize;
  }

  onSearch = (event: string) => this.search = event;

  onSort = (event: { active: string, direction: string }) => this.sort = event.direction
    ? { propertyName: event.active, isDescending: event.direction === 'desc' }
    : null
}
