## Backend

### Architecture

L'architecture suit les principes du Domain-Driven Design (DDD).  
L'application est structurée en quatre couches principales : **Domain**, **Application**, **Infrastructure** et **WebAPI**.

Le domaine métier est représenté par `Article`. Chaque couche est isolée selon son niveau d’abstraction afin de respecter le principe d’inversion des dépendances. Le **domaine ne dépend d’aucune technologie** ; les implémentations concrètes (ex. : accès à la base de données) sont injectées.

---
### Test unitaire

J'ai mis en place 4 tests unitaire vérifier des règles métier.

- Verification du refut de prix négatif
- Verification du refut du stockage négatif
- Verification du refut du nom déjà existant 
- Verification de la creation d'un article correct

Pour lancer les tests sans visual studio, se placer dans `back/src` : 

> dotnet test ./StockBack.Test

---

### Fonctionnalités implémentées

- Récuperation de la liste de tout les `Articles`
- Récuperation d'un `Articles` par **reference**
- Suppression d’un `Articles` par **référence**
- Ajout d’un nouvel `Articles`
- Modification de la valeur du stock (_storage_) d’un `Articles` par **référence**
- Récuperation de la liste des `Articles` qui ont un prix HT entre un **min** et **max**

---

### Dockerisation & Base de données

- J'ai crée **Dockerfile** pour containeriser le service Web.
- Un **docker-compose** facilite le **debug** et le **développement multi-environnement**.

Le `docker-compose` comprend :
- La **Web API**, construite à partir du `Dockerfile`, exposée sur le port **8080** ;
- Une **base de données PostgreSQL**, exposée sur le port **5432** _(utile qu'a des fins de test)_.

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
