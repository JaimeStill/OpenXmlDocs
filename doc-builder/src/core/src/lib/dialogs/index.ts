import { ConfirmDialog } from './confirm';
import { DocumentDialogs } from './doc';

export const Dialogs = [
  ConfirmDialog,
  ...DocumentDialogs
];

export * from './confirm';
export * from './doc';
