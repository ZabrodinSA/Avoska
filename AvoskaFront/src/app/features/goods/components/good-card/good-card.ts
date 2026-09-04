import {Component, input} from '@angular/core';
import { Good } from '../../models/good';

@Component({
  selector: 'app-good-card',
  templateUrl: './good-card.html',
  standalone: true,
  styleUrl: './good-card.css'
})

export class GoodCard {
  good = input.required<Good>();
}
