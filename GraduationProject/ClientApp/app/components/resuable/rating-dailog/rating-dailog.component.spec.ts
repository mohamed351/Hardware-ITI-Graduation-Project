import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RatingDailogComponent } from './rating-dailog.component';

describe('RatingDailogComponent', () => {
  let component: RatingDailogComponent;
  let fixture: ComponentFixture<RatingDailogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RatingDailogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RatingDailogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
