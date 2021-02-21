import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicTableComponent } from './public-table.component';

describe('PublicTableComponent', () => {
  let component: PublicTableComponent;
  let fixture: ComponentFixture<PublicTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PublicTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PublicTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
