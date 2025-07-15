import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArticleSearch } from './article-search';

describe('ArticleSearch', () => {
  let component: ArticleSearch;
  let fixture: ComponentFixture<ArticleSearch>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ArticleSearch]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ArticleSearch);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
