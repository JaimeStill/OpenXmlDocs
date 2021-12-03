import {
  OnInit,
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  Output
} from '@angular/core';

import { Subscription } from 'rxjs';
import { ApiQueryService } from '../../services';
import { QueryResult } from '../../models';

@Component({
  selector: 'async-container',
  templateUrl: 'async-container.component.html'
})
export class AsyncContainerComponent<T> implements OnInit, OnDestroy {
  private sub?: Subscription;

  @Input() src?: ApiQueryService<T>;
  @Input() inlineControls: boolean = false;
  @Input() outerStyle: string = 'p8';
  @Input() searchLabel: string = 'Search';
  @Input() searchMin: number = 1;
  @Output() query = new EventEmitter<QueryResult<T>>();

  ngOnInit() {
    this.sub = this.src
      ?.queryResult$
      .subscribe(q => this.query.emit(q));
  }

  ngOnDestroy() {
    this.sub?.unsubscribe();
  }
}
