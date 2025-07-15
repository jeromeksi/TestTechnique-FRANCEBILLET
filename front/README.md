## Frontend

### Notes

Étant moins expérimenté avec Angular, j'ai développé application simple, avec un style _"minimaliste"_, permettant de :
- lister les articles,
- en ajouter via un formulaire,
- les supprimer par référence.

> Beaucoup d'améliorations possibles :
> - _Lazy loading_
> - _Gestion structurée des erreurs HTTP_
> - _Nouvelles fonctionnalités_

---

### Architecture

Le frontend suit également une structure inspirée du DDD, semblable à celle du backend.

---

### Fonctionnalités implémentées

- Affichage de la liste des articles (via l’API backend)
- Suppression d’un article par référence
- Ajout d’un nouvel article

---

### Style

Le style reposse sur des CSS simples, générés ou issus de templates existants.

---

### Lancement en local

Depuis le dossier `front` :

```
npm install
ng serve --port 4200
```

Accès à l'application :  
[http://localhost:4200](http://localhost:4200)

> Le frontend communique avec le backend à l'adresse `http://localhost:8080`, sans configuration dynamique.

---

### Dockerisation

Un **Dockerfile** permet de containeriser le frontend.  
Depuis le dossier `front` :

```
docker build -t stockfront .
docker run -p 4200:80 stockfront
```

Accès à l'application :  
[http://localhost:4200](http://localhost:4200)

> L’adresse du backend est en dur : `http://localhost:8080`.
