import { Routes } from '@angular/router';
import { GoodsPage } from './features/goods/pages/goods-page/goods-page';
import { GoodDetailsPage } from './features/goods/pages/good-details-page/good-details-page';

export const routes: Routes = [
  {
    path: 'goods',
    component: GoodsPage
  },
  {
    path: 'goods/:id',
    component: GoodDetailsPage
  }
];
