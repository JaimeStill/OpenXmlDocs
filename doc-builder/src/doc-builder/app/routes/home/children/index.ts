import { Route } from '@angular/router';

import { DocumentsRoute } from './documents.route';
import { TemplatesRoute } from './templates.route';

export const HomeChildComponents = [
  DocumentsRoute,
  TemplatesRoute
];

export const HomeChildRoutes: Route[] = [
  { path: 'documents', component: DocumentsRoute },
  { path: 'templates', component: TemplatesRoute },
  { path: '', redirectTo: 'templates', pathMatch: 'prefix' },
  { path: '**', redirectTo: 'templates', pathMatch: 'prefix' }
];
