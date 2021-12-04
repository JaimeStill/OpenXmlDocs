import {
  Component,
  OnDestroy,
  OnInit
} from '@angular/core';

import {
  ConfirmDialog,
  Doc,
  DocDialog,
  DocService,
  DocSource,
  DocType,
  QueryResult
} from 'core';

import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'documents-route',
  templateUrl: 'documents.route.html',
  providers: [
    DocService,
    DocSource
  ]
})
export class DocumentsRoute implements OnInit, OnDestroy {
  private sub?: Subscription;

  query?: QueryResult<Doc>;

  constructor(
    private dialog: MatDialog,
    private docSvc: DocService,
    private router: Router,
    public docSrc: DocSource
  ) { }

  ngOnInit() {
    this.sub = this.docSrc.urlSub;
    this.docSrc.setUrl('queryDocs');
  }

  setQuery = (q: QueryResult<Doc>) => this.query = q;

  ngOnDestroy() {
    this.sub?.unsubscribe();
  }

  add = () => this.dialog.open(DocDialog, {
    data: { doc: {} as Doc, type: DocType.Document },
    disableClose: true,
    minWidth: '500px',
    maxWidth: '800px',
    width: '90%'
  })
  .afterClosed()
  .subscribe(res => res && this.docSrc.forceRefresh());

  edit = (doc: Doc) => this.router.navigate(['document', doc.id, 'edit']);
  fill = (doc: Doc) => this.router.navigate(['document', doc.id, 'fill']);
  view = (doc: Doc) => this.router.navigate(['document', doc.id, 'view']);

  clone = async (doc: Doc) => {
    const res = await this.docSvc.cloneDoc(doc);
    res && this.docSrc.forceRefresh();
  }

  remove = (doc: Doc) => this.dialog.open(ConfirmDialog, {
    data: {
      title: `Remove ${doc.name}`,
      content: `Are you sure you want to remove document ${doc.name}?`
    },
    autoFocus: false,
    disableClose: true
  })
  .afterClosed()
  .subscribe(async (result: boolean) => {
    if (result) {
      const res = await this.docSvc.removeDoc(doc);
      res && this.docSrc.forceRefresh();
    }
  });
}
