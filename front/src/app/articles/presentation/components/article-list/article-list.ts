import { Component, OnInit } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { ArticleService } from '../../../infrastructure/repositories/article.service'; 
import { Article } from '../../../domain/models/article.model'; 

import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-article-list',
  standalone: true,
  imports: [CommonModule, HttpClientModule],
  templateUrl: './article-list.html',
  styleUrls: ['./article-list.scss']
})
export class ArticleListComponent implements OnInit {
  articles: Article[] = [];

  constructor(private articleService: ArticleService) {}

  ngOnInit(): void {
    this.articleService.getArticles().subscribe((data) => {
      this.articles = data;
    });
  }

  deleteArticle(reference: string): void {
    this.articleService.deleteArticle(reference).subscribe(() => {
      this.articles = this.articles.filter((article) => article.reference !== reference);
    });
  }
}
