using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursWPF.BankManager.Models
{
    /// <summary>
    ///     Jeu de données prenant en charge la sérialisation au format json.
    /// </summary>
    public class DataStore
    {
        #region Fields

        /// <summary>
        ///     Collection des comptes bancaires.
        /// </summary>
        private ObservableCollection<BankAccount> _BankAccounts;

        /// <summary>
        ///     Collection des catégories.
        /// </summary>
        private ObservableCollection<Category> _Categories;

        /// <summary>
        ///     Collection des lignes d'écritures.
        /// </summary>
        private ObservableCollection<BankAccountLine> _BankAccountLines;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient la collection des comptes bancaires.
        /// </summary>
        public ObservableCollection<BankAccount> BankAccounts => this._BankAccounts;

        /// <summary>
        ///     Obtient la collection des catégories.
        /// </summary>
        public ObservableCollection<Category> Categories => this._Categories;

        /// <summary>
        ///     Obtient la collection des lignes d'écritures.
        /// </summary>
        public ObservableCollection<BankAccountLine> BankAccountLines => this._BankAccountLines;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de <see cref="DataStore"/>.
        /// </summary>
        public DataStore()
        {
            this._BankAccounts = new ObservableCollection<BankAccount>();
            this._Categories = new ObservableCollection<Category>();
            this._BankAccountLines = new ObservableCollection<BankAccountLine>();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Sauvegarde le jeu de données dans un fichier au format json.
        /// </summary>
        public void Save()
        {
            File.WriteAllText(".\\data.json", JsonConvert.SerializeObject(this));
        }

        /// <summary>
        ///     Charge un jeu de données depuis un fichier au format json.
        /// </summary>
        /// <returns>Instance du <see cref="DataStore"/> correspondant au fichier ou un <see cref="DataStore"/> vide si le fichier n'existe pas.</returns>
        public static DataStore Load()
        {
            DataStore dataStore;

            try
            {
                dataStore = JsonConvert.DeserializeObject<DataStore>(File.ReadAllText(".\\data.json"));

                foreach (BankAccountLine bankAccountLine in dataStore.BankAccountLines)
                {
                    BankAccount bankAccount = dataStore.BankAccounts.FirstOrDefault(ba => ba.Identifier == bankAccountLine.IdentifierBankAccount);

                    if (bankAccount != null)
                    {
                        bankAccount.BankAccountLines.Add(bankAccountLine);
                    }

                    bankAccountLine.Category = dataStore.Categories.FirstOrDefault(cat => cat.Identifier == bankAccountLine.IdentifierCategory);
                }
            }
            catch
            {
                dataStore = new DataStore();
            }

            return dataStore;
        }

        #endregion
    }
}
