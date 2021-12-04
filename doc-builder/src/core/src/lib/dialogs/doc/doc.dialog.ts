import {
  MatDialogRef,
  MAT_DIALOG_DATA
} from '@angular/material/dialog';

import {
  AfterViewInit,
  Component,
  ElementRef,
  Inject,
  OnDestroy,
  OnInit,
  ViewChild
} from '@angular/core';

import {
  Doc,
  DocCategory,
  DocT,
  DocType
} from '../../models';

import {
  CoreService,
  DocService,
  DocTService
} from '../../services';

import { Subscription } from 'rxjs';

@Component({
  selector: 'doc-dialog',
  templateUrl: 'doc.dialog.html',
  providers: [
    DocService,
    DocTService
  ]
})
export class DocDialog implements AfterViewInit, OnInit, OnDestroy{
  private subs = new Array<Subscription>();
  nameValid: boolean | undefined;
  categoryValid: boolean | undefined;

  constructor(
    private core: CoreService,
    private dialog: MatDialogRef<DocDialog>,
    private docTSvc: DocTService,
    @Inject(MAT_DIALOG_DATA) public data: { doc: Doc | DocT, type: DocType },
    public docSvc: DocService,
  ) { }

  @ViewChild('name') name!: ElementRef;
  @ViewChild('category') category!: ElementRef;

  ngAfterViewInit() {
    this.subs.push(
      this.core.generateInputObservable(this.name)
        .subscribe(async (val: string) => {
          if (val && val.length > 0) {
            this.nameValid = this.data.type === DocType.Document
              ? await this.docSvc.verifyDoc(this.data.doc as Doc)
              : await this.docTSvc.verifyTemplate(this.data.doc as DocT);
          } else this.nameValid = undefined;
        }),
      this.core.generateInputObservable(this.category)
        .subscribe(async (val: string) => {
          if (val && val.length > 0)
            this.categoryValid = await this.docSvc.verifyCategory({value: val} as DocCategory);
          else
            this.categoryValid = undefined;
        })
    )
  }

  ngOnInit() {
    this.docSvc.getDocCategories();
  }

  ngOnDestroy() {
    this.subs.forEach(sub => sub.unsubscribe());
  }

  title = () => this.data.type === DocType.Document
    ? 'Create Document'
    : 'Create Document Template';

  addCategory = async (value: string) => {
    const res = await this.docSvc.saveDocCategory({ value: value } as DocCategory);
    res && this.docSvc.getDocCategories();
    this.category.nativeElement.value = '';
    this.categoryValid = undefined;
  }

  save = async () => {
    const res = this.data.type === DocType.Document
      ? await this.docSvc.saveDoc(this.data.doc as Doc)
      : await this.docTSvc.saveDocT(this.data.doc as DocT);

    res && this.dialog.close(true);
  }
}
