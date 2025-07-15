import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Article } from '../../domain/models/article.model';
import { ArticleCreateRequest } from '../../domain/models/article-create-request.model'; 
import { ArticleServiceInterface } from '../../application/ports/article-service.interface';
@Injectable({
  providedIn: 'root',
})

@Injectable({
  providedIn: 'root'
})
export class ArticleService implements ArticleServiceInterface {
  private apiUrl = 'http://localhost:8080/api/article'; 

  constructor(private http: HttpClient) {}

  getArticles(): Observable<Article[]> {
    return this.http.get<Article[]>(this.apiUrl);
  }

  getArticleByReference(reference: string): Observable<Article> {
    return this.http.get<Article>(`${this.apiUrl}/${reference}`);
  }

  addArticle(article: ArticleCreateRequest): Observable<any> {
    return this.http.post<any>(this.apiUrl, article);
  }
  updateArticle(reference: string, article: Article): Observable<Article> {
    return this.http.put<Article>(`${this.apiUrl}/${reference}`, article);
  }

  deleteArticle(reference: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${reference}`);
  }

  searchArticlesByName(name: string): Observable<Article[]> {
    return this.http.get<Article[]>(`${this.apiUrl}?nom_like=${name}`);
  }

  searchArticlesByPrice(min: number, max: number): Observable<Article[]> {
    return this.http.get<Article[]>(`${this.apiUrl}?prixHT_gte=${min}&prixHT_lte=${max}`);
  }
}
