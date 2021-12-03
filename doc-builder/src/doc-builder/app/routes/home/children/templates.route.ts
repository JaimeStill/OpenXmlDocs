import {
  Component,
  OnDestroy,
  OnInit
} from '@angular/core';

import {
  ConfirmDialog,
  DocT,
  DocTService,
  DocTSource,
  QueryResult
} from 'core';

import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'templates-route',
  templateUrl: 'templates.route.html',
  providers: [
    DocTService,
    DocTSource
  ]
})
export class TemplatesRoute implements OnInit, OnDestroy {
  private sub?: Subscription;

  query?: QueryResult<DocT>;

  constructor(
    private dialog: MatDialog,
    private docTSvc: DocTService,
    private router: Router,
    public docTSrc: DocTSource
  ) { }

  ngOnInit() {
    this.sub = this.docTSrc.urlSub;
    this.docTSrc.setUrl('queryDocTs');
  }

  ngOnDestroy() {
    this.sub?.unsubscribe();
  }

  setQuery = (q: QueryResult<DocT>) => this.query = q;

  add = () => this.router.navigate(['template', 'new']);
  edit = (docT: DocT) => this.router.navigate(['template', docT.id, 'edit']);
  view = (docT: DocT) => this.router.navigate(['template', docT.id, 'view']);

  clone = async (docT: DocT) => {
    const res = await this.docTSvc.cloneDocT(docT);
    res && this.docTSrc.forceRefresh();
  }

  generate = async (docT: DocT) => {
    const res = await this.docTSvc.generateDoc(docT);
    res && this.router.navigate(['document', res.id, 'fill']);
  }

  remove = (docT: DocT) => this.dialog.open(ConfirmDialog, {
    data: {
      title: `Remove ${docT.name}`,
      content: `Are you sure you want to remove document template ${docT.name}`
    },
    autoFocus: false,
    disableClose: true
  })
  .afterClosed()
  .subscribe(async (result: boolean) => {
    if (result) {
      const res = await this.docTSvc.removeDocT(docT);
      res && this.docTSrc.forceRefresh();
    }
  });
}
