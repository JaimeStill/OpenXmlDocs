import { DocItem } from './doc-item';

export interface DocOption {
  id: number;
  docItemId: number;
  value: string;
  selected: boolean;

  docItem?: DocItem;
}
