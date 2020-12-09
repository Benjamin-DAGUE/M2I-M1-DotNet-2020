using CoursWPF.MVVM.ViewModels.Abstracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursWPF.MVVM.ViewModels
{
    public abstract class ViewModelList<T> : ObservableObject, IViewModelList<T>
        where T : new()
    {
        #region Fields

        /// <summary>
        ///     Liste des éléments.
        /// </summary>
        private ObservableCollection<T> _ItemsSource;

        /// <summary>
        ///     Élémént sélectionné.
        /// </summary>
        private T _SelectedItem;

        /// <summary>
        ///     Commande pour ajouter une élément.
        /// </summary>
        private RelayCommand _AddItem;

        /// <summary>
        ///     Commande pour supprimer l'élément sélectionné.
        /// </summary>
        private RelayCommand _DeleteItem;

        #endregion

        #region Properties

        /// <summary>
        ///     Obtient la liste des éléments.
        /// </summary>
        public ObservableCollection<T> ItemsSource
        {
            get => this._ItemsSource;
            private set => this.SetProperty(nameof(this.ItemsSource), ref this._ItemsSource, value);
        }

        /// <summary>
        ///     Obtient ou définit l'élément sélectionné.
        /// </summary>
        public T SelectedItem
        {
            get => this._SelectedItem;
            set => this.SetProperty(nameof(this.SelectedItem), ref this._SelectedItem, value);
        }

        /// <summary>
        ///     Obtient la commande pour ajouter un élément.
        /// </summary>
        public RelayCommand AddItem => this._AddItem;

        /// <summary>
        ///     Obtient la commande pour supprimer élément.
        /// </summary>
        public RelayCommand DeleteItem => this._DeleteItem;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialise une nouvelle instance de la classe <see cref="MainViewModel"/>.
        /// </summary>
        public ViewModelList()
        {
            this._AddItem = new RelayCommand(this.ExecuteAddItem, this.CanExecuteAddItem);
            this._DeleteItem = new RelayCommand(this.ExecuteDeleteItem, this.CanExecuteDeleteItem);

            this._ItemsSource = new ObservableCollection<T>();
        }

        #endregion

        #region Methods

        #region AddItem

        protected virtual bool CanExecuteAddItem(object param) => true;

        protected virtual void ExecuteAddItem(object param)
        {
            T p = new T();
            this.ItemsSource.Add(p);
            this.SelectedItem = p;
        }

        #endregion

        #region DeleteItem

        protected virtual bool CanExecuteDeleteItem(object param) => param is T || (param == null && this.SelectedItem != null);

        protected virtual void ExecuteDeleteItem(object param)
        {
            if (param is T item)
            {
                this.ItemsSource.Remove(item);
            }
            else
            {
                this.ItemsSource.Remove(this.SelectedItem);
            }
        }

        #endregion

        #endregion
    }
}
