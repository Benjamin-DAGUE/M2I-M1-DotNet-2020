# Cours Service
La solution est composée de trois projets :
- `CoursService` : Projet de type `Service Windows (.NET Framework)`
- `CoursService.Client` : Projet de type `Console (.NET Framework)`
- `CoursService.Core` : Projet de type `Bibliothèque de classe (.NET Framework)`


Le projet `CoursService.Core` va contenir l'ensemble de la logique du service.
Il contient une classe `MonService` qui représente la logique du service.
`MonService` dispose donc des méthodes `Start` et `Stop` pour démarrer et arrêter le service.

Le projet `CoursService.Client` va être utilisé uniquement pendant la phase de développement pour tester la classe `MonService` et le comportement du service.

Le projet `CoursService` va être utilisé uniquement pour le déploiement du service.

## Interface graphique
Un service ne dispose pas d'interface graphique et ne permet donc pas d'intéragir avec l'utilisateur.
L'utilisation de `Console.WriteLine()` peut être utilisé à des fins de développement mais il est conseillé d'utiliser une compilation conditionnelle pour ne pas les appeler en `RELEASE` :

``` csharp
#if DEBUG
    Console.WriteLine("This is a log");
#else
    File.AppendAllText("C:\\log.txt", "This is a log" + Environment.NewLine);
#endif
```

## Installation d'un service

Pour installer un service, vous devez commencer par générer l'installeur :
- Dans le fichier `Service1.cs` > clic droit > `Ajouter le programme d'installation`
- Le fichier `ProjectInstaller.cs` est alors généré dans le projet et permet de configurer le projet (nom, description, utilisateur qui exécute le service, type de démarrage...)
- Après avoir configuré l'installateur, vous devez utiliser la commande `installutil` pour installer le service

> Attention, nécessite de lancer la console en Admin
```
cd C:\Windows\Microsoft.NET\Framework64\v4.0.30319
installutil "C:\Users\bdague\source\repos\M2I-M1-DotNet-2020\CoursService\CoursService\bin\Debug\CoursService.exe"
```

Pour désinscrire le service de Windows : 
> Attention, nécessite de lancer la console en Admin
```
cd C:\Windows\Microsoft.NET\Framework64\v4.0.30319
installutil /u "C:\Users\bdague\source\repos\M2I-M1-DotNet-2020\CoursService\CoursService\bin\Debug\CoursService.exe"
```

