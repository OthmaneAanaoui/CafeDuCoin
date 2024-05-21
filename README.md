 # CafeDuCoin

CafeDuCoin est une application web pour la location de jeux dans un café local. L'application se compose d'un backend Web API .NET et d'un frontend React, permettant aux utilisateurs de consulter les jeux disponibles, de les louer et de les retourner.

## Aperçu du projet

CafeDuCoin est un service de location de jeux conçu pour être utilisé dans un cadre de café. Les utilisateurs peuvent consulter la liste des jeux disponibles, louer des jeux, retourner des jeux loués et consulter l'historique des locations de jeux spécifiques.

## Fonctionnalités

- Authentification et autorisation des utilisateurs
- Consulter les jeux disponibles
- Louer et retourner des jeux
- Consulter l'historique des locations pour chaque jeu
- Lister tous les jeux loués par l'utilisateur connecté

## Stack technologique

- **Backend Web API** : .NET Core 8.0, Entity Framework Core, PostgreSQL
- **Frontend** : React, Material-UI
- **Autres outils** : AutoMapper, JWT pour l'authentification, Docker

## Prérequis

- .NET SDK 8.0 ou ultérieur
- Node.js 16 ou ultérieur
- Base de données PostgreSQL
- Docker (facultatif, pour la conteneurisation)

## Installation

1. **Cloner le dépôt :**

```bash
git clone https://github.com/OthmaneAanaoui/CafeDuCoin.git
cd cafeducoin
```

## Configuration du Web API :

```bash
dotnet restore
```

- Mettre à jour `appsettings.json` avec votre chaîne de connexion PostgreSQL.

```bash
dotnet ef database update
dotnet run
```

## Configuration du frontend :

```bash
cd frontend
npm install
npm start
```

- Ouvrir l'application dans votre navigateur : http://localhost:3000

## Points d'accès API 
- `POST /api/Auth/register` : s'enregistrer.
- `POST /api/Auth/login` : se connecter.
- `GET /api/Game/games` : Récupérer la liste de tous les jeux.
- `GET /api/Game/games/{id}/history` : Récupérer l'historique de location d'un jeu spécifique.
- `POST /api/Game/games/{id}/rent` : Louer un jeu spécifique.
- `POST /api/Game/games/{id}/return` : Retourner un jeu loué.
- `GET /api/Game/games/rented` : Lister tous les jeux loués par l'utilisateur connecté.

## Configuration Docker

### Construction et exécution avec Docker

```bash
docker build -t cafeducoin .
docker run -d -p 80:80 --name cafeducoin_container cafeducoin
```

### Vue d'ensemble du Dockerfile

Le Dockerfile est configuré pour gérer à la fois les builds backend et frontend :

- **Étape 1** : Construire le frontend avec Node.js.
- **Étape 2** : Construire le backend avec le SDK .NET.
- **Étape 3** : Combinaison du backend et du frontend, en copiant la build frontend dans le répertoire `wwwroot` du backend.

## Contribuer

Les contributions sont les bienvenues ! Veuillez créer une issue ou soumettre une pull request pour toute modification.

## Licence

Ce projet est sous licence MIT.