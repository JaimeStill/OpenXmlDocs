import { Route } from '@angular/router';

import { HomeRoute } from './home.route';

import {
  HomeChildComponents,
  HomeChildRoutes
} from './children';

export const HomeComponents = [
  HomeRoute,
  ...HomeChildComponents
];

export const HomeRoutes: Route[] = [
  { path: 'home', component: HomeRoute, children: HomeChildRoutes }
];
