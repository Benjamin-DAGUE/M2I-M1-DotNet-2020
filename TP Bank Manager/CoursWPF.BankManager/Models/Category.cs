using CoursWPF.MVVM.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace CoursWPF.BankManager.Models
{
    /// <summary>
    ///     Classe de données qui représente une catégorie d'écriture.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class Category : Entity
    {
        #region Fields

        /// <summary>
        ///     Structure des données de la classe <see cref="Category"/>.
        /// </summary>
        private struct CategoryData
        {
            /// <summary>
            ///     Nom du compte.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            ///     Collection observable des écritures.
            /// </summary>
            public ObservableCollection<BankAccountLine> BankAccountLines { get; set; }
        }

        /// <summary>
        ///     Données actuelles.
        /// </summary>
        CategoryData _CurrentData;

        /// <summary>
        ///     Sauvegarde des données au début de l'édition.
        /// </summary>
        CategoryData? _BackupData;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient ou définit le nom du compte.
        /// </summary>
        public string Name 
        { 
            get => this._CurrentData.Name;
            set => this.SetProperty(nameof(this.Name), () => this._CurrentData.Name, (v) => this._CurrentData.Name = v, value);
        }

        /// <summary>
        ///     Obtient ou définit le numéro du compte.
        /// </summary>
        [JsonIgnore]
        public ObservableCollection<BankAccountLine> BankAccountLines
        {
            get => this._CurrentData.BankAccountLines;
            private set => this.SetProperty(nameof(this.BankAccountLines), () => this._CurrentData.BankAccountLines, (v) => this._CurrentData.BankAccountLines = v, value);
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="Category"/>.
        /// </summary>
        public Category()
        {
            this.BankAccountLines = new ObservableCollection<BankAccountLine>();
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
