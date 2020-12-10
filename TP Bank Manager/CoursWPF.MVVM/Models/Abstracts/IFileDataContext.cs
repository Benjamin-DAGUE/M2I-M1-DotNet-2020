using System;
using System.Collections.Generic;
using System.Text;

namespace CoursWPF.MVVM.Models.Abstracts
{
    /// <summary>
    ///     Interface d'un contexte de données en fichier.
    /// </summary>
    public interface IFileDataContext : IDataContext
    {
        #region Properties

        /// <summary>
        ///     Obtient le chemin du fichier de données.
        /// </summary>
        string FilePath { get; }


        #endregion
    }
}
