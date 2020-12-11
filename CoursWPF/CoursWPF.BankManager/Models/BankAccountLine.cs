using CoursWPF.MVVM;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursWPF.BankManager.Models
{
    [JsonObject(MemberSerialization.OptOut)]
    public class BankAccountLine : ObservableObject
    {
        #region Fields

        private Guid _Identifier;

        private Guid _IdentifierBankAccount;

        private Guid? _IdentifierCategory;

        /// <summary>
        ///     Libellé de l'écriture.
        /// </summary>
        private string _Label;

        /// <summary>
        ///     Montant de l'écirutre.
        /// </summary>
        private decimal _Value;

        /// <summary>
        ///     Date de l'écriture
        /// </summary>
        private DateTime _Date;

        private Category _Category;

        #endregion

        #region Properties

        public Guid Identifier
        {
            get => this._Identifier;
            set => this.SetProperty(nameof(this.Identifier), ref this._Identifier, value);
        }

        public Guid IdentifierBankAccount
        {
            get => this._IdentifierBankAccount;
            set => this.SetProperty(nameof(this.IdentifierBankAccount), ref this._IdentifierBankAccount, value);
        }

        public Guid? IdentifierCategory
        {
            get => this._IdentifierCategory;
            set => this.SetProperty(nameof(this.IdentifierCategory), ref this._IdentifierCategory, value);
        }


        /// <summary>
        ///     Libellé de l'écriture.
        /// </summary>
        public string Label
        {
            get => this._Label;
            set => this.SetProperty(nameof(this.Label), ref this._Label, value);
        }

        /// <summary>
        ///     Montant de l'écirutre.
        /// </summary>
        public decimal Value
        {
            get => this._Value;
            set => this.SetProperty(nameof(this.Value), ref this._Value, value);
        }

        /// <summary>
        ///     Date de l'écriture
        /// </summary>
        public DateTime Date
        {
            get => this._Date;
            set => this.SetProperty(nameof(this.Date), ref this._Date, value);
        }

        [JsonIgnore]
        public Category Category
        {
            get => this._Category;
            set
            {
                this.SetProperty(nameof(this.Category), ref this._Category, value);
                this.IdentifierCategory = this.Category?.Identifier;
            }
        }

        #endregion
    }
}
