import {
  Injectable,
  Optional
} from '@angular/core';

import {
  Doc,
  DocT,
  DocItemT,
  DocOptionT,
  SaveResult
} from '../../models';

import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { ServerConfig } from '../../config';
import { SnackerService } from '../snacker.service';

@Injectable({
  providedIn: 'root'
})
export class DocTService {
  private docT = new BehaviorSubject<DocT | null>(null);
  private docItemTs = new BehaviorSubject<DocItemT[] | null>(null);
  private docItemT = new BehaviorSubject<DocItemT | null>(null);
  private docOptionTs = new BehaviorSubject<DocOptionT[] | null>(null);
  private docOptionT = new BehaviorSubject<DocOptionT | null>(null);

  docT$ = this.docT.asObservable();
  docItemTs$ = this.docItemTs.asObservable();
  docItemT$ = this.docItemT.asObservable();
  docOptionTs$ = this.docOptionTs.asObservable();
  docOptionT$ = this.docOptionT.asObservable();

  constructor(
    private http: HttpClient,
    private snacker: SnackerService,
    @Optional() private config: ServerConfig
  ) { }

  //#region DocT

  getDocT = (id: number) => this.http.get<DocT>(`${this.config.api}docT/getDocT/${id}`)
    .subscribe({
      next: data => this.docT.next(data),
      error: err => this.snacker.sendErrorMessage(err.message)
    });

  verifyTemplate = (docT: DocT): Promise<boolean> => new Promise((resolve) => {
    this.http.post<boolean>(`${this.config.api}docT/verifyTemplate`, docT)
      .subscribe({
        next: data => resolve(data),
        error: err => {
          this.snacker.sendErrorMessage(err.error);
          resolve(false);
        }
      })
  });

  cloneDocT = (docT: DocT): Promise<DocT | null> => new Promise((resolve) => {
    this.http.post<DocT>(`${this.config.api}docT/cloneDocT`, docT)
      .subscribe({
        next: data => resolve(data),
        error: err => {
          this.snacker.sendErrorMessage(err.message);
          resolve(null);
        }
      });
  });

  generateDoc = (docT: DocT): Promise<Doc | null> => new Promise((resolve) => {
    this.http.post<Doc>(`${this.config.api}docT/generateDoc`, docT)
      .subscribe({
        next: data => resolve(data),
        error: err => {
          this.snacker.sendErrorMessage(err.message);
          resolve(null);
        }
      })
  });

  saveDocT = (docT: DocT): Promise<boolean> => new Promise((resolve) => {
    this.http.post<SaveResult>(`${this.config.api}docT/saveDocT`, docT)
      .subscribe({
        next: data => {
          if (data.isValid) {
            this.snacker.sendSuccessMessage(`${docT.name} successfully saved.`);
            resolve(true);
          } else {
            this.snacker.sendErrorMessage(data.message ?? `An error occurred attempting to save ${docT.name}`);
            resolve(false);
          }
        },
        error: err => {
          this.snacker.sendErrorMessage(err.message);
          resolve(false);
        }
      })
  });

  removeDocT = (docT: DocT): Promise<boolean> => new Promise((resolve) => {
    this.http.post(`${this.config.api}docT/removeDocT`, docT)
      .subscribe({
        next: () => {
          this.snacker.sendSuccessMessage(`${docT.name} successfully removed.`);
          resolve(true);
        },
        error: err => {
          this.snacker.sendErrorMessage(err.error);
          resolve(false);
        }
      })
  });

  //#endregion

  //#region ItemT

  getDocItemTs = (docTId: number) => this.http.get<DocItemT[]>(`${this.config.api}docT/getDocItemTs/${docTId}`)
    .subscribe({
      next: data => this.docItemTs.next(data),
      error: err => this.snacker.sendErrorMessage(err.message)
    });

  getDocItemT = (id: number) => this.http.get<DocItemT | null>(`${this.config.api}docT/getDocItemT/${id}`)
    .subscribe({
      next: data => this.docItemT.next(data),
      error: err => this.snacker.sendErrorMessage(err.message)
    });

  saveDocItemT = (item: DocItemT): Promise<boolean> => new Promise((resolve) => {
    this.http.post<SaveResult>(`${this.config.api}docT/saveDocItemT`, item)
      .subscribe({
        next: data => {
          if (data.isValid) {
            this.snacker.sendSuccessMessage(`${item.type} Item successfully saved.`);
            resolve(true);
          } else {
            this.snacker.sendErrorMessage(data.message ?? `An error occurred attempting to save ${item.type} Item.`);
            resolve(false);
          }
        },
        error: err => {
          this.snacker.sendErrorMessage(err.message);
          resolve(false);
        }
      })
  });

  removeDocItemT = (item: DocItemT): Promise<boolean> => new Promise((resolve) => {
    this.http.post(`${this.config.api}docT/removeDocItemT`, item)
      .subscribe({
        next: () => {
          this.snacker.sendSuccessMessage(`${item.type} Item successfully removed.`);
          resolve(true);
        },
        error: err => {
          this.snacker.sendErrorMessage(err.message);
          resolve(false);
        }
      })
  });

  //#endregion

  //#region OptionT

  getDocOptionTs = (selectId: number) => this.http.get<DocOptionT[]>(`${this.config.api}docT/getDocOptionTs/${selectId}`)
    .subscribe({
      next: data => this.docOptionTs.next(data),
      error: err => this.snacker.sendErrorMessage(err.message)
    });

  getDocOptionT = (id: number) => this.http.get<DocOptionT | null>(`${this.config.api}docT/getDocOptionT/${id}`)
    .subscribe({
      next: data => this.docOptionT.next(data),
      error: err => this.snacker.sendErrorMessage(err.message)
    });

  saveDocOptionT = (option: DocOptionT): Promise<boolean> => new Promise((resolve) => {
    this.http.post<SaveResult>(`${this.config.api}docT/saveDocOptionT`, option)
      .subscribe({
        next: data => {
          if (data.isValid) {
            this.snacker.sendSuccessMessage(`${option.value} successfully saved.`);
            resolve(true);
          } else {
            this.snacker.sendErrorMessage(data.message ?? `An error occurred attempting to save ${option.value}`);
            resolve(false);
          }
        },
        error: err => {
          this.snacker.sendErrorMessage(err.message);
          resolve(false);
        }
      })
  });

  removeDocOptionT = (option: DocOptionT): Promise<boolean> => new Promise((resolve) => {
    this.http.post(`${this.config.api}docT/removeDocOptionT`, option)
      .subscribe({
        next: () => {
          this.snacker.sendSuccessMessage(`${option.value} successfully removed.`);
          resolve(true);
        },
        error: err => {
          this.snacker.sendErrorMessage(err.message);
          resolve(false);
        }
      })
  });

  //#endregion
}
