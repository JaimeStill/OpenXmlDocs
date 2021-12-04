import { CoreComponents } from './core';
import { DocumentComponents } from './document';
import { LayoutComponents } from './layout';
import { TemplateComponents } from './template';

export const Components = [
  ...CoreComponents,
  ...DocumentComponents,
  ...LayoutComponents,
  ...TemplateComponents
];

export * from './core';
export * from './document';
export * from './layout';
export * from './template';
