import {
  Component,
  Input,
  Output,
  EventEmitter
} from '@angular/core';

import { DocT } from '../../models';

@Component({
  selector: 'doc-t-card',
  templateUrl: 'doc-t-card.component.html'
})
export class DocTCardComponent {
  @Input() template?: DocT;
  @Input() cardStyle = `p8 m4 background-card card-outline-accent glow-accent rounded`;
  @Input() size = 380;
  @Output() view = new EventEmitter<DocT>();
  @Output() edit = new EventEmitter<DocT>();
  @Output() remove = new EventEmitter<DocT>();
  @Output() clone = new EventEmitter<DocT>();
  @Output() generate = new EventEmitter<DocT>();
}
