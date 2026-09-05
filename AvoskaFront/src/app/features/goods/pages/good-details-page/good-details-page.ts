import { Component, inject } from '@angular/core';
import { AsyncPipe } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { GoodsService } from '../../data-access/good.service';
@Component({
  imports: [AsyncPipe],
  selector: 'app-good-details-page',
  styleUrl: './good-details-page.css',
  templateUrl: './good-details-page.html',
  standalone: true
})

export class GoodDetailsPage {
  private readonly route = inject(ActivatedRoute);
  private readonly goodsService = inject(GoodsService);

  id = this.route.snapshot.paramMap.get('id');

  good$ = this.id
    ? this.goodsService.getById(this.id)
    : null;
}
