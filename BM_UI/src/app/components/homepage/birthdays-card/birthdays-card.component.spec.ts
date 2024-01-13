import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BirthdaysCardComponent } from './birthdays-card.component';

describe('BirthdaysCardComponent', () => {
  let component: BirthdaysCardComponent;
  let fixture: ComponentFixture<BirthdaysCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BirthdaysCardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BirthdaysCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
