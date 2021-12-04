import {
  Injectable,
  Optional
} from '@angular/core';

import {
  Doc,
  DocAnswer,
  DocCategory,
  DocItem,
  DocOption,
  SaveResult
} from '../../models';

import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { ServerConfig } from '../../config';
import { SnackerService } from '../snacker.service';

@Injectable({
  providedIn: 'root'
})
export class DocService {
  private doc = new BehaviorSubject<Doc | null>(null);
  private docCategories = new BehaviorSubject<DocCategory[] | null>(null);
  private docCategory = new BehaviorSubject<DocCategory | null>(null);
  private docItems = new BehaviorSubject<DocItem[] | null>(null);
  private docItem = new BehaviorSubject<DocItem | null>(null);
  private docOptions = new BehaviorSubject<DocOption[] | null>(null);
  private docOption = new BehaviorSubject<DocOption | null>(null);
  private docAnswer = new BehaviorSubject<DocAnswer | null>(null);

  doc$ = this.doc.asObservable();
  docCategories$ = this.docCategories.asObservable();
  docCategory$ = this.docCategory.asObservable();
  docItems$ = this.docItems.asObservable();
  docItem$ = this.docItem.asObservable();
  docOptions$ = this.docOptions.asObservable();
  docOption$ = this.docOption.asObservable();
  docAnswer$ = this.docAnswer.asObservable();

  constructor(
    private http: HttpClient,
    private snacker: SnackerService,
    @Optional() private config: ServerConfig
  ) { }

  //#region Doc

  getDoc = (id: number) => this.http.get<Doc>(`${this.config.api}doc/getDoc/${id}`)
    .subscribe({
      next: data => this.doc.next(data),
      error: err => this.snacker.sendErrorMessage(err.message)
    });

  verifyDoc = (doc: Doc): Promise<boolean> => new Promise((resolve) => {
    this.http.post<boolean>(`${this.config.api}doc/verifyDoc`, doc)
      .subscribe({
        next: data => resolve(data),
        error: err => {
          this.snacker.sendErrorMessage(err.error);
          resolve(false);
        }
      })
  });

  cloneDoc = (doc: Doc): Promise<Doc | null> => new Promise((resolve) => {
    this.http.post<Doc>(`${this.config.api}doc/cloneDoc`, doc)
      .subscribe({
        next: data => resolve(data),
        error: err => {
          this.snacker.sendErrorMessage(err.message);
          resolve(null);
        }
      });
  });

  saveDoc = (doc: Doc): Promise<boolean> => new Promise((resolve) => {
    this.http.post<SaveResult>(`${this.config.api}doc/saveDoc`, doc)
      .subscribe({
        next: data => {
          if (data.isValid) {
            this.snacker.sendSuccessMessage(`${doc.name} successfully saved.`);
            resolve(true);
          } else {
            this.snacker.sendErrorMessage(data.message ?? `An error occurred attempting to save ${doc.name}`);
            resolve(false);
          }
        },
        error: err => {
          this.snacker.sendErrorMessage(err.message);
          resolve(false);
        }
      })
  });

  removeDoc = (doc: Doc): Promise<boolean> => new Promise((resolve) => {
    this.http.post(`${this.config.api}doc/removeDoc`, doc)
      .subscribe({
        next: () => {
          this.snacker.sendSuccessMessage(`${doc.name} successfully removed.`);
          resolve(true);
        },
        error: err => {
          this.snacker.sendErrorMessage(err.error);
          resolve(false);
        }
      })
  });

  //#endregion

  //#region Category

  getDocCategories = () => this.http.get<DocCategory[]>(`${this.config.api}doc/getDocCategories`)
    .subscribe({
      next: data => this.docCategories.next(data),
      error: err => this.snacker.sendErrorMessage(err.message)
    });

  getDocCategory = (id: number) => this.http.get<DocCategory | null>(`${this.config.api}doc/getDocCategory/${id}`)
    .subscribe({
      next: data => this.docCategory.next(data),
      error: err => this.snacker.sendErrorMessage(err.message)
    });

  verifyCategory = (category: DocCategory): Promise<boolean> => new Promise((resolve) => {
    this.http.post<boolean>(`${this.config.api}doc/verifyCategory`, category)
      .subscribe({
        next: data => resolve(data),
        error: err => {
          this.snacker.sendErrorMessage(err.message);
          resolve(false);
        }
      })
  });

