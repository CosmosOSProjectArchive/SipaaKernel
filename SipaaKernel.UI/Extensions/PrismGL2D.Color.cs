using Cosmos.System;
using PrismGL2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel.UI
{
    public static class ColorEx
    {
        /// <summary>
        /// This function will remove alpha if SipaaKernel is running on a virtual machine.
        /// </summary>
        /// <returns>The color (with alpha removed if running on a virtual machine)</returns>
        public static Color RemoveAlphaIfIsVM(this Color color)
        {
            if (VMTools.IsQEMU || VMTools.IsVMWare || VMTools.IsVirtualBox)
            {
                color.A = 255;
                return color;
            }
            return color;
        }
    }
}
