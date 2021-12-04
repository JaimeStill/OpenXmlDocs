import {
  Component,
  Input
} from '@angular/core';

import {
  Doc,
  DocT
} from '../../models';

@Component({
  selector: 'doc-card',
  templateUrl: 'doc-card.component.html'
})
export class DocCardComponent {
  @Input() doc?: Doc | DocT;
  @Input() cardStyle = `p8 m4 background-card card-outline-accent glow-accent rounded`;
  @Input() size = 380;
}
