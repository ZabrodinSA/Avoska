import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Good } from '../models/good';

@Injectable({
  providedIn: 'root'
})

export class GoodsService {
  private readonly http = inject(HttpClient);

  private readonly apiUrl = 'http://localhost:5555/goods';

  getAll(): Observable<Good[]> {
    return this.http.get<Good[]>(this.apiUrl);
  }
}
