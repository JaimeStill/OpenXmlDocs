import { DocT } from './doc-t';
import { DocItemType } from '../enums';
import { DocOptionT } from './doc-option-t';

export interface DocItemT {
  id: number;
  docTId: number;
  type: DocItemType;
  index: number;
  value: string;
  allowMultiple: boolean;
  isDropdown: boolean;

  docT?: DocT;

  options: DocOptionT[];
}
