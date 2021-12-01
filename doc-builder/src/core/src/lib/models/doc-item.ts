import { Doc } from './doc';
import { DocAnswer } from './doc-answer';
import { DocItemType } from './enums';
import { DocOption } from './doc-option';

export interface DocItem {
  id: number;
  docId: number;
  type: DocItemType;
  index: number;
  value: string;
  allowMultiple: boolean;
  isDropdown: boolean;

  answer?: DocAnswer;
  doc?: Doc;

  options: DocOption[];
}
