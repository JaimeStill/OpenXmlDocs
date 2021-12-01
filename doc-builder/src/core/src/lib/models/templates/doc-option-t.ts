import { DocItemT } from './doc-item-t';

export interface DocOptionT {
  id: number;
  docItemTId: number;
  value: string;

  docItemT?: DocItemT;
}
