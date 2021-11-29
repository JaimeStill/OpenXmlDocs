import {
  ModuleWithProviders,
  NgModule
} from '@angular/core';

import { CommonModule } from '@angular/common';
import { MaterialModule } from './material.module';

import { Components } from './components';
import { Dialogs } from './dialogs';
import { Directives } from './directives';
import { Pipes } from './pipes';

import { ServerConfig } from './config';

@NgModule({
  declarations: [
    ...Components,
    ...Dialogs,
    ...Directives,
    ...Pipes
  ],
  imports: [
    CommonModule,
    MaterialModule
  ],
  exports: [
    ...Components,
    ...Dialogs,
    ...Directives,
    ...Pipes,
    MaterialModule
  ]
})
export class CoreModule {
  static forRoot(config: ServerConfig): ModuleWithProviders<CoreModule> {
    return {
      ngModule: CoreModule,
      providers: [
        { provide: ServerConfig, useValue: config }
      ]
    }
  }
}
