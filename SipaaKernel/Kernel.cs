using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using SipaaKernel.Core;
using PrismGL3D;
using PrismGL2D.Extentions;
using PrismGL3D.Types;
using static Cosmos.HAL.PCIDevice;
using PrismGL2D;
using Cosmos.Core;

namespace SipaaKernel
{
    public class Kernel : Sys.Kernel
    {
        VBECanvas c;
        Engine e;
        Mesh cube;

        protected override void BeforeRun()
        {
            Core.Global.Boot(false);
            Console.Clear();
            Console.WriteLine("SipaaKernel booted successfully. Type a line of text to get it echoed back.");
            c = new();
            Sys.MouseManager.ScreenWidth = VBE.getModeInfo().width;
            Sys.MouseManager.ScreenHeight= VBE.getModeInfo().height;
        }

        protected override void Run()
        {
            c.Clear(Color.Black);
            c.DrawFilledRectangle((int)Sys.MouseManager.X, (int)Sys.MouseManager.Y, 8, 12, 0, Color.White);
            c.DrawString(10, 10, $"{c.GetFPS()} FPS", Font.Fallback, Color.White);
            c.Update();
        }
    }
}
