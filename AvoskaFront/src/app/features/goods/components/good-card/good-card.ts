import {Component, input} from '@angular/core';
import { Good } from '../../models/good';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-good-card',
  templateUrl: './good-card.html',
  standalone: true,
  imports: [
    RouterLink
  ],
  styleUrl: './good-card.css'
})

export class GoodCard {
  good = input.required<Good>();
}
