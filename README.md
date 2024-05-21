# CafeDuCoin

CafeDuCoin est une application web pour la location de jeux dans un café local. L'application se compose d'un webapi .NET et d'un frontend React, permettant aux utilisateurs de consulter les jeux disponibles, de les louer et de les retourner.

## Table des matières

- [Aperçu du projet](#aperçu-du-projet)
- [Fonctionnalités](#fonctionnalités)
- [Stack technologique](#stack-technologique)
- [Prérequis](#prérequis)
- [Installation](#installation)
- [Exécution du projet](#exécution-du-projet)
- [Points d'accès API](#points-daccès-api)
- [Configuration Docker](#configuration-docker)
- [Contribuer](#contribuer)
- [Licence](#licence)

## Aperçu du projet

CafeDuCoin est un service de location de jeux conçu pour être utilisé dans un cadre de café. Les utilisateurs peuvent consulter la liste des jeux disponibles, louer des jeux, retourner des jeux loués et consulter l'historique des locations de jeux spécifiques.

## Fonctionnalités

- Authentification et autorisation des utilisateurs
- Consulter les jeux disponibles
- Louer et retourner des jeux
- Consulter l'historique des locations pour chaque jeu
- Lister tous les jeux loués par l'utilisateur connecté

## Stack technologique

- **CafeDuCoin webapi**: .NET Core 8.0, Entity Framework Core, PostgreSQL
- **CafeDuCoin frontend**: React, Material-UI
- **Autres outils**: AutoMapper, JWT pour l'authentification, Docker

## Prérequis

- .NET SDK 8.0 ou ultérieur
- Node.js 16 ou ultérieur
- Base de données PostgreSQL
- Docker (facultatif, pour la conteneurisation)

## Installation

1. Cloner le dépôt :

```bash
git clone https://github.com/OthmaneAanaoui/CafeDuCoin.git
cd cafeducoin