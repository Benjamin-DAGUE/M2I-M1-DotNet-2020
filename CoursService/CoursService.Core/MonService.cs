using M2I.Diagnostics;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
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
                Loggers.WriteInformation("Starting service...");
                
                try
                {
                    //Créé le dossier d'entrée s'il n'existe pas.
                    Directory.CreateDirectory(this._InputFolderPath);
                    Directory.CreateDirectory(this._OutputFolderPath);
                }
                catch (Exception ex)
                {
                    Loggers.WriteError("Impossible de créer les dossiers");
                    Loggers.WriteError(ex.ToString());
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
                Loggers.WriteInformation("Service started.");
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
                Loggers.WriteInformation("Service paused.");
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
                Loggers.WriteInformation("Service resumed.");
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
                Loggers.WriteInformation("Service stopped.");
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
            try
            {
                //Copie du fichier créé dans la sortie.
                //File.Copy(e.FullPath, e.FullPath.Replace(this._InputFolderPath, this._OutputFolderPath));

                if (new string[] { ".png", ".jpg", ".jpeg" }.Contains(Path.GetExtension(e.FullPath)))
                {
                    Loggers.WriteInformation("Ouverture du fichier : " + e.FullPath);
                    //L'événement Created est appelé dès que le fichier est créé.
                    //Cependant, si le fichier est en cours d'écriture, il ne sera pas possible de l'ouvrir.
                    //Ici, on appel donc une méthode qui va se charger d'attendre que le fichier soit disponible puis ensuite l'ouvrir.
                    FileStream imageFileStream = OpenFileAndWaitIfNeeded(e.FullPath);

                    Loggers.WriteInformation("Redimenssionnement de l'image : " + e.FullPath);
                    //Redimenssionnement de l'image
                    ResizeAndCenterImage(imageFileStream, e.FullPath.Replace(this._InputFolderPath, this._OutputFolderPath));

                    Loggers.WriteInformation("Fermeture et suppression du fichier source : " + e.FullPath);
                    //On ferme le fichier puit on supprime le fichier dans le dossier Input
                    imageFileStream.Dispose();
                    File.Delete(e.FullPath);
                }
            }
            catch (Exception ex)
            {
                Loggers.WriteError("Impossible de traiter le fichier " + e.FullPath);
                Loggers.WriteError(ex.ToString());
            }
        }

        /// <summary>
        ///     Ouvre un fichier avec attente si le fichier n'est pas disponible.
        /// </summary>
        /// <param name="filePath">Chemin du fichier à ouvrir.</param>
        /// <returns>Flux du fichier ouvert.</returns>
        private FileStream OpenFileAndWaitIfNeeded(string filePath)
        {
            bool isFileBusy = true;
            FileStream fileStream = null;

            DateTime startDateTime = DateTime.Now;

            do
            {
                try
                {
                    fileStream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                    isFileBusy = false; //Si on arrive à ouvrir, le fichier est accessible
                }
                catch (IOException ex)
                {
                    //Si on a une erreur d'IO, c'est que le fichier est encore ouvert
                    System.Threading.Thread.Sleep(200);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erreur à l'ouverture du fichier", ex);
                }

                if (DateTime.Now > startDateTime.AddMinutes(15))
                {
                    throw new Exception("Délai d'attente dépassé, impossible d'ouvrir le fichier.");
                }

            } while (isFileBusy);

            return fileStream;
        }

        /// <summary>
        /// Redimensionne l'image en centrant l'image source puis sauvegarde une copie dans le chemin spécifié.
        /// </summary>
        /// <param name="imageFileStream">Image source à redimensionner.</param>
        /// <param name="outputFilePath">Chemin du fichier de sortie.</param>
        private static void ResizeAndCenterImage(FileStream imageFileStream, string outputFilePath)
        {
            //Création d'un objet System.Drawing.Image depuis le flux du fichier.
            using (Image image = Image.FromStream(imageFileStream)) //Ouverture de l'image
            {
                //On traite uniquement si l'image n'a pas la même hauteur / largeur.
                if (image.Width != image.Height)
                {
                    //On recherche si c'est la largeur la plus grande.
                    bool isWidthLarger = image.Width > image.Height;
                    //On récupère la valeur du plus grand côté et la valeur du plus petit côté.
                    int largerSize = isWidthLarger ? image.Width : image.Height;
                    int smallerSize = isWidthLarger ? image.Height : image.Width;
                    //On calcul la position à laquelle on va positionner la nouvelle 
                    int position = (largerSize - smallerSize) / 2;

                    //On créé un point pour la position de l'image.
                    Point imagePoint = isWidthLarger ? new Point(0, position) : new Point(position, 0);

                    using (Bitmap resizedImage = new Bitmap(image, largerSize, largerSize)) //Création d'une nouvelle image à la bonne dimension
                    {
                        //DPI = Dot Per Inch, c'est le nombre de pixel par pouce.
                        //Par exemple, une image de 300 pixel en 96 DPI fera 300px / 95dpi = 3.125inch, sachant que 1 inch = 2.54cm.
                        //Il est important de reprendre les DPI d'origines pour conserver proprement les proportions de l'image.
                        resizedImage.SetResolution(image.HorizontalResolution, image.VerticalResolution); //On reprend les DPI d'origines

                        using (Graphics graphics = Graphics.FromImage(resizedImage)) //Graphics permet de modifier l'image
                        {
                            graphics.Clear(Color.Transparent);      //On met un fond transparent
                            graphics.DrawImage(image, imagePoint);  //On copie l'image d'origine à la bonne position
                        } //Après utilisation, on Dispose le Graphics avec le using.

                        resizedImage.Save(outputFilePath); //Sauvegarde du résultat dans le fichier output.
                    }
                }
                else
                {
                    image.Save(outputFilePath); //Si Width = Height, on sauvegarde l'image d'origine.
                }
            }
        }

        #endregion
    }
}
