import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Studies } from './studies';

describe('Studies', () => {
  let component: Studies;
  let fixture: ComponentFixture<Studies>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Studies]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Studies);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
