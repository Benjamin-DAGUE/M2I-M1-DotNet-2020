using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursService.Core
{
    /// <summary>
    ///     Service de redimensionnement d'image.
    ///     Écoute un répertoire d'entrée et fait une copie dans le répertoire de sortie spécifiée en redimenssionant l'image en carré.
    /// </summary>
    public class MonService
    {
        #region Fields

        /// <summary>
        ///     Chemin du dossier d'entrée.
        /// </summary>
        private readonly string _InputFolderPath;
        
        /// <summary>
        ///     Chemin du dossier de sortie.
        /// </summary>
        private readonly string _OutputFolderPath;

        /// <summary>
        ///     Classe d'écoute du répertoire d'entrée.
        /// </summary>
        private FileSystemWatcher _Watcher;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="MonService"/>.
        /// </summary>
        /// <param name="inputFolderPath">Chemin du répertoire d'entrée.</param>
        /// <param name="outputFolderPath">Chemin du répertoire de sortie.</param>
        public MonService(string inputFolderPath, string outputFolderPath)
        {
            //this._InputFolderPath = inputFolderPath ?? throw new ArgumentNullException(nameof(inputFolderPath));
            this._InputFolderPath = !string.IsNullOrWhiteSpace(inputFolderPath) ?
                                        inputFolderPath : throw new ArgumentNullException(nameof(inputFolderPath));
            this._OutputFolderPath = !string.IsNullOrWhiteSpace(outputFolderPath) ?
                                        outputFolderPath : throw new ArgumentNullException(nameof(outputFolderPath));
        }

        #endregion

        #region Methods

        #region Service

        /// <summary>
        ///     Démarre le service.
        /// </summary>
        public void Start()
        {
            if (this._Watcher == null)
            {
                File.AppendAllText("C:\\log.txt", "Starting service..." + Environment.NewLine);
                
                try
                {
                    //Créé le dossier d'entrée s'il n'existe pas.
                    Directory.CreateDirectory(this._InputFolderPath);
                    Directory.CreateDirectory(this._OutputFolderPath);
                }
                catch (Exception ex)
                {
                    //En cas d'erreur, on log l'exception.
                    File.AppendAllText("C:\\log.txt", ex.ToString() + Environment.NewLine);
                    //On relance l'exception pour arrêter le démarrage du service.
                    throw new Exception("Impossible de créer le dossier d'entrée ou de sortie", ex);
                }

                //On créer une instace d'un watcher sur le dossier d'entrée.
                this._Watcher = new FileSystemWatcher(this._InputFolderPath);
                //Permet de surveiller les sous-dossiers.
                //this._Watcher.IncludeSubdirectories = true;
                //On s'abonne à l'événement Created du Watcher.
                this._Watcher.Created += this.Watcher_Created;

                //On démarre l'écoute.
                this._Watcher.EnableRaisingEvents = true;
                File.AppendAllText("C:\\log.txt", "Service started." + Environment.NewLine);
            }
        }

        /// <summary>
        ///     Met en pause l'exécution du service.
        /// </summary>
        public void Pause()
        {
            if (this._Watcher != null && this._Watcher.EnableRaisingEvents)
            {
                this._Watcher.EnableRaisingEvents = false;
                File.AppendAllText("C:\\log.txt", "Service paused." + Environment.NewLine);
            }
        }

        /// <summary>
        ///     Reprend l'exécution du service.
        /// </summary>
        public void Continue()
        {
            if (this._Watcher != null && !this._Watcher.EnableRaisingEvents)
            {
                this._Watcher.EnableRaisingEvents = true;
                File.AppendAllText("C:\\log.txt", "Service resumed." + Environment.NewLine);
            }
        }

        /// <summary>
        ///     Arrête l'exécution du service.
        /// </summary>
        public void Stop()
        {
            if (this._Watcher != null)
            {
                this._Watcher.Created -= this.Watcher_Created;
                this._Watcher.Dispose();
                this._Watcher = null;
                File.AppendAllText("C:\\log.txt", "Service stopped" + Environment.NewLine);
            }
        }

        #endregion

        /// <summary>
        ///     Méthode déclenchée lors de l'ajout d'un fichier dans le répertoire d'entrée.
        /// </summary>
        /// <param name="sender">Instance qui a déclenchée l'événement.</param>
        /// <param name="e">Arguments de l'événements.</param>
        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            //Copie du fichier créé dans la sortie.
            File.Copy(e.FullPath, e.FullPath.Replace(this._InputFolderPath, this._OutputFolderPath));
        }

        #endregion
    }
}
