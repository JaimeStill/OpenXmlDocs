import {
  Component,
  Input,
  Output,
  EventEmitter
} from '@angular/core';

import { Doc } from '../../models';

@Component({
  selector: 'doc-card',
  templateUrl: 'doc-card.component.html'
})
export class DocCardComponent {
  @Input() doc?: Doc;
  @Input() cardStyle = `p8 m4 background-card card-outline-accent glow-accent rounded`;
  @Input() size = 380;
  @Output() view = new EventEmitter<Doc>();
  @Output() edit = new EventEmitter<Doc>();
  @Output() fill = new EventEmitter<Doc>();
  @Output() remove = new EventEmitter<Doc>();
  @Output() clone = new EventEmitter<Doc>();
}