  saveDocCategory = (category: DocCategory): Promise<boolean> => new Promise((resolve) => {
    this.http.post<SaveResult>(`${this.config.api}doc/saveDocCategory`, category)
      .subscribe({
        next: data => {
          if (data.isValid) {
            this.snacker.sendSuccessMessage(`${category.value} successfully saved.`);
            resolve(true);
          } else {
            this.snacker.sendErrorMessage(data.message ?? `An error occurred attempting to save ${category.value}`);
            resolve(false);
          }
        },
        error: err => {
          this.snacker.sendErrorMessage(err.message);
          resolve(false);
        }
      })
  });

  removeDocCategory = (category: DocCategory): Promise<boolean> => new Promise((resolve) => {
    this.http.post(`${this.config.api}doc/removeDocCategory`, category)
      .subscribe({
        next: () => {
          this.snacker.sendSuccessMessage(`${category.value} successfully removed.`);
          resolve(true);
        },
        error: err => {
          this.snacker.sendErrorMessage(err.message);
          resolve(false);
        }
      })
  });

  //#endregion

  //#region Item

  getDocItems = (docTId: number) => this.http.get<DocItem[]>(`${this.config.api}doc/getDocItems/${docTId}`)
    .subscribe({
      next: data => this.docItems.next(data),
      error: err => this.snacker.sendErrorMessage(err.message)
    });

  getDocItem = (id: number) => this.http.get<DocItem | null>(`${this.config.api}doc/getDocItem/${id}`)
    .subscribe({
      next: data => this.docItem.next(data),
      error: err => this.snacker.sendErrorMessage(err.message)
    });

  saveDocItem = (item: DocItem): Promise<boolean> => new Promise((resolve) => {
    this.http.post<SaveResult>(`${this.config.api}doc/saveDocItem`, item)
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

  removeDocItem = (item: DocItem): Promise<boolean> => new Promise((resolve) => {
    this.http.post(`${this.config.api}doc/removeDocItem`, item)
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

  //#region Option

  getDocOptions = (selectId: number) => this.http.get<DocOption[]>(`${this.config.api}doc/getDocOptions/${selectId}`)
    .subscribe({
      next: data => this.docOptions.next(data),
      error: err => this.snacker.sendErrorMessage(err.message)
    });

  getDocOption = (id: number) => this.http.get<DocOption | null>(`${this.config.api}doc/getDocOption/${id}`)
    .subscribe({
      next: data => this.docOption.next(data),
      error: err => this.snacker.sendErrorMessage(err.message)
    });

  saveDocOption = (option: DocOption): Promise<boolean> => new Promise((resolve) => {
    this.http.post<SaveResult>(`${this.config.api}doc/saveDocOption`, option)
      .subscribe({
        next: data => {
          if (data.isValid) {
            this.snacker.sendSuccessMessage(`${option.value} successfully saved`);
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

  removeDocOption = (option: DocOption): Promise<boolean> => new Promise((resolve) => {
    this.http.post(`${this.config.api}doc/removeDocOption`, option)
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

  //#region Answer

  getDocAnswer = (questionId: number) => this.http.get<DocAnswer | null>(`${this.config.api}doc/getDocAnswer/${questionId}`)
    .subscribe({
      next: data => this.docAnswer.next(data),
      error: err => this.snacker.sendErrorMessage(err.message)
    });

  saveDocAnswer = (answer: DocAnswer): Promise<boolean> => new Promise((resolve) => {
    this.http.post<SaveResult>(`${this.config.api}doc/saveDocAnswer`, answer)
      .subscribe({
        next: data => {
          if (data.isValid) {
            this.snacker.sendSuccessMessage(`Answer successfully saved.`);
            resolve(true);
          } else {
            this.snacker.sendErrorMessage(data.message ?? `An error occurred attempting to save the answer.`);
            resolve(false);
          }
        },
        error: err => {
          this.snacker.sendErrorMessage(err.message);
          resolve(false);
        }
      })
  });

  removeDocAnswer = (answer: DocAnswer): Promise<boolean> => new Promise((resolve) => {
    this.http.post(`${this.config.api}doc/removeDocAnswer`, answer)
      .subscribe({
        next: () => {
          this.snacker.sendSuccessMessage(`Answer successfully removed.`);
          resolve(true);
        },
        error: err => {
          this.snacker.sendErrorMessage(err.message);
          resolve(false);
        }
      })
  })

  //#endregion
}
