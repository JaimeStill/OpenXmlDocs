import { Route } from '@angular/router';

import { EditRoute } from './edit.route';
import { FillRoute } from './fill.route';
import { ViewRoute } from './view.route';

export const DocumentComponents = [
  EditRoute,
  FillRoute,
  ViewRoute
];

export const DocumentRoutes: Route[] = [
  { path: 'document/:id/edit', component: EditRoute },
  { path: 'document/:id/fill', component: FillRoute },
  { path: 'document/:id/view', component: ViewRoute }
]
