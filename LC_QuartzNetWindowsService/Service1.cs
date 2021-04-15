using LC_Common;
using LC_QuartzNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace LC_QuartzNetWindowsService
{
    public partial class Service1 : ServiceBase
    {
        private Logger logger = new Logger(typeof(Service1));
        public Service1()
        {
            InitializeComponent();
            this.logger.Info($"This is Service1 ctor start...");

            DispatchManager.Init().GetAwaiter().GetResult();

            this.logger.Info($"This is Service1 ctor end...");
        }

        protected override void OnStart(string[] args)
        {
            this.logger.Info($"This is Service1 OnStart...");

        }

        protected override void OnStop()
        {
            this.logger.Info($"This Service1 is OnStop...");
        }
    }
}
