import { Component, inject } from '@angular/core';
import { GoodsService } from '../../data-access/good.service';
import { AsyncPipe } from '@angular/common';

@Component({
  imports: [AsyncPipe],
  selector: 'app-goods-page',
  styleUrl: './goods-page.css',
  templateUrl: './goods-page.html',
})

export class GoodsPage {
  private readonly goodsService = inject(GoodsService);

  goods$ = this.goodsService.getAll();
}
