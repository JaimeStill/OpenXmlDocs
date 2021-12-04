import { Route } from '@angular/router';

import { EditRoute } from './edit.route';
import { ViewRoute } from './view.route';

export const TemplateComponents = [
  EditRoute,
  ViewRoute
];

export const TemplateRoutes: Route[] = [
  { path: 'template/:id/edit', component: EditRoute },
  { path: 'template/:id/view', component: ViewRoute }
];
