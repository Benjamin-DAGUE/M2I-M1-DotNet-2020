# Introduction
Bank Manager est une application de gestion de comptes bancaire.
Cette application est développé en `WPF`, son architecture repose sur les principes du modèles `MVVM`.

`Cours.WPF.BankManager` est une application `WPF net core 3.0`.

# Dépendances
- [Mahapps.Metro](https://github.com/MahApps/MahApps.Metro) : Librairie graphique `WPF`.
- [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json) : Librairie de sérialisation au format `JSON`.
- [Microsoft.Extensions.DependencyInjection](https://github.com/aspnet/Extensions) : Librairie d'injection de dépendance.
- CoursWPF.MVVM : Librairie `MVVM` pour `WPF` développée lors du cours.

# Architecture
L'architecture du projet repose sur les principes du modèles `MVVM`.
Le modèle définit trois couches :
- **Modèle** : Logique métier et données de l'application développée en langage `C#`
- **Vue** : Interface utilisateur développée en langage `XAML` et `C#`
- **Vue-modèle** : Logique de présentation développée en langage `C#`

## Modèle
En théorie, le modèle `MVVM` découpe la partie modèle en trois partie :
- **Modèle de présentation** : Classe de données compatible avec le système de binding. Prend en charge des éléments de présentation comme par exemple des propriétés calculées ou la validation de données.
- **Modèle de données** : Reflète le système de stockage de données.
- **Data Store** : Magasin de données.

### DataStore de l'application
L'application utilise un fichier de données unique au format `JSON`, il représente le `DataStore`.

### Modèle de données de l'application
Le modèle de données utilisé par l'application est le suivant :

- **`BankAcount` : Représente un compte bancaire**
  - `Identifier` : Identifiant unique d'un enregistrement
  - `Name` : Nom du compte bancaire
  - `Number` : Numéro du compte bancaire
  - `BankAccountLines` : Instances des écritures bancaire associées au compte
- **`BankAccountLine` : Représente une ligne d'écriture dans un compte bancaire**
  - `Identifier` : Identifiant unique d'un enregistrement
  - `IdentifierBankAccount` : Identifiant du compte bancaire associé
  - `IdentifierCategory` : Identifiant de la catégorie associée utilisée pour classer l'écriture
  - `BankAccount` : Instance du compte bancaire associée
  - `Category` : Instance de la catégorie associée utilisée pour classer l'écriture
  - `Label` : Libellé de l'écriture
  - `Value` : Montant de l'écriture
  - `Date` : Date de l'écriture
- **`Category` : Représente une catégorie de classement des écritures bancaire**
  - `Identifier` : Identifiant unique d'un enregistrement
  - `Name` : Nom de la catégorie
  - `BankAccountLines` : Instance des écritures bancaires associées au compte.

### Implémentation du modèle

La logique `MVVM` voudrait que le modèle de présentation et le modèle logique soit liés avec une dépendance faible.
L'application ne fait pas de réèl distinction entre le modèle de données et le modèle global.

Le modèle de l'application est défini dans le répertoire `.\Models\`.
Le modèle de l'application ne définit pas de couche d'abstraction (interfaces).

Les classes du modèle s'appuies sur la ligne d'héritage suivante :

- `System.Object`
  - `CoursWPF.MVVM.ObservableObject` : `CoursWPF.MVVM.Abstracts.IObservableObject` (Objet compatible avec le moteur de `Binding`)
    - `CoursWPF.MVVM.Models.Entity` : `CoursWPF.MVVM.Models.Abstracts.IEntity` (Objet observable avec une propriété `Identifier`)
      - `CoursWPF.BankManager.Models.XXX` (Objet du modèle de l'application)

Chaque classe du modèle implement l'interface `System.CompnentModel.IEditableObject` qui définie trois méthodes :
- `void BeginEdit();`
- `void EndEdit();`
- `void CancelEdit();`

> L'interface `System.CompnentModel.IEditableObject` est utilisée par la `DataGrid` pour gérer l'édition en ligne avec validation ou annulation.

Pour implémenter cette interface, chaque classe du modèle est construite en déclarant une structure de données sous-jacente.

Par exemple :

``` csharp
public class BankAccount : Entity
{
    #region Fields

    private struct BankAccountData //Structure sous-jacente
    {

        public string Name { get; set; }
        public string Number { get; set; }
        public ObservableCollection<BankAccountLine> BankAccountLines { get; set; }
    }

    BankAccountData _CurrentData; //Données actuelles
    BankAccountData? _BackupData; //Données sauvegardées lors que la méthode BeginEdit est appelée.

    #endregion

    #region Properties
        
    /*Les propriétés exposent les données de la structure de données sous-jacente.*/

    public string Name 
    { 
        get => this._CurrentData.Name;
        set => this.SetProperty(nameof(this.Name), () => this._CurrentData.Name, (v) => this._CurrentData.Name = v, value);
    }

    public string Number
    {
        get => this._CurrentData.Number;
        set => this.SetProperty(nameof(this.Number), () => this._CurrentData.Number, (v) => this._CurrentData.Number = v, value);
    }

    public ObservableCollection<BankAccountLine> BankAccountLines
    {
        get => this._CurrentData.BankAccountLines;
        private set => this.SetProperty(nameof(this.BankAccountLines), () => this._CurrentData.BankAccountLines, (v) => this._CurrentData.BankAccountLines = v, value);
    }

    #endregion

    #region Methods

    public override void BeginEdit()
    {
        if (this._BackupData == null)
        {
            this._BackupData = this._CurrentData; //Sauvegarde des données au début de l'édition.
        }
    }

    public override void CancelEdit()
    {
        if (this._BackupData != null)
        {
            this._CurrentData = this._BackupData.Value; //On restaure les données sauvegardées au début de l'édition.
            this._BackupData = null;
            this.OnPropertyChanged(""); //Déclenche le changement de l'ensemble des propriétés.
        }
    }

    public override void EndEdit()
    {
        if (this._BackupData != null)
        {
            this._BackupData = null; //Si l'édition est terminée sans annulation, on supprime les données sauvegardées.
        }
    }

    #endregion
}
```

### Implémentation du contexte de données
Le contexte de données est le conteneur des données de l'application.
Le contexte est représenté par la classe `CoursWPF.BankManager.BankManagerContext` qui possède la ligne d'héritage suivante :

- `System.Object`
  - `CoursWPF.MVVM.ObservableObject` : `CoursWPF.MVVM.Abstracts.IObservableObject` (Objet compatible avec le moteur de `Binding`)
    - `CoursWPF.MVVM.Models.FileDataContext` : `CoursWPF.MVVM.Models.Abstracts.IFileDataContext` (Contexte de données qui prend en charge la sauvegarde dans un fichier unique au format `JSON`)
      - `CoursWPF.BankManager.Models.BankManagerContext` (Contexte de données de l'application)

Le contexte contient une collection observable pour chaque entité du modèle de données de l'application :

``` csharp
public class BankManagerContext : FileDataContext
{
    #region Fields

    private ObservableCollection<BankAccount> _BankAccounts;
    private ObservableCollection<BankAccountLine> _BankAccountLines;
    private ObservableCollection<Category> _Categories;

    #endregion

    #region Properties

    public ObservableCollection<BankAccount> BankAccounts { get => this._BankAccounts; private set => this.SetProperty(nameof(this.BankAccounts), ref this._BankAccounts, value); }
    public ObservableCollection<BankAccountLine> BankAccountLines { get => this._BankAccountLines; private set => this.SetProperty(nameof(this.BankAccountLines), ref this._BankAccountLines, value); }
    public ObservableCollection<Category> Categories { get => this._Categories; private set => this.SetProperty(nameof(this.Categories), ref this._Categories, value); }

    #endregion

    #region Constructors

    public BankManagerContext(string filePath)
        : base(filePath)
    {
        this._BankAccounts = new ObservableCollection<BankAccount>();
        this._BankAccountLines = new ObservableCollection<BankAccountLine>();
        this._Categories = new ObservableCollection<Category>();
    }

    #endregion
}
``` 

`CoursWPF.MVVM.FileDataContext` implémente `CoursWPF.MVVM.Models.Abstracts.IFileDataContext` qui hérite de `CoursWPF.MVVM.Models.Abstracts.IDataContext`.

`CoursWPF.MVVM.Models.Abstracts.IDataContext` déclare les méthodes suivantes :
- `bool CanSave()` : Détermine si le contexte peut être sauvegardé, implémentée par `CoursWPF.MVVM.FileDataContext`
- `void Save()` : Sauvegarde le contexte, implémentée par `CoursWPF.MVVM.FileDataContext`
- `T CreateItem<T>() where T : IObservableObject` : Permet de créer une entité du type spécifié. Cette méthode est abrtraite dans `CoursWPF.MVVM.FileDataContext` et donc être implémentée dans `CoursWPF.BankManager.BankManagerContext`
- `ObservableCollection<T> GetItems<T>() where T : IObservableObject` : Permet d'obtenir la collection des données du type spécifié. Cette méthode est abrtraite dans `CoursWPF.MVVM.FileDataContext` et donc être implémentée dans `CoursWPF.BankManager.BankManagerContext`

> `CreateItem` et `GetItems` seront utilisées principalement dans les vue-modèles pour accéder aux données du contexte.

Implémentation de `T CreateItem<T>() where T : IObservableObject` : 
``` csharp
public override T CreateItem<T>()
{
    IObservableObject createdItem;

    //Pour chaque type concret du modèle, on créer une instance de ce type et on l'ajoute à la collection de données.
    if (typeof(T) == typeof(BankAccount))
    {
        createdItem = new BankAccount();
        this.BankAccounts.Add(createdItem as BankAccount);
    }
    else if (typeof(T) == typeof(BankAccountLine))
    {
        createdItem = new BankAccountLine();
        this.BankAccountLines.Add(createdItem as BankAccountLine);
    }
    else if (typeof(T) == typeof(Category))
    {
        createdItem = new Category();
        this.Categories.Add(createdItem as Category);
    }
    else
    {
        throw new Exception("Le type spécifié n'est pas valide");
    }

    return (T)createdItem;
}
```

Implémentation de `ObservableCollection<T> GetItems<T>() where T : IObservableObject` :
``` csharp
public override ObservableCollection<T> GetItems<T>()
{
    ObservableCollection<T> result;

    //Pour chaque type concret du modèle, on retourne la collection de données du type.
    if (typeof(T) == typeof(BankAccount))
    {
        result = this.BankAccounts as ObservableCollection<T>;
    }
    else if (typeof(T) == typeof(BankAccountLine))
    {
        result = this.BankAccountLines as ObservableCollection<T>;
    }
    else if (typeof(T) == typeof(Category))
    {
        result = this.Categories as ObservableCollection<T>;
    }
    else
    {
        throw new Exception("Le type spécifié n'est pas valide");
    }

    return result;
}
```

## Vue

### Architecture graphique
La vue est construite avec l'architecture suivante :

``` xml
<MainWindow>    <!--ViewModelMain-->
    <Menu/>
    <TabControl>
        <TabItem>
            <AccountingView/>   <!--ViewModelAccounting-->
        </TabItem>
        <TabItem>
            <StatisticsView/>   <!--ViewModelStatistics-->
        </TabItem>
        <TabItem>
            <AdministrationView>    <!--ViewModelAdministration-->
                <TabControl>
                    <TabItem>
                        <BankAccountsView/> <!--ViewModelBankAccounts-->
                    </TabItem>
                    <TabItem>
                        <CategoriesView/>   <!--ViewModelCategories-->
                    </TabItem>
                </TabControl>
            </AdministrationView>
        </TabItem>
    </TabControl>
    <StatusBar/>
```

### Styles
L'application se base principalement sur les styles graphiques définis par `Mahapps`.
Les styles de `Mahapps` sont fusionnés dans le fichier `.\App.xaml`.
L'application définie également sont propre dictionnaire de styles ainsi qu'un dictionnaire de `DataTemplate` (cf. ci-dessous).

``` xml
<Application.Resources>
  <ResourceDictionary>
    <ResourceDictionary.MergedDictionaries>

      <!--Fusion des dictionnaires de styles de Mahapps-->
      <ResourceDictionary Source="pack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml" />
      <ResourceDictionary Source="pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml" />
      <ResourceDictionary Source="pack://application:,,,/MahApps.Metro;component/Styles/Themes/Light.Green.xaml" />
      
      <!--Fusion du dictionnaire de style de l'application-->
      <ResourceDictionary Source="Resources/Styles.xaml"/>

      <!--Fusion des DataTemplates pour représenter les vue-modèles de l'application-->
      <ResourceDictionary Source="Resources/DataTemplates.xaml"/>

    </ResourceDictionary.MergedDictionaries>

    ...

  </ResourceDictionary>
</Application.Resources>
```

### Fenêtre principale
La fenêtre principale de l'application n'est pas instanciée en définissant la propriété `App.StartupUri` dans le fichier `.\App.xaml`.
L'instanciation est réalisée dans l'événement `App.Startup` implémenté dans le fichier `.App.xaml.cs` :

``` csharp
public partial class App : Application
{
    private void Application_Startup(object sender, StartupEventArgs e)
    {
        //Permet de forcer le framework à utiliser la culture du système par défaut dans les Binding.
        FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

        ...

        //Création de l'instance de la fenêtre principale.
        MainWindow window = new MainWindow();
        //Injection du vue-modèle de la fenêtre.
        window.DataContext = serviceProvider.GetService<IViewModelMain>();
        //Affichage de la fenêtre principale.
        window.Show();
    }
}
```

Dans sa structure, la fenêtre principale contient un `TabControl` dont la source de données est liées par `Binding` au vue-modèle principal :
``` xml
<TabControl Grid.Row="1" ItemsSource="{Binding ItemsSource}" SelectedItem="{Binding SelectedItem}">
  <TabControl.ItemTemplate>
    <DataTemplate>
      <TextBlock Text="{Binding Title}"/>
    </DataTemplate>
  </TabControl.ItemTemplate>
</TabControl>
```

### Liaison entre les vues et les vues-modèles
Le choix du `DataTemplate` utilisé pour représenter chaque vue-modèle est réalisé dans le dictionnaire de ressource `.\Resources\DataTemplates.xaml` :
``` xml
<DataTemplate DataType="{x:Type viewModels:ViewModelAccounting}">
  <views:AccountingView/>
</DataTemplate>

<DataTemplate DataType="{x:Type viewModels:ViewModelStatistics}">
  <views:StatisticsView/>
</DataTemplate>

<DataTemplate DataType="{x:Type viewModels:ViewModelAdministration}">
  <views:AdministrationView/>
</DataTemplate>

<DataTemplate DataType="{x:Type viewModels:ViewModelBankAccounts}">
  <views:BankAccountsView/>
</DataTemplate>

<DataTemplate DataType="{x:Type viewModels:ViewModelCategories}">
  <views:CategoriesView/>
</DataTemplate>
```

Chaque vue-modèle est représenté par un contrôle utilisateur définis dans le dossier `.\Views\`.

### Vue `AccountingView`
Cette vue est la plus complexe puisqu'elle est divisées en deux parties.
La partie de gauche affiche la liste des comptes, le contexte de données utilise directement le vue-modèle `ViewModelAccountingView`.
La partie de droite affiche un `DataGrid` pour l'édition des lignes d'écritures, le contexte de données utilise le vue-modèle `ViewModelBankAccountLines` qui est exposé par le vue-modèle `ViewModelAccountingView`.

Les données du `DataGrid` sont filtrées suivant une période mensuelle représentée dans le vue-modèle par la propriété `CurrentDate`.
L'utilisateur peut changer de période à l'aide deux boutons branchés par `Binding` sur la commande `ChangePeriodCommand` qui attend en paramètre la valeur `"-1"` ou `"1"` pour déterminer le sens.
``` xml
<Button     Grid.Row="0" Grid.Column="1" Content="&lt;&lt;" Command="{Binding ChangePeriodCommand}" CommandParameter="-1" Style="{StaticResource ButtonStyle}"/>
<TextBlock  Grid.Row="0" Grid.Column="2" Text="{Binding CurrentDate, StringFormat='{}{0:MM/yyyy}'}" FontWeight="Bold" FontSize="20" MinWidth="100" TextAlignment="Center"/>
<Button     Grid.Row="0" Grid.Column="3" Content="&gt;&gt;" Command="{Binding ChangePeriodCommand}" CommandParameter="1"  Style="{StaticResource ButtonStyle}"/>
```

> `&lt;` permet d'afficher le caractère `<` et `&gt;` permet d'afficher le caractère `>`

Contrairement aux autres `DataGrid` ce dernier a été configuré pour ne pas avoir de mode édition, les contrôles graphiques d'édition sont directement présenté à l'utilisateur :

Pour permettre ce comportement il faut :
- Mettre le `DataGrid` en lecture seule : `IsReadOnly="True"`
- Définir chaque colonne avec un `DataGridTemplateColumn` et mettre le contrôle d'édition directement dans `DataGridTemplateColumn.CellTemplate`.

``` xml
<DataGrid x:Name="DataGridAccountLines"
          IsReadOnly="True">
 <DataGrid.Columns>
   
   <DataGridTemplateColumn Header="Date" SortMemberPath="Date" SortDirection="Descending" MinWidth="100">
     <DataGridTemplateColumn.CellTemplate>
       <DataTemplate>
          <DatePicker SelectedDate="{Binding Date, Mode=TwoWay,UpdateSourceTrigger=PropertyChanged}" SelectedDateChanged="DatePicker_SelectedDateChanged"/>
        </DataTemplate>
      </DataGridTemplateColumn.CellTemplate>
    </DataGridTemplateColumn>

    <DataGridTemplateColumn Header="Libellé"  MinWidth="180" >
      <DataGridTemplateColumn.CellTemplate>
        <DataTemplate>
          <TextBox Text="{Binding Label, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}" />
        </DataTemplate>
      </DataGridTemplateColumn.CellTemplate>
    </DataGridTemplateColumn>

   ...

  </DataGrid.Columns>
</DataGrid>
```

Il est à notter que l'événement `DatePicker.SelectedDateChanged` est implémenté pour :
- Changer la période visible du vue-modèle
- Forcer le tri du `DataGrid`

``` csharp
private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
{
    //e.RemovedItems.Count > 0 : Si la date a été changée
    if (e.RemovedItems.Count > 0 && sender is DatePicker dp && dp.DataContext is BankAccountLine bankAccountLine && DataGridAccountLines.DataContext is IViewModelBankAccountLines viewModel)
    {
        //Change la période visible du vue-modèle.
        viewModel.CurrentDate = new DateTime(bankAccountLine.Date.Year, bankAccountLine.Date.Month, 1);

        //Force le DataGrid à trier les données en fonction de la date d'écriture.
        DataGridAccountLines.Items.SortDescriptions.Clear();
        DataGridAccountLines.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Date", System.ComponentModel.ListSortDirection.Descending));
        DataGridAccountLines.Items.IsLiveSorting = true;
        DataGridAccountLines.Items.Refresh();
    }
}
```

### Vue `StatisticsView`
Cette vue n'est pas implémentée pour le moment.

### Vue `AdministrationView`
Cette vue contient un unique `TabControl` qui présente les vues `BankAccountsView` et `CategoriesView`.
Tout comme le `TabControl` de la fenêtre principal, la source de données est liés par `Binding` au vue-modèle.

### Vue `BankAccountsView`
Cette vu contient un `DataGrid` qui permet l'édition des comptes dans un tableau de données.

### Vue `CategoriesView`
Cette vu contient un `DataGrid` qui permet l'édition des catégories dans un tableau de données.

## Vue-modèle
### Injection des dépendances
Les vues-modèles ont été développés sur le principe de la dépendance faible.
Chaque vue-modèle ne connait donc pas l'instance concrète des autres vues-modèles ainsi que la classe concrète du contexte de données utilisés.
La seule dépendance forte réside dans l'utilisation des classes concrètes du modèles de données.

L'injection de dépendance nécessite pour chaque vue-modèle de déclarer une interface qui décrit le comportement attendu du vue-modèle.

Les vues-modèles sont déclarés dans le répertoire `.\ViewModels\` et les interfaces dans `.\ViewModels\Abstracts\`

L'injection des dépendances est réalisés de deux manières :
- Passage de l'ensemble des dépendances par contructeur
- Passage du `System.IServiceProvider` dans le constructeur pour permettre au vue-modèle de résoudre à tous moment ses dépendances.

#### Passage de l'ensemble des dépendances par contructeur
Par exemple, le vue-modèle `ViewModelBankAccounts` ne dépend que d'un `IDataContext`.
La résolution de cette dépendance n'est pas réalisée par le vue-modèle lui-même mais par la classe qui instancie le vue-modèle.

``` csharp
public ViewModelBankAccounts(IDataContext dataContext)
    : base(dataContext)
{
    this.LoadData();
}
```

``` csharp
private void Application_Startup(object sender, StartupEventArgs e)
{
    ServiceCollection serviceCollection = new ServiceCollection();

    //Création du contexte de données de l'application.
    serviceCollection.AddSingleton<IDataContext, BankManagerContext>(sp => FileDataContext.Load(@"C:\Temp\data.json", new BankManagerContext(@"C:\Temp\data.json")));

    //Lors de l'enregistrement du service IViewModelBankAccounts
    //on précise une méthode d'initialisation du ViewModelBankAccounts pour lui passer en paramètre le service IDataContext.
    serviceCollection.AddTransient<IViewModelBankAccounts, ViewModelBankAccounts>(sp => new ViewModelBankAccounts(sp.GetService<IDataContext>()));

    ...
}
```

#### Passage du `System.IServiceProvider` dans le constructeur
Par exemple, le vue-modèle `ViewModelMain` dépend que directement d'un `IServiceProvider`.
Ceci permet au vue-modèle ensuite de demander au fournisseur de service de résoudre ses dépendances.

``` csharp
/// <summary>
///     Initialise une nouvelle instance de la classe <see cref="ViewModelMain"/>.
/// </summary>
/// <param name="serviceProvider">Fournisseur de service de l'application.</param>
public ViewModelMain(IServiceProvider serviceProvider)
    : base(serviceProvider.GetService<IDataContext>())
{
    this._ServiceProvider = serviceProvider;

    this._ViewModelAccounting = this._ServiceProvider.GetService<IViewModelAccounting>();
    this._ViewModelStatistics = this._ServiceProvider.GetService<IViewModelStatistics>();
    this._ViewModelAdministration = this._ServiceProvider.GetService<IViewModelAdministration>();

    this._ExitCommand = new RelayCommand(this.Exit, this.CanExit);
    this.LoadData();
}
```

``` csharp
private void Application_Startup(object sender, StartupEventArgs e)
{
    ServiceCollection serviceCollection = new ServiceCollection();

    //Création du contexte de données de l'application.
    serviceCollection.AddSingleton<IDataContext, BankManagerContext>(sp => FileDataContext.Load(@"C:\Temp\data.json", new BankManagerContext(@"C:\Temp\data.json")));

    //Lors de l'enregistrement du service IViewModelMain
    //on précise une méthode d'initialisation du ViewModelMain pour lui passer en paramètre le fournisseur de service pour lui permettre de résoudre ses dépendances.
    serviceCollection.AddTransient<IViewModelMain, ViewModelMain>(sp => new ViewModelMain(sp));

    ...
}
```

#### Gestion des instances par le fournisseur de service
L'ensemble des services des vues-modèles sont déclarés avec la méthode `AddTransient`, ce qui signifie qu'à chaque résolution, le fournisseur de service retourne une nouvelle instance du vue-modèle demandé.
Par contre, le contexte de données doit être commun à l'ensemble de l'application, le service est donc déclaré avec la méthode `AddSingleton`, ce qui signifie qu'à chaque résolution, le fournisseur de service retoune l'instance unique du contexte de données.
``` csharp
private void Application_Startup(object sender, StartupEventArgs e)
{
    ServiceCollection serviceCollection = new ServiceCollection();

    //Enregistrement des services
    serviceCollection.AddSingleton<IDataContext, BankManagerContext>(sp => FileDataContext.Load(@"C:\Temp\data.json", new BankManagerContext(@"C:\Temp\data.json")));
    serviceCollection.AddTransient<IViewModelMain, ViewModelMain>(sp => new ViewModelMain(sp));
    serviceCollection.AddTransient<IViewModelAccounting, ViewModelAccounting>(sp => new ViewModelAccounting(sp));
    serviceCollection.AddTransient<IViewModelStatistics, ViewModelStatistics>(sp => new ViewModelStatistics());
    serviceCollection.AddTransient<IViewModelAdministration, ViewModelAdministration>(sp => new ViewModelAdministration(sp));
    serviceCollection.AddTransient<IViewModelBankAccounts, ViewModelBankAccounts>(sp => new ViewModelBankAccounts(sp.GetService<IDataContext>()));
    serviceCollection.AddTransient<IViewModelBankAccountLines, ViewModelBankAccountLines>(sp => new ViewModelBankAccountLines(sp.GetService<IDataContext>()));
    serviceCollection.AddTransient<IViewModelCategories, ViewModelCategories>(sp => new ViewModelCategories(sp.GetService<IDataContext>()));

    //Construction du fournisseur de service à partir de la définition des services disponibles.
    ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

    MainWindow window = new MainWindow();

    //Utilisation du fournisseur de service pour résoudre la dépendance IViewModelMain.
    window.DataContext = serviceProvider.GetService<IViewModelMain>(); 
    window.Show();
}
```

### Architecture
Les vues-modèles respectent l'architecture suivante :

``` xml
<ViewModelMain>
  <ViewModelMain.ItemsSource>

    <ViewModelAccounting>
      <ViewModelBankAccountLines/>
    </ViewModelAccounting>

    <ViewModelStatistics/>

    <ViewModelAdministration>
      <ViewModelAdministration.ItemsSource>

        <ViewModelBankAccounts/>

        <ViewModelCategories/>

      </ViewModelAdministration.ItemsSource>
    </ViewModelAdministration>

  </ViewModelMain.ItemsSource>    
</ViewModelMain>
```

### Vue-modèle `ViewModelMain`
Ce vue-modèle hérite de la classe `CoursWPF.MVVM.ViewModels.ViewModelList<IObservableObject, IDataContext>`.

Il dispose donc d'une collection observable `ItemsSource` et d'un `SelectedItem` de type `IObservableObject` ainsi que du contexte de données.
Ce dernier ne sera pas réèlement utilisé puisque `ViewModelMain` est un vue-modèle structurel (utilisé pour structurer l'interface graphique).

#### Fermeture de l'application
Le vue-modèle expose et implémente la commande `ExitCommand` qui permet de fermer l'application :
``` csharp
public class ViewModelMain : ViewModelList<IObservableObject, IDataContext>, IViewModelMain
{
    #region Fields

    ...

    private readonly RelayCommand _ExitCommand;

    #endregion

    #region Properties

    ...

    public RelayCommand ExitCommand => this._ExitCommand;

    #endregion

    #region Constructors

    public ViewModelMain(IServiceProvider serviceProvider)
        : base(serviceProvider.GetService<IDataContext>())
    {
        ...
        this._ExitCommand = new RelayCommand(this.Exit, this.CanExit);
        ...
    }

    #endregion

    #region Methods

    ...

    #region ExitCommand

    protected virtual bool CanExit(object parameter) => true;

    protected virtual void Exit(object parameter)
    {
        App.Current.Shutdown(0);
    }

    #endregion

    #endregion
}
```

La commande est accessible dans le menu principal de l'application.

#### Sauvegarde des données
Comme expliqué ci-dessus, le vue-modèle hérite de `CoursWPF.MVVM.ViewModels.ViewModelList<IObservableObject, IDataContext>` qui lui-même hérite de `CoursWPF.MVVM.ViewModels.ViewModelWithDataContext<IDataContext>`.
Dans son implémentaton, cette dernière définie la commande `SaveCommand`. C'est donc implicitement que le vue-modèle `ViewModelMain` est chargé de sauvegarder le contexte de données :
``` csharp
    public class ViewModelWithDataContext<T> : ObservableObject, IViewModelWithDataContext<T>
        where T : IDataContext
    {
        #region Fields

        private IDataContext _DataContext;
        private readonly RelayCommand _SaveCommand;

        #endregion

        #region Properties

        public IDataContext DataContext { get => this._DataContext; private set => this.SetProperty(nameof(this.DataContext), ref this._DataContext, value); }
        public RelayCommand SaveCommand => this._SaveCommand;

        #endregion

        #region Constructors

        public ViewModelWithDataContext(IDataContext dataContext)
        {
            this._DataContext = dataContext;
            this._SaveCommand = new RelayCommand(this.Save, this.CanSave);
        }

        #endregion

        #region Methods

        ...

        protected virtual bool CanSave(object parameter) => this.DataContext.CanSave();

        protected virtual void Save(object parameter)
        {
            this.DataContext.Save();
        }

        #endregion
    }
```

La commande est accessible dans le menu principal de l'application.

#### Gestion des commandes `AddCommand` et `DeleteCommand`
Dans le menu principal de l'application, les commandes d'ajout et de suppression sont liées par `Binding` au vue-modèle enfant sélectionné dans `ViewModelMain` :
``` xml
<Menu Grid.Row="0" Grid.ColumnSpan="2">
<MenuItem Header="Fichier">
    <MenuItem Header="Sauvegarder"  Command="{Binding SaveCommand}"/>
    <Separator/>
    <MenuItem Header="Quitter"      Command="{Binding ExitCommand}"/>
</MenuItem>
<MenuItem Header="Édition">
    <MenuItem Header="Ajouter"    Command="{Binding SelectedItem.AddCommand}"/>
    <MenuItem Header="Supprimer"  Command="{Binding SelectedItem.DeleteCommand}"/>
</MenuItem>
</Menu>
```

Il est donc préférable de désactiver les commandes `AddCommand` et `DeleteCommand` dans `ViewModelMain` :
``` csharp
public class ViewModelMain : ViewModelList<IObservableObject, IDataContext>, IViewModelMain
{
    protected override bool CanAdd(object parameter) => false;
    protected override bool CanDelete(object parameter) => false;
}
```

#### Initialisation des vues-modèles enfants et présentation à la vue
Le vue-modèle principal de l'application gère les trois vues-modèles utilisés par les trois onglets principaux :
- `ViewModelAccounting` : Onglet de gestion des écritures dans un compte
- `ViewModelStatistics` : Onglet de l'affichage des statistiques
- `ViewModelAdministration` : Onglet de paramétrage de l'application (gestion des comptes et des catégories)

Les vues-modèles enfants sont déclarés et exposés en tant que propriété dans `ViewModelMain` :

``` csharp
public class ViewModelMain : ViewModelList<IObservableObject, IDataContext>, IViewModelMain
{
    #region Fields

    private IViewModelAccounting _ViewModelAccounting;
    private IViewModelStatistics _ViewModelStatistics;
    private IViewModelAdministration _ViewModelAdministration;

    #endregion

    #region Properties

    public IViewModelAccounting ViewModelAccounting { get => this._ViewModelAccounting; private set => this.SetProperty(nameof(this.ViewModelAccounting), ref this._ViewModelAccounting, value); }
    public IViewModelStatistics ViewModelStatistics { get => this._ViewModelStatistics; private set => this.SetProperty(nameof(this.ViewModelStatistics), ref this._ViewModelStatistics, value); }
    public IViewModelAdministration ViewModelAdministration { get => this._ViewModelAdministration; private set => this.SetProperty(nameof(this.ViewModelAdministration), ref this._ViewModelAdministration, value); }

    #endregion
}
```

L'initialisation des vues-modèles est réalisée dans le constructeur de la classe :
``` csharp
public class ViewModelMain : ViewModelList<IObservableObject, IDataContext>, IViewModelMain
{
    public ViewModelMain(IServiceProvider serviceProvider)
        : base(serviceProvider.GetService<IDataContext>())
    {
        this._ServiceProvider = serviceProvider;

        //Les instances des vues-modèles sont créées par le fournisseur de service (injection de dépendance).
        this._ViewModelAccounting = this._ServiceProvider.GetService<IViewModelAccounting>();
        this._ViewModelStatistics = this._ServiceProvider.GetService<IViewModelStatistics>();
        this._ViewModelAdministration = this._ServiceProvider.GetService<IViewModelAdministration>();

        ...

        //Méthode appelée pour initialise la collection observable ItemsSource
        this.LoadData();
    }
}
```

Comme indiqué, le constructeur appel la méthode `void LoadData()` qui se charge d'initialiser la collection observable `ItemsSource` :
``` csharp
public class ViewModelMain : ViewModelList<IObservableObject, IDataContext>, IViewModelMain
{
    public override void LoadData()
    {
        //La collection observable ItemsSource contient les vues-modèles pour les présenter au TabControl principal.
        this.ItemsSource = new ObservableCollection<IObservableObject>(new IObservableObject[]
        {
            this._ViewModelAccounting,
            this._ViewModelStatistics,
            this._ViewModelAdministration
        });
        //Sélection de l'onglet représenté par ViewModelAccounting
        this.SelectedItem = this._ViewModelAccounting;
    }
}
```

Il est à noter que le comportement de la méthode `void LoadData()` de la classe parente (`CoursWPF.MVVM.ViewModels.ViewModelList<IObservableObject, IDataContext>`)
est substitué (`override` sans appel de `base.LoadData()`).
Par défaut, la méthode `base.LoadData()` appel la méthode `DataContext.GetItems<T>()` pour initailiser la collection observable `ItemsSource`.
Les vues-modèles présentés dans `ItemsSource` par `ViewModelMain` ne font pas parti du modèle de données et n'ont pas d'existance dans le contexte de données, l'appel de `base.LoadData()` dans ce cas de figure génère donc une excepion.

#### Gestion du rafraîchissement des données lors du changement de l'onglet sélectionné
Immaginer le comportement suivant :
- L'utilisateur est dans l'onglet de gestion des écritures dans un compte, géré par le vue-modèle `ViewModelAccounting`.
- L'utilisateur sélectionne l'onglet d'administration de l'application, géré par le vue-modèle `ViewModelAdministration`
- L'utilisateur ajoute un compte bancaire
- L'utilisateur sélectionne de nouveau l'onglet de gestion des écritures dans un compte, géré par le vue-modèle `ViewModelAccounting`

Dans ce cas, l'utilisateur ne verra pas le compte bancaire apparaître dans l'onglet de gestion des écritures dans un compte.
Ce comportement est lié au fait que la méthode `ViewModelAccounting.LoadData()` qui se charge de remplire la propriété `ViewModelAccounting.ItemsSource` est appelée uniquement lors du constructeur de la classe `ViewModelAccounting`.
Pour voir apparaître le nouveau compte, il faut explicitement rappeler la méthode `ViewModelAccounting.LoadData()`.
Pour palier ce problème, le `ViewModelMain` implémente la logique suivante :
- Lorsque l'onglet sélectionné dans le `TabControl` principal change, la méthode `LoadData()` du vue-modèle de l'onglet sélectionné est appelée.

Ce comportement correspond au code suivant :
``` csharp
public class ViewModelMain : ViewModelList<IObservableObject, IDataContext>, IViewModelMain
{

    protected override void OnPropertyChanged(string propertyName)
    {
        base.OnPropertyChanged(propertyName);

        switch (propertyName)
        {
            //Lorsque l'onglet sélectionné change
            case nameof(this.SelectedItem):
                 //On appel la méthode LoadData() du vue-modèle sélectionné.
                (this.SelectedItem as IViewModelList<IDataContext>)?.LoadData();
                break;
            default:
                break;
        }
    }
}
```

Lors du changement de l'onglet sélectionné dans le `TabControl` principal de l'application, il est nécessaire de forcer les données des sous-onglets de se rafraîchir.

### Vue-modèle `ViewModelAccounting`
Ce vue-modèle hérite de la classe `CoursWPF.MVVM.ViewModels.ViewModelList<BankAccount, IDataContext>`.
Il représente dans une liste de compte bancaire.
Ce vue-modèle est également en charge de gérer le vue-modèle des écritures, nottament en lui donnant le compte bancaire sélectionné.

#### Initialisation du vue-modèle enfant de gestion des écritures
L'intialisation du vue-modèle enfant est similaire à ce qui est fait dans `ViewModelMain`.

#### Passage du compte bancaire sélectionné au vue-modèle des écritures
Lorsque le compte bancaire sélectionné change dans la liste, il est nécessaire de transférer cette information au vue-modèle enfant :
``` csharp
public class ViewModelAccounting : ViewModelList<BankAccount, IDataContext>, IViewModelAccounting
{
    protected override void OnPropertyChanged(string propertyName)
    {
        base.OnPropertyChanged(propertyName);

        switch (propertyName)
        {
            case nameof(this.SelectedItem):
                //Affectation de la banque sélectionnée.
                this.ViewModelBankAccountLines.BankAccount = this.SelectedItem;
                break;
            default:
                break;
        }
    }
}
```
> Il est à notter que le vue-modèle enfant appel automatiquement la méthode `LoadData()` lorsque la propriété `BankAccount` est modifiée.

#### Gestion des commandes `AddCommand` et `DeleteCommand`
L'utilisateur ne doit pas pouvoir ajouter ou supprimer un compte bancaire depuis ce vue-modèle.
Le comportement attendu est de relayer les commandes du vue-modèle sous-jacent de gestion des écritures :

``` csharp
public class ViewModelAccounting : ViewModelList<BankAccount, IDataContext>, IViewModelAccounting
{
    public override RelayCommand AddCommand => this.ViewModelBankAccountLines.AddCommand;
    public override RelayCommand DeleteCommand => this.ViewModelBankAccountLines.DeleteCommand;
}
```

Les commandes doivent donc être désactivée dans ce vue-modèle.
``` csharp
public class ViewModelAccounting : ViewModelList<BankAccount, IDataContext>, IViewModelAccounting
{
    protected override bool CanAdd(object parameter) => false;
    protected override bool CanDelete(object parameter) => false;
}
```

### Vue-modèle `ViewModelBankAccountLines`
Ce vue-modèle hérite de la classe `CoursWPF.MVVM.ViewModels.ViewModelList<BankAccountLine, IDataContext>`.
Il représente dans une liste d'écriture dans un compte bancaire.

#### Filtre de données sur une période mensuelle et d'une banque associée.
Contrairement aux autres vue-modèle, les données exposées dans la collection `ItemsSource` sont filtrées en fonction :
- Du compte bancaire associé
- D'une période mensuelle sélectionnée

La période est représentée par la propriété `CurrentDate` et le compte bancaire associé par la propriété `BankAccount`.
``` csharp
public class ViewModelBankAccountLines : ViewModelList<BankAccountLine, IDataContext>, IViewModelBankAccountLines
{
    #region Fields

    private BankAccount _BankAccount;
    private DateTime _CurrentDate;

    #endregion
        
    #region Properties

    public BankAccount BankAccount { get => this._BankAccount; set => this.SetProperty(nameof(this.BankAccount), ref this._BankAccount, value); }
    public DateTime CurrentDate { get => this._CurrentDate; set => this.SetProperty(nameof(this.CurrentDate), ref this._CurrentDate, value); }

    #endregion
}
```

La propriété `CurrentDate` est initialisée au premier jour du mois en cours dans le constructeur :
``` csharp
public class ViewModelBankAccountLines : ViewModelList<BankAccountLine, IDataContext>, IViewModelBankAccountLines
{
    public ViewModelBankAccountLines(IDataContext dataContext)
        : base(dataContext)
    {
        this._CurrentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
    }
}
```

Contrairement aux autres vue-modèle, la méthode `LoadData()` n'est pas appelée dans le constructeur puisqu'à ce moment, la banque associée n'est pas définie.
Lors du chargement, le filtre est réalisé en surchargeant le comportement de la méthode `base.LoadData()`.

``` csharp
public class ViewModelBankAccountLines : ViewModelList<BankAccountLine, IDataContext>, IViewModelBankAccountLines
{
    public override void LoadData()
    {
        this.ItemsSource = this.BankAccount == null ? new ObservableCollection<BankAccountLine>() : new ObservableCollection<BankAccountLine>(this.DataContext.GetItems<BankAccountLine>().Where(bal => bal.IdentifierBankAccount == this.BankAccount.Identifier && bal.Date >= this.CurrentDate && bal.Date < this.CurrentDate.AddMonths(1)));
    }
}
```

Lors du changement de la période sélectionnée ou de la banque associée, il est nécessaire de rafraîchir les données présentées à la vue : 
``` csharp
public class ViewModelBankAccountLines : ViewModelList<BankAccountLine, IDataContext>, IViewModelBankAccountLines
{
    protected override void OnPropertyChanged(string propertyName)
    {
        base.OnPropertyChanged(propertyName);

        switch (propertyName)
        {
            case nameof(this.CurrentDate):
            case nameof(this.BankAccount):
                this.LoadData();
                break;
            default:
                break;
        }
    }
}
```

#### Changement de la période mensuelle
La période mensuelle du vue-modèle, représentée par la propriété `CurrentDate` peut être changée de deux manières :
- Directement en modifiant la valeur de la propriété `CurrentDate`
  >**<!> Pour assurer le bon fonctionnement du filtre, la date doit impérativement être au premier jour du mois avec l'heure à 00:00:00. Pour éviter les bogues il serait judicieux que le vue-modèle s'assure de se comportement au niveau du `set` de la propriété `CurrentDate`<!>**
- En utilisant la commande `ChangePeriodCommand`

La commande est définie et exposée par le vue-modèle : 
``` csharp
public class ViewModelBankAccountLines : ViewModelList<BankAccountLine, IDataContext>, IViewModelBankAccountLines
{
    #region Fields

    private RelayCommand _ChangePeriodCommand;

    #endregion
        
    #region Properties
        
    public RelayCommand ChangePeriodCommand => this._ChangePeriodCommand;

    #endregion

    #region Constructors

    public ViewModelBankAccountLines(IDataContext dataContext)
        : base(dataContext)
    {
        this._ChangePeriodCommand = new RelayCommand(this.ChangePeriod, this.CanChangePeriod);
    }

    #endregion
}
```

Son implémentation oblige le passage d'un paramètre `"1"` ou `"-1"` ainsi que la présente d'une banque associée :
``` csharp
public class ViewModelBankAccountLines : ViewModelList<BankAccountLine, IDataContext>, IViewModelBankAccountLines
{
    protected virtual void ChangePeriod(object parameter)
    {
        if (parameter as string == "-1")
        {
            this.CurrentDate = this.CurrentDate.AddMonths(-1);
        }
        else if (parameter as string == "1")
        {
            this.CurrentDate = this.CurrentDate.AddMonths(1);
        }
    }
    protected virtual bool CanChangePeriod(object parameter) => this.BankAccount != null && (parameter as string == "-1" || parameter as string == "1");

    #endregion
}
```

#### Ajout d'une écriture
Lors de l'ajout d'une écriture, il est nécessaire d'associer l'écriture au compte bancaire :
``` csharp
public class ViewModelBankAccountLines : ViewModelList<BankAccountLine, IDataContext>, IViewModelBankAccountLines
{
    protected override void Add(object parameter)
    {
        base.Add(parameter);

        //Affectation de la date à la période actuelle pour que l'écriture soit visible.
        this.SelectedItem.Date = this.CurrentDate;
        
        //Ajout de l'écriture dans la banque associée
        this.BankAccount.BankAccountLines.Add(this.SelectedItem);

        //Calcul de l'identifiant de l'écriture
        this.SelectedItem.Identifier = this.DataContext.GetItems<BankAccountLine>().Max(bal => bal.Identifier) + 1;
        
        //Association de la banque
        this.SelectedItem.IdentifierBankAccount = this.BankAccount.Identifier;
        this.SelectedItem.BankAccount = this.BankAccount;
    }

    protected override bool CanAdd(object parameter) => this.BankAccount != null;
}
```

#### Suppression d'une écriture
Lors de la suppression d'une écriture, il est nécessaire de la désassocier de la banque :
``` csharp
public class ViewModelBankAccountLines : ViewModelList<BankAccountLine, IDataContext>, IViewModelBankAccountLines
{
    protected override void Delete(object parameter)
    {
        this.BankAccount.BankAccountLines.Remove(this.SelectedItem);
        base.Delete(parameter);
    }

    protected override bool CanDelete(object parameter) => this.BankAccount != null && base.CanDelete(parameter);
}
```

### Vue-modèle `ViewModelStatistics`
Ce vue-modèle n'est pas implémenté pour le moment, il ne fait que définir une propriété `Title` utilisée par la vue dans le titre du `TabItem`.

### Vue-modèle `ViewModelAdministration`
Tout comme `ViewModelMain`, `ViewModelAdministration` hérite de la classe `CoursWPF.MVVM.ViewModels.ViewModelList<IObservableObject, IDataContext>`.

Il dispose donc d'une collection observable `ItemsSource` et d'un `SelectedItem` de type `IObservableObject` ainsi que du contexte de données.
Ce dernier ne sera pas utilisé puisque `ViewModelAdministration` est un vue-modèle structurel (utilisé pour structurer l'interface graphique).

#### Initialisation des vues-modèles enfants et présentation à la vue
Tout comme `ViewModelMain`, ce vue-modèle déclare et expose dans `ItemsSource` des vues-modèles enfants :
- `ViewModelBankAccounts` : Onglet de gestion des comptes bancaires
- `ViewModelCategories` : Onglet de gestion des catégories

L'intialisation des vues-modèle enfants est similaire à ce qui est fait dans `ViewModelMain`.

#### Gestion des commandes `AddCommand` et `DeleteCommand`

Tout comme `ViewModelMain`, l'implémentation des commandes d'ajout et de suppression par `ViewModelAdministration` n'a pas de sens, il est nécessaire d'empêcher l'exécution des commandes :

``` csharp
public class ViewModelAdministration : ViewModelList<IObservableObject, IDataContext>, IViewModelAdministration
{
    protected override bool CanAdd(object parameter) => false;
}
```

Par contre, les commandes d'ajout et de suppression doivent être relayées aux vue-modèle sélectionné (`ViewModelBankAccounts` ou `ViewModelCategories`) :

``` csharp
public class ViewModelAdministration : ViewModelList<IObservableObject, IDataContext>, IViewModelAdministration
{
    #region Properties

    public override RelayCommand AddCommand => (this.SelectedItem as IViewModelList<IDataContext>)?.AddCommand ?? base.AddCommand;
    public override RelayCommand DeleteCommand => (this.SelectedItem as IViewModelList<IDataContext>)?.DeleteCommand ?? base.DeleteCommand;

    #endregion

    #region Methods

    protected override void OnPropertyChanged(string propertyName)
    {
        base.OnPropertyChanged(propertyName);

        switch (propertyName)
        {
            //Lorsque l'onglet sélectionné change
            case nameof(this.SelectedItem):
                //On prévient la vue que la commande AddCommand et DeleteCommand ont changés pour qu'elles soient bien routées au vue-modèle sélectionné.
                this.OnPropertyChanged(nameof(this.AddCommand));
                this.OnPropertyChanged(nameof(this.DeleteCommand));
                break;
            default:
                break;
        }
    }

    #endregion
}
```

### Vue-modèle `ViewModelBankAccounts`
Ce vue-modèle hérite de la classe `CoursWPF.MVVM.ViewModels.ViewModelList<BankAccount, IDataContext>`.
Il représente dans une liste de compte bancaire. Ce vue-modèle est utilisé dans l'administration pour la gestion des comptes bancaire.

Le comportement spécifique de ce vue-modèle ce limite à :
- La propriété `Title`, utilisée par la vue dans le titre du `TabItem`
- L'appel de la méthode `LoadData()` dans le constructeur pour charger les données dans `ItemsSource`
- Lors de l'exécution de la commande `AddCommand`, calcul de l'identifiant de la catégorie ajoutée

### Vue-modèle `ViewModelCategories`
Ce vue-modèle hérite de la classe `CoursWPF.MVVM.ViewModels.ViewModelList<Category, IDataContext>`.
Il représente dans une liste de catégorie. Ce vue-modèle est utilisé dans l'administration pour la gestion des catégories.

Le comportement spécifique de ce vue-modèle ce limite à :
- La propriété `Title`, utilisée par la vue dans le titre du `TabItem`
- L'appel de la méthode `LoadData()` dans le constructeur pour charger les données dans `ItemsSource`
- Lors de l'exécution de la commande `AddCommand`, calcul de l'identifiant de la catégorie ajoutée

# Bogues actifs
Dans son fonctionnement, l'intégrité référentiel des entités du contexte est gérée manuellement.
Actuellement, lorsqu'une catégorie d'une écriture est sélectionnée, la propriété `BankAccountLine.Category` est bien définie mais
la propriété `BankAccountLine.IdentifierCategory` qui représente l'identifiant de la catégorie n'est pas définie.
Ceci provoque la non sauvegarde de la catégorie associée.

La correction de se comportement pourrait être fait en implémentant un système d'intégrité référentielle au niveau des entités.
Ceci permettrait également de simplifier le code dans certains vue-modèle où l'intégrité référentielle est gérée manuellement.