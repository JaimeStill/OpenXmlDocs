import { DocItem } from './doc-item';

export interface DocAnswer {
  id: number;
  docItemId: number;
  value: string;

  docItem?: DocItem;
}
