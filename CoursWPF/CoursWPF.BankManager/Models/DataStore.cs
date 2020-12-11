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
    public class DataStore
    {
        #region Fields

        private ObservableCollection<BankAccount> _BankAccounts;
        private ObservableCollection<Category> _Categories;
        private ObservableCollection<BankAccountLine> _BankAccountLines;

        #endregion

        #region Properties

        public ObservableCollection<BankAccount> BankAccounts => this._BankAccounts;
        public ObservableCollection<Category> Categories => this._Categories;
        public ObservableCollection<BankAccountLine> BankAccountLines => this._BankAccountLines;

        #endregion

        #region Constructors

        public DataStore()
        {
            this._BankAccounts = new ObservableCollection<BankAccount>();
            this._Categories = new ObservableCollection<Category>();
            this._BankAccountLines = new ObservableCollection<BankAccountLine>();
        }

        #endregion

        #region Methods

        public void Save()
        {
            File.WriteAllText(".\\data.json", JsonConvert.SerializeObject(this));
        }

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
