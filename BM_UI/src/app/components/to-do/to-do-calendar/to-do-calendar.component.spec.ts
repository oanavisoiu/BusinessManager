import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ToDoCalendarComponent } from './to-do-calendar.component';

describe('ToDoCalendarComponent', () => {
  let component: ToDoCalendarComponent;
  let fixture: ComponentFixture<ToDoCalendarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ToDoCalendarComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ToDoCalendarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
