import {
  AfterViewInit,
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnDestroy,
  Output,
  ViewChild
} from '@angular/core';

import { Subscription } from 'rxjs';
import { CoreService } from '../../services';

@Component({
  selector: 'searchbar',
  templateUrl: 'searchbar.component.html'
})
export class SearchbarComponent implements AfterViewInit, OnDestroy {
  private sub!: Subscription;

  @Input() label = 'Search';
  @Input() hint?: string;
  @Input() minimum = 2;
  @Output() search = new EventEmitter<string>();
  @Output() clear = new EventEmitter();

  @ViewChild('searchbar') searchbar?: ElementRef;

  constructor(
    private core: CoreService
  ) { }

  ngAfterViewInit() {
    if (this.searchbar) {
      this.sub = this.core.generateInputObservable(this.searchbar)
        .subscribe((val: string) => {
          val && val.length >= this.minimum
            ? this.search.emit(val)
            : this.clear.emit();
        });
    }
  }

  ngOnDestroy() {
    this.sub?.unsubscribe();
  }
}
