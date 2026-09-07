import { Component, inject } from '@angular/core';
import { AsyncPipe } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { GoodsService } from '../../data-access/good.service';
import { switchMap } from 'rxjs';

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

  good$ = this.route.paramMap.pipe(
    switchMap(params => {
      const id = params.get('id');

      if (!id) {
        throw new Error('Good id is missing');
      }

      return this.goodsService.getById(id);
    })
  );
}
