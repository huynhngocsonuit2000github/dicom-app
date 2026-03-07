import { TestBed } from '@angular/core/testing';

import { Study } from './study';

describe('Study', () => {
  let service: Study;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Study);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
