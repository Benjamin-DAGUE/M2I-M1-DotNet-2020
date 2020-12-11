using CoursWPF.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursWPF.BankManager.Models
{
    /// <summary>
    ///     Représente une catégorie.
    /// </summary>
    public class Category : ObservableObject
    {
        #region Fields

        /// <summary>
        ///     Identifiant de la catégorie.
        /// </summary>
        private Guid _Identifier;
        
        /// <summary>
        ///     Libellé de la catégorie.
        /// </summary>
        private string _Label;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient l'identifiant de la catégorie.
        /// </summary>
        public Guid Identifier
        {
            get => this._Identifier;
            set => this.SetProperty(nameof(this.Identifier), ref this._Identifier, value);
        }

        /// <summary>
        ///     Obtient le libellé de la catégorie.
        /// </summary>
        public string Label
        {
            get => this._Label;
            set => this.SetProperty(nameof(this.Label), ref this._Label, value);
        }

        #endregion

    }
}
