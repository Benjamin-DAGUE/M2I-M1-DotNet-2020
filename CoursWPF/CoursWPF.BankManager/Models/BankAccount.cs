using CoursWPF.MVVM;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursWPF.BankManager.Models
{
    /// <summary>
    ///     Représente un compte bancaire.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class BankAccount : ObservableObject
    {
        #region Fields

        /// <summary>
        ///     Identifiant du compte bancaire.
        /// </summary>
        private Guid _Identifier;

        /// <summary>
        ///     Nom du compte bancaire.
        /// </summary>
        private string _Name;

        /// <summary>
        ///     Numéro du compte bancaire.
        /// </summary>
        private string _Number;

        /// <summary>
        ///     Collection des lignes d'écritures.
        /// </summary>
        private ObservableCollection<BankAccountLine> _BankAccountLines;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient ou définit l'identifiant du compte bancaire.
        /// </summary>
        public Guid Identifier
        {
            get => this._Identifier;
            set => this.SetProperty(nameof(this.Identifier), ref this._Identifier, value);
        }

        /// <summary>
        ///     Obtient ou définit le nom du compte bancaire.
        /// </summary>
        public string Name 
        {
            get => this._Name;
            set => this.SetProperty(nameof(this.Name), ref this._Name, value);
        }

        /// <summary>
        ///     Obtient ou définit le numéro du compte bancaire.
        /// </summary>
        public string Number
        {
            get => this._Number;
            set => this.SetProperty(nameof(this.Number), ref this._Number, value);
        }

        /// <summary>
        ///     Obtient ou définit la collection des lignes d'écritures.
        /// </summary>
        [JsonIgnore]
        public ObservableCollection<BankAccountLine> BankAccountLines
        {
            get => this._BankAccountLines;
            set => this.SetProperty(nameof(this.BankAccountLines), ref this._BankAccountLines, value);
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="BankAccount"/>.
        /// </summary>
        public BankAccount()
        {
            this._BankAccountLines = new ObservableCollection<BankAccountLine>();
        }

        #endregion
    }
}
