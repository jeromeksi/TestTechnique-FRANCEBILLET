import { Observable } from 'rxjs';
import { Article } from '../../domain/models/article.model';  
import { ArticleCreateRequest } from '../../domain/models/article-create-request.model'; 

export interface ArticleServiceInterface {
  getArticles(): Observable<Article[]>;
  getArticleByReference(reference: string): Observable<Article>;
  addArticle(article: ArticleCreateRequest): Observable<any>;
  updateArticle(reference: string, article: Article): Observable<Article>;
  deleteArticle(reference: string): Observable<void>;
  searchArticlesByName(name: string): Observable<Article[]>;
  searchArticlesByPrice(min: number, max: number): Observable<Article[]>;
}
