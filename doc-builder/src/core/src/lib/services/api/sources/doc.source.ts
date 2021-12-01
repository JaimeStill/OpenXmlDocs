import {
  Injectable,
  Optional
} from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { ApiQueryService } from '../../abstract';
import { ServerConfig } from '../../../config';
import { Doc } from '../../../models';

@Injectable()
export class DocSource extends ApiQueryService<Doc> {
  constructor(
    protected http: HttpClient,
    @Optional() private config: ServerConfig
  ) {
    super(http);
    this.sort = {
      isDescending: false,
      propertyName: 'name'
    };
  }

  setUrl = (url: string) => this.baseUrl = `${this.config.api}doc/${url}`;
}
