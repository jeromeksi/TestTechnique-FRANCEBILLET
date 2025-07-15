import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterModule],
  template: `
    <nav>
      <a routerLink="/articles">Liste des articles</a> | 
      <a routerLink="/add">Ajouter un article</a> | 
      <a routerLink="/search">Rechercher un article</a>
    </nav>
    <router-outlet></router-outlet>
  `,
  styleUrls: ['./app.component.scss']
})
export class AppComponent {}
