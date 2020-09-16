# Cours Service
La solution est compos�e de trois projets :
- `CoursService` : Projet de type `Service Windows (.NET Framework)`
- `CoursService.Client` : Projet de type `Console (.NET Framework)`
- `CoursService.Core` : Projet de type `Biblioth�que de classe (.NET Framework)`


Le projet `CoursService.Core` va contenir l'ensemble de la logique du service.
Il contient une classe `MonService` qui repr�sente la logique du service.
`MonService` dispose donc des m�thodes `Start` et `Stop` pour d�marrer et arr�ter le service.

Le projet `CoursService.Client` va �tre utilis� uniquement pendant la phase de d�veloppement pour tester la classe `MonService` et le comportement du service.

Le projet `CoursService` va �tre utilis� uniquement pour le d�ploiement du service.

## Interface graphique
Un service ne dispose pas d'interface graphique et ne permet donc pas d'int�ragir avec l'utilisateur.
L'utilisation de `Console.WriteLine()` peut �tre utilis� � des fins de d�veloppement mais il est conseill� d'utiliser une compilation conditionnelle pour ne pas les appeler en `RELEASE` :

``` csharp
#if DEBUG
    Console.WriteLine("This is a log");
#else
    File.AppendAllText("C:\\log.txt", "This is a log" + Environment.NewLine);
#endif
```

## Installation d'un service

Pour installer un service, vous devez commencer par g�n�rer l'installeur :
- Dans le fichier `Service1.cs` > clic droit > `Ajouter le programme d'installation`
- Le fichier `ProjectInstaller.cs` est alors g�n�r� dans le projet et permet de configurer le projet (nom, description, utilisateur qui ex�cute le service, type de d�marrage...)
- Apr�s avoir configur� l'installateur, vous devez utiliser la commande `installutil` pour installer le service

> Attention, n�cessite de lancer la console en Admin
```
cd C:\Windows\Microsoft.NET\Framework64\v4.0.30319
installutil "C:\Users\bdague\source\repos\M2I-M1-DotNet-2020\CoursService\CoursService\bin\Debug\CoursService.exe"
```

Pour d�sinscrire le service de Windows : 
> Attention, n�cessite de lancer la console en Admin
```
cd C:\Windows\Microsoft.NET\Framework64\v4.0.30319
installutil /u "C:\Users\bdague\source\repos\M2I-M1-DotNet-2020\CoursService\CoursService\bin\Debug\CoursService.exe"
```

