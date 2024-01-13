import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyNotSetComponent } from './company-not-set.component';

describe('CompanyNotSetComponent', () => {
  let component: CompanyNotSetComponent;
  let fixture: ComponentFixture<CompanyNotSetComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CompanyNotSetComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CompanyNotSetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
