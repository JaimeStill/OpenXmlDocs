import {
  Injectable,
  Optional
} from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { ApiQueryService } from '../../abstract';
import { ServerConfig } from '../../../config';
import { DocCategory } from '../../../models';

@Injectable()
export class DocCategorySource extends ApiQueryService<DocCategory> {
  constructor(
    protected http: HttpClient,
    @Optional() private config: ServerConfig
  ) {
    super(http);
    this.sort = {
      isDescending: false,
      propertyName: 'value'
    };
  }

  setUrl = (url: string) => this.baseUrl = `${this.config.api}doc/${url}`;
}
