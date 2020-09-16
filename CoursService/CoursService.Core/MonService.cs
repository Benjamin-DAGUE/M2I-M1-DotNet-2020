using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursService.Core
{
    public class MonService
    {
        #region Methods

        public void Start()
        {
            System.Threading.Thread.Sleep(15000);


            File.AppendAllText("C:\\log.txt", "Starting service..." + Environment.NewLine);
            File.AppendAllText("C:\\log.txt", "Service started" + Environment.NewLine);

        }

        public void Stop()
        {
            File.AppendAllText("C:\\log.txt", "Service stopped" + Environment.NewLine);
        }

        #endregion
    }
}
