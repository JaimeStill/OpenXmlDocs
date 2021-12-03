import {
  Component,
  OnDestroy,
  OnInit
} from '@angular/core';

import {
  Doc,
  DocSource
} from 'core';

import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'documents-route',
  templateUrl: 'documents.route.html',
  providers: [ DocSource ]
})
export class DocumentsRoute implements OnInit, OnDestroy {
  private sub?: Subscription;

  constructor(
    private router: Router,
    public docSrc: DocSource
  ) { }

  ngOnInit() {
    this.sub = this.docSrc.urlSub;
    this.docSrc.setUrl('queryDocs');
  }

  ngOnDestroy() {
    this.sub?.unsubscribe();
  }

  editDoc = (doc: Doc) => this.router.navigate(['document', doc.id, 'edit']);
  fillDoc = (doc: Doc) => this.router.navigate(['document', doc.id, 'fill']);
  viewDoc = (doc: Doc) => this.router.navigate(['document', doc.id, 'view']);
}
