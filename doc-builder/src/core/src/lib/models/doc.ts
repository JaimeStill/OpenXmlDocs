import { DocCategory } from './doc-category';
import { DocItem } from './doc-item';

export interface Doc {
  id: number;
  categoryId: number;
  name: string;
  description?: string;

  category?: DocCategory;

  items: DocItem[];
}
