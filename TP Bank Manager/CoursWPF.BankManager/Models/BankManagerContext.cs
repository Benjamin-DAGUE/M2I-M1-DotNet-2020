using CoursWPF.MVVM.Abstracts;
using CoursWPF.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CoursWPF.BankManager.Models
{
    /// <summary>
    ///     Contexte de données de l'application.
    /// </summary>
    public class BankManagerContext : FileDataContext
    {
        #region Fields

        /// <summary>
        ///     Collection des comptes bancaire.
        /// </summary>
        private ObservableCollection<BankAccount> _BankAccounts;

        /// <summary>
        ///     Collection des écritures bancaire.
        /// </summary>
        private ObservableCollection<BankAccountLine> _BankAccountLines;

        /// <summary>
        ///     Collection des catégories d'écritures.
        /// </summary>
        private ObservableCollection<Category> _Categories;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient la collection des comptes bancaire.
        /// </summary>
        public ObservableCollection<BankAccount> BankAccounts { get => this._BankAccounts; private set => this.SetProperty(nameof(this.BankAccounts), ref this._BankAccounts, value); }

        /// <summary>
        ///     Obtient la collection des écritures bancaire.
        /// </summary>
        public ObservableCollection<BankAccountLine> BankAccountLines { get => this._BankAccountLines; private set => this.SetProperty(nameof(this.BankAccountLines), ref this._BankAccountLines, value); }

        /// <summary>
        ///     Obtient la collection des catégories d'écritures.
        /// </summary>
        public ObservableCollection<Category> Categories { get => this._Categories; private set => this.SetProperty(nameof(this.Categories), ref this._Categories, value); }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="BankManagerContext"/>.
        /// </summary>
        /// <param name="filePath">Chemin du fichier de données.</param>
        public BankManagerContext(string filePath)
            : base(filePath)
        {
            this._BankAccounts = new ObservableCollection<BankAccount>();
            this._BankAccountLines = new ObservableCollection<BankAccountLine>();
            this._Categories = new ObservableCollection<Category>();
        }

        #endregion

        #region Methods

        public override T CreateItem<T>()
        {
            IObservableObject createdItem;

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

        public override ObservableCollection<T> GetItems<T>()
        {
            ObservableCollection<T> result;

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

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            this.BankAccountLines.ToList().ForEach(bal =>
            {
                bal.BankAccount = this.BankAccounts.FirstOrDefault(ba => ba.Identifier == bal.IdentifierBankAccount);
                bal.BankAccount?.BankAccountLines?.Add(bal);
                bal.Category = this.Categories.FirstOrDefault(ba => ba.Identifier == bal.IdentifierCategory);
                bal.Category?.BankAccountLines?.Add(bal);
            });
        }

        #endregion
    }
}
