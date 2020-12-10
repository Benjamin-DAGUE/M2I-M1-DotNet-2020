using CoursWPF.MVVM.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CoursWPF.BankManager.Models
{
    /// <summary>
    ///     Classe de données qui représente une ligne d'écriture dans un <see cref="BankAccount"/>.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class BankAccountLine : Entity
    {
        #region Fields

        /// <summary>
        ///     Structure des données de la classe <see cref="BankAccountLine"/>.
        /// </summary>
        private struct BankAccountLineData
        {
            /// <summary>
            ///     Identifiant du compte bancaire associé.
            /// </summary>
            public long IdentifierBankAccount { get; set; }

            /// <summary>
            ///     Identifiant de la catégorie associée.
            /// </summary>
            public long? IdentifierCategory { get; set; }

            /// <summary>
            ///     Compte bancaire associé.
            /// </summary>
            public BankAccount BankAccount { get; set; }

            /// <summary>
            ///     Catégorie associée.
            /// </summary>
            public Category Category { get; set; }

            /// <summary>
            ///     Libellé de l'écriture.
            /// </summary>
            public string Label { get; set; }

            /// <summary>
            ///     Montant de l'écirutre.
            /// </summary>
            public decimal Value { get; set; }

            /// <summary>
            ///     Date de l'écriture
            /// </summary>
            public DateTime Date { get; set; }
        }

        /// <summary>
        ///     Données actuelles.
        /// </summary>
        BankAccountLineData _CurrentData;

        /// <summary>
        ///     Sauvegarde des données au début de l'édition.
        /// </summary>
        BankAccountLineData? _BackupData;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient ou définit l'identifiant du compte bancaire associé.
        /// </summary>
        public long IdentifierBankAccount
        { 
            get => this._CurrentData.IdentifierBankAccount;
            set => this.SetProperty(nameof(this.IdentifierBankAccount), () => this._CurrentData.IdentifierBankAccount, (v) => this._CurrentData.IdentifierBankAccount = v, value);
        }

        /// <summary>
        ///     Obtient ou définit l'identifiant de la catégorie associée.
        /// </summary>
        public long? IdentifierCategory
        {
            get => this._CurrentData.IdentifierCategory;
            set => this.SetProperty(nameof(this.IdentifierCategory), () => this._CurrentData.IdentifierCategory, (v) => this._CurrentData.IdentifierCategory = v, value);
        }

        /// <summary>
        ///     Obtient ou définit le compte bancaire associé.
        /// </summary>
        [JsonIgnore]
        public BankAccount BankAccount
        {
            get => this._CurrentData.BankAccount;
            set => this.SetProperty(nameof(this.BankAccount), () => this._CurrentData.BankAccount, (v) => this._CurrentData.BankAccount = v, value);
        }

        /// <summary>
        ///     Obtient ou définit la catégorie associée.
        /// </summary>
        [JsonIgnore]
        public Category Category
        {
            get => this._CurrentData.Category;
            set => this.SetProperty(nameof(this.Category), () => this._CurrentData.Category, (v) => this._CurrentData.Category = v, value);
        }

        /// <summary>
        ///     Obtient ou définit le libellé de l'écriture.
        /// </summary>
        public string Label
        {
            get => this._CurrentData.Label;
            set => this.SetProperty(nameof(this.Label), () => this._CurrentData.Label, (v) => this._CurrentData.Label = v, value);
        }

        /// <summary>
        ///     Obtient ou définit le montant de l'écirutre.
        /// </summary>
        public decimal Value
        {
            get => this._CurrentData.Value;
            set => this.SetProperty(nameof(this.Value), () => this._CurrentData.Value, (v) => this._CurrentData.Value = v, value);
        }

        /// <summary>
        ///     Obtient ou définit la date de l'écriture.
        /// </summary>
        public DateTime Date
        {
            get => this._CurrentData.Date;
            set => this.SetProperty(nameof(this.Date), () => this._CurrentData.Date, (v) => this._CurrentData.Date = v, value);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Commence l'édition d'une entité.
        /// </summary>
        public override void BeginEdit()
        {
            if (this._BackupData == null)
            {
                this._BackupData = this._CurrentData;
            }
        }

        /// <summary>
        ///     Annule les modifications effectuées sur l'entité.
        /// </summary>
        public override void CancelEdit()
        {
            if (this._BackupData != null)
            {
                this._CurrentData = this._BackupData.Value;
                this._BackupData = null;
                this.OnPropertyChanged("");
            }
        }

        /// <summary>
        ///     Valide les modifications effectuées sur l'entité.
        /// </summary>
        public override void EndEdit()
        {
            if (this._BackupData != null)
            {
                this._BackupData = null;
            }
        }

        #endregion
    }
}
