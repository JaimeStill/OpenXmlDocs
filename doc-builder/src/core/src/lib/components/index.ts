import { CoreComponents } from './core';
import { LayoutComponents } from './layout';
import { TemplateComponents } from './template';

export const Components = [
  ...CoreComponents,
  ...LayoutComponents,
  ...TemplateComponents
];

export * from './core';
export * from './layout';
export * from './template';
