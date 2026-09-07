import { Component, inject } from '@angular/core';
import { GoodsService } from '../../data-access/good.service';
import { AsyncPipe } from '@angular/common';
import { GoodCard } from '../../components/good-card/good-card';
import { CatalogService } from '../../../catalog/data-access/catalog.service';

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

  goods$ = this.goodsService.getAll();
  categories$ = this.catalogService.getAll();
}
