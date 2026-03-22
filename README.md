    Présentation du projet

Ce projet est un simulateur de gestion de zoo développé en C#.
Le joueur doit gérer un zoo en prenant en compte :

le budget
les animaux
les habitats
la nourriture
les maladies
la reproduction
les visiteurs
les événements aléatoires

    Démarche de développement

Au début du projet, j’ai réalisé une grande partie des User Stories ainsi qu’un diagramme UML pour structurer le fonctionnement du jeu.

Cependant, je n’avais pas totalement terminé le diagramme UML et il me manquait certaines User Stories.
En avançant dans le développement du code, cela m’a permis de mieux comprendre les liens entre les User Stories et le diagramme UML.

Comme mentionné précédemment, j’ai passé une grande partie du projet sur la gestion. À un moment, j’étais bloqué : les liens entre l’énoncé, les User Stories et le diagramme UML me semblaient confus.

J’ai donc décidé de commencer le développement du code, ce qui m’a permis de mieux comprendre le projet et d’affiner progressivement la conception.

    Structure du projet
Mon projet est organisé en plusieurs parties :
Dossier Enums :
Contient les énumérations utilisées dans le projet :

Saison
Sexe
TypeNourriture

Le dossier Models :
Contient les classes principales du jeu :

Zoo 
Animal 
Tigre, Aigle, Poule, Coq 
Habitat 
StockNourriture 

Le dossier Services : 
Contient les systèmes du jeu :

Visiteur 
Reproduction 
Subvention
EvenementAleatoire 

et le fichier program.cs qui est le point d'entrée du prgramme 

    Outils utilisés : 
C# / .NET
Visual Studio / VS Code
Draw.io (diagramme UML)
Google Sheets (User Stories)

    Gestion du projet (GitHub) :
Le projet a été réalisé seul.
J’ai donc choisi une organisation simple :

Peu de branches utilisées
Travail principalement sur une branche principale
lien github : https://github.com/yaniscalo36-commits/zoo-simulator.git

    Fonctionnalités principales : 
Gestion du budget
Achat/vente d’animaux
Gestion des habitats
Système de nourriture
Reproduction
Maladies
Événements aléatoires
Système de visiteurs avec revenus

    Contenu du rendu : 
Code source du projet
Diagramme UML (Draw.io)
User Stories (Google Sheets)

    Conclusion

Ce projet n’a pas été un projet comme les autres réalisés au cours de l’année.
En effet, l’énoncé se rapproche davantage d’une situation réelle, contrairement aux exercices habituels qui sont souvent guidés par des questions.

J’ai dû réfléchir de manière autonome à l’organisation du projet, à la structure du code et aux fonctionnalités à mettre en place.

La partie la plus difficile et la plus longue a été la gestion du projet. Cependant, cela m’a appris à poser des bases solides avant de commencer à coder. En prenant le temps de réfléchir à une bonne organisation, le développement du code devient ensuite plus simple et plus logique.

Enfin, travailler seul sur ce projet m’a permis de mieux apprendre et de réellement comprendre ce que je faisais, même si cela a été parfois difficile et a demandé du temps.


