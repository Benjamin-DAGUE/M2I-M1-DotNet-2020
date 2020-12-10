using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursWPF.FirstApp.Models
{
    public interface IDataStore
    {
        /// <summary>
        ///     Obtient la liste des personnes.
        /// </summary>
        ObservableCollection<Person> People { get; }

        /// <summary>
        ///     Obtient la liste des véhicules.
        /// </summary>
        ObservableCollection<Vehicule> Vehicules { get; }

        /// <summary>
        ///     Obtient le chemin du fichier de données.
        /// </summary>
        string FilePath { get; }

        /// <summary>
        ///     Sauvegarde le jeux de données dans un fichier.
        /// </summary>
        void Save();
    }
}
