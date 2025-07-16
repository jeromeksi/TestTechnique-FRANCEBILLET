# Test Technique - FRANCEBILLET

## Backend
### Notes
Pour ce test technique je n'ai pas mis de controle d'identité sur l'API mais il est évident que pour une application en production un controle sur les différents endpoint de l'API est indispensable.

### Architecture

L'application est en .NET 8.0, et suit les principes du Domain-Driven Design (DDD).
Elle est structurée en quatre couches principales : **Domain**, **Application**, **Infrastructure** et **WebAPI**.

Le domaine métier est représenté par `Article`. Chaque couche est isolée selon son niveau d’abstraction afin de respecter le principe d’inversion des dépendances. Le **domaine ne dépend d’aucune technologie** ; les implémentations concrètes (ex. : accès à la base de données) sont injectées.

L'application fonctionne avec une base de données PostgreSQL (fournis dans le docker-compose). J'utilise le micro ORM Dapper pour effectuer les request SQL.
J'ai implémenté une gestion des exceptions simple basé sur des règles métiers fictive.

---

### Fonctionnalités implémentées
> On peux retrouver les endpoints via un swagger.

- Récuperation de la liste de tout les `Articles` 
- Récuperation d'un `Articles` par **reference**
- Suppression d’un `Articles` par **référence**
- Ajout d’un nouvel `Articles`
- Modification de la valeur du stock (_storage_) d’un `Articles` par **référence**
- Récuperation de la liste des `Articles` qui ont un prix HT entre un **min** et **max**

---

### Dockerisation & Base de données

- J'ai crée un **Dockerfile** pour containeriser le service WebApi.
- Un **docker-compose** facilite le **debug** et le **développement multi-environnement**.

Le `docker-compose` comprend :
- La **Web API**, construite à partir du `Dockerfile`, exposée sur le port **8080** ;
- Une **base de données PostgreSQL** exposée sur le port **5432** _(utile qu'a des fins de test)_.

Cette configuration permet de **travailler facilement sur Linux et Windows**, et constitue une base pour un futur **déploiement avec Helm**.

---

### Lancement en local

Se placer dans le dossier `back/src`.

- **Avec Docker Compose** :  
  ```
  docker compose -p stockback up
  ```

- **Avec Visual Studio 2022** :  
  Définir `docker-compose` comme projet de démarrage, puis lancer avec `F5` _(le debugging en pas à pas est fonctionnel)_.

- **Sous Linux avec Rider** : fonctionne également.

- **Swagger** :  
  [http://localhost:8080/swagger/index.html](http://localhost:8080/swagger/index.html)

- **API** :  
  [http://localhost:8080/api/article](http://localhost:8080/api/article)

> Le port d’exposition de l’API peut être modifié dans le fichier `docker-compose.yml` mais doit-être reporter dans le frontend.

#### Base de données

Un script `init.sql` dans `back/src` initialise la base de données (automatique avec le docker-compose):  
- Création de la base et de la table `articles`  
- Insertion de fausse données

---

### Déploiement avec Minikube (Kubernetes)

**Pré-requis** : `minikube` et `helm` installés.


1. Build l'image du backend, se placer dans le dossier `back/src`:  
   ```
   docker build -f ./WebAPI/Dockerfile -t stockwebapi .
   ```

2. Lancer Minikube :  
   ```
   minikube start
   ```

3. Ajouter l'image `stockwebapi` dans la registery de minikube:  
   ```
   minikube image load stockwebapi:latest
   ```

4. Se placer dans le dossier `deploy` et installer le chart Helm dans le kubernetes :  
   ```
   helm install stockback .
   ```

5. Deux services devraient être créés.

6. Faire un port-forward :  
   ```
   kubectl port-forward deployment/stockback-api 8080:8080
   ```
_(le swagger n'est pas accéssible car le build de la web api est en RELEASE, il faut lui passer le **BUILD_CONFIGURATION**=DEVELOPPEMENT si besoin)_

---

## Frontend

### Notes

Étant moins expérimenté avec Angular, j'ai développé application simple, avec un style _" très minimaliste"_, permettant de :
- lister les articles,
- en ajouter via un formulaire,
- les supprimer par référence.

> Beaucoup d'améliorations possibles :
> - _Lazy loading_
> - _Gestion structurée des erreurs HTTP_
> - _Nouvelles fonctionnalités_
> - Un style plus moderne et efficace ne serait pas désagréable

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

J'ai fait un  **Dockerfile** permettant de containeriser le frontend.  
Depuis le dossier `front` :

```
docker build -t stockfront .
docker run -p 4200:80 stockfront
```

Accès à l'application :  
[http://localhost:4200](http://localhost:4200)

> L’adresse du backend est en dur et doit-être : `http://localhost:8080`.
