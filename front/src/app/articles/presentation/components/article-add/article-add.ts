import { Component } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { FormsModule } from '@angular/forms';   
import { ArticleService } from '../../../infrastructure/repositories/article.service'; 
import { ArticleCreateRequest, TypeArticleDTOs } from '../../../domain/models/article-create-request.model'; 

@Component({
  selector: 'app-article-add',
  standalone: true,   
  imports: [CommonModule, FormsModule],
  templateUrl: './article-add.html',
  styleUrls: ['./article-add.scss']
})
export class ArticleAddComponent {
  typesArticle = TypeArticleDTOs;
  article: ArticleCreateRequest = {
    type: TypeArticleDTOs.NonConsomable,
    name: '',
    price: 0.0,
    storage: null,
    canTakeAway: false
  };

  constructor(private articleService: ArticleService) {}

  onTypeChange(event: Event): void {
    const selectedValue = (event.target as HTMLSelectElement).value;
    this.article.type = parseInt(selectedValue, 10); 
  }
  onSubmit(form: any): void {
    if (form.valid) {
      this.articleService.addArticle(this.article).subscribe(
        (response) => {
          form.reset(); 
        },
        (error) => {
          console.error('Erreur :', error);
        }
      );
    } else {
      console.log('Invalide forms');
    }
  }
}
