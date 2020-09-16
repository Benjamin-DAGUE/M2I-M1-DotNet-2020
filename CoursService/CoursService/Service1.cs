using CoursService.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace CoursService
{
    public partial class Service1 : ServiceBase
    {
        private MonService _Service;

        public Service1()
        {
            InitializeComponent();
            this._Service = new MonService(@"C:\TMP\INPUT", "C:\\TMP\\OUTPUT");
        }

        protected override void OnStart(string[] args) => this._Service.Start();

        protected override void OnStop() => this._Service.Stop();

        protected override void OnPause() => this._Service.Pause();

        protected override void OnContinue() => this._Service.Continue();
    }
}
