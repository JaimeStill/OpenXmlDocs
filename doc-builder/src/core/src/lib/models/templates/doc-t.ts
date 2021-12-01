import { DocCategory } from '../doc-category';
import { DocItemT } from './doc-item-t';

export interface DocT {
  id: number;
  categoryId: number;
  name: string;
  description?: string;

  category?: DocCategory;

  items: DocItemT[];
}
