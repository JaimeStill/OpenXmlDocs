import { Route } from '@angular/router';

import {
  DocumentComponents,
  DocumentRoutes
} from './document';

import {
  HomeComponents,
  HomeRoutes
} from './home';

import {
  TemplateComponents,
  TemplateRoutes
} from './template';

export const RouteComponents = [
  ...DocumentComponents,
  ...HomeComponents,
  ...TemplateComponents
]

export const Routes: Route[] = [
  ...DocumentRoutes,
  ...HomeRoutes,
  ...TemplateRoutes,
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', redirectTo: '', pathMatch: 'full' }
]
