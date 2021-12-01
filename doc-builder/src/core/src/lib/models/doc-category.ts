import { Doc } from './doc';
import { DocT } from './templates/doc-t';

export interface DocCategory {
  id: number;
  value: string;

  docs: Doc[];
  docTs: DocT[];
}
