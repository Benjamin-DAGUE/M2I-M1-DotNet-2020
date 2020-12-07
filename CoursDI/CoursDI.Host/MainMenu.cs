using CoursDI.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursDI.Host
{
    class MainMenu
    {
        IEnumerable<I> _Modules;

        public MainMenu(IEnumerable<I> modules)
        {
            //La classe MainMenu
            _Modules = modules;

            foreach (I module in _Modules)
            {
                module.ShowMessage();
            }
        }
    }
}
