using SipaaGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel.Core
{
    public abstract class Application
    {
        public Graphics ApplicationIcon { get; set; }
        public string ApplicationDisplayName { get; set; } = "SipaaKernel Application";
        public string ApplicationName { get; set; } = "SipaaKernelApp";
        public string ApplicationPackageName { get; set; } = "fr.raphmar2019.sipaakernelapp";

        public Application()
        {
            InitAppInfo();
        }

        public abstract void InitAppInfo();
        public abstract void AppMain();
    }
}
