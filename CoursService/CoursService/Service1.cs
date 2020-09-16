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
            _Service = new MonService();
        }

        protected override void OnStart(string[] args) => _Service.Start();

        protected override void OnStop() => _Service.Stop();
    }
}
