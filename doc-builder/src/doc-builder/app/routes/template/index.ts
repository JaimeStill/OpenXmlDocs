import { Route } from '@angular/router';

import { EditRoute } from './edit.route';
import { NewRoute } from './new.route';
import { ViewRoute } from './view.route';

export const TemplateComponents = [
  EditRoute,
  NewRoute,
  ViewRoute
];

export const TemplateRoutes: Route[] = [
  { path: 'template/new', component: NewRoute },
  { path: 'template/:id/edit', component: EditRoute },
  { path: 'template/:id/view', component: ViewRoute }
];
