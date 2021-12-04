import { CoreComponents } from './core';
import { DocumentComponents } from './doc';
import { LayoutComponents } from './layout';

export const Components = [
  ...CoreComponents,
  ...DocumentComponents,
  ...LayoutComponents
];

export * from './core';
export * from './doc';
export * from './layout';
