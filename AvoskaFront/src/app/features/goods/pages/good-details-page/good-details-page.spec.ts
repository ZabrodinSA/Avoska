import { ComponentFixture, TestBed } from '@angular/core/testing';
import { GoodDetailsPage } from './good-details-page';

describe('GoodDetailsPage', () => {
  let component: GoodDetailsPage;
  let fixture: ComponentFixture<GoodDetailsPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GoodDetailsPage],
    }).compileComponents();

    fixture = TestBed.createComponent(GoodDetailsPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
