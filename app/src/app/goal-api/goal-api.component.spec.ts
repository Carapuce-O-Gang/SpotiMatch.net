import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GoalApiComponent } from './goal-api.component';

describe('GoalApiComponent', () => {
  let component: GoalApiComponent;
  let fixture: ComponentFixture<GoalApiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GoalApiComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GoalApiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
