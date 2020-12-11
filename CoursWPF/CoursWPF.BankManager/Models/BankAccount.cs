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
    [JsonObject(MemberSerialization.OptOut)]
    public class BankAccount : ObservableObject
    {
        #region Fields

        private Guid _Identifier;

        private string _Name;

        private string _Number;

        private ObservableCollection<BankAccountLine> _BankAccountLines;

        #endregion

        #region Properties

        public Guid Identifier
        {
            get => this._Identifier;
            set => this.SetProperty(nameof(this.Identifier), ref this._Identifier, value);
        }

        public string Name 
        {
            get => this._Name;
            set => this.SetProperty(nameof(this.Name), ref this._Name, value);
        }

        public string Number
        {
            get => this._Number;
            set => this.SetProperty(nameof(this.Number), ref this._Number, value);
        }

        [JsonIgnore]
        public ObservableCollection<BankAccountLine> BankAccountLines
        {
            get => this._BankAccountLines;
            set => this.SetProperty(nameof(this.BankAccountLines), ref this._BankAccountLines, value);
        }

        #endregion

        #region Constructors

        public BankAccount()
        {
            this._BankAccountLines = new ObservableCollection<BankAccountLine>();
        }

        #endregion
    }
}
