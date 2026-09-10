import { Component, inject, signal } from '@angular/core';
import { GoodsService } from '../../data-access/good.service';
import { AsyncPipe } from '@angular/common';
import { GoodCard } from '../../components/good-card/good-card';
import { CatalogService } from '../../../catalog/data-access/catalog.service';
import {
  catchError,
  combineLatest,
  debounceTime,
  distinctUntilChanged,
  map,
  Observable,
  of,
  startWith,
  switchMap
} from 'rxjs';
import {ActivatedRoute, Router} from '@angular/router';
import {toObservable, toSignal} from '@angular/core/rxjs-interop';
import {Good} from '../../models/good';

type GoodsState =
  | { status: 'loading' }
  | { status: 'success'; goods: Good[] }
  | { status: 'error'; message: string };

@Component({
  imports: [AsyncPipe, GoodCard],
  selector: 'app-goods-page',
  styleUrl: './goods-page.css',
  templateUrl: './goods-page.html',
  standalone: true
})

export class GoodsPage {
  private readonly goodsService = inject(GoodsService);
  private readonly catalogService = inject(CatalogService);

  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);

  searchText = signal('');
  private readonly searchText$ = toObservable(this.searchText).pipe(
    debounceTime(300),
    map(text => text.trim()),
    distinctUntilChanged()
  );

  categories$ = this.catalogService.getAll();
  private readonly category$ = this.route.queryParamMap.pipe(
    map(params => params.get('category'))
  );

  selectedCategory = toSignal(
    this.route.queryParamMap.pipe(
      map(params => params.get('category'))
    ),
    {
      initialValue: null
    }
  );

  goodsState$: Observable<GoodsState> = combineLatest([
    this.category$,
    this.searchText$
  ]).pipe(
    switchMap(([categoryName, searchText]) => {
      let request$: Observable<Good[]>;

      if (searchText) {
        request$ = this.goodsService.searchByName(searchText);
      } else if (categoryName) {
        request$ = this.goodsService.getByCategory(categoryName);
      } else {
        request$ = this.goodsService.getAll();
      }

      return request$.pipe(
        map(goods => ({
          status: 'success',
          goods
        }) as GoodsState),

        catchError(() =>
          of<GoodsState>({
            status: 'error',
            message: 'Failed to load goods'
          })
        ),

        startWith<GoodsState>({
          status: 'loading'
        })
      );
    })
  );

  selectCategory(categoryName: string | null) {
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: {
        category: categoryName
      }
    });
  }

  onSearch(event: Event) {
    const input = event.target as HTMLInputElement;
    this.searchText.set(input.value);
  }
}
