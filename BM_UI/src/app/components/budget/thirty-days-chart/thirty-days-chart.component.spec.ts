import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ThirtyDaysChartComponent } from './thirty-days-chart.component';

describe('ThirtyDaysChartComponent', () => {
  let component: ThirtyDaysChartComponent;
  let fixture: ComponentFixture<ThirtyDaysChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ThirtyDaysChartComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ThirtyDaysChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
