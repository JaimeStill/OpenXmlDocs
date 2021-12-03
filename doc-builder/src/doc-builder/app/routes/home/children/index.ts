import { Route } from '@angular/router';

import { TemplatesRoute } from './templates.route';

export const HomeChildComponents = [
  TemplatesRoute
];

export const HomeChildRoutes: Route[] = [
  { path: 'templates', component: TemplatesRoute },
  { path: '', redirectTo: 'templates', pathMatch: 'prefix' },
  { path: '**', redirectTo: 'templates', pathMatch: 'prefix' }
];
