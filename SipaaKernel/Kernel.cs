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
using SipaaKernel.UI.Widgets;

namespace SipaaKernel
{
    public class Kernel : Sys.Kernel
    {
        VBECanvas c;
        Button b;
        Setup s;

        protected override void BeforeRun()
        {
            // Init bootscreen
            c = new();
            Sys.MouseManager.ScreenWidth = VBE.getModeInfo().width;
            Sys.MouseManager.ScreenHeight = VBE.getModeInfo().height;
            c.DrawImage((int)c.Width / 2 - (int)Assets.BootBitmap.Width / 2, (int)c.Height / 2 - (int)Assets.BootBitmap.Height / 2, Assets.BootBitmap, false);
            c.Update();

            // Run default SipaaKernel boot routines (audio, network & file system)
            Core.Global.Boot(false);

            // Check if SipaaKernel needs to be installed
            if (!SipaaKernelInstallationManager.IsInstalled)
            {
                s = new();
            }

            // Resize the wallpaper & Init a button
            Assets.Wallpaper = Assets.Wallpaper.Scale(VBE.getModeInfo().width, VBE.getModeInfo().height);
            b = new();
            b.Width = 150;
            b.Height = 40;
            b.X = 30;
            b.Y = 30;
            b.Text = "Shutdown";
            b.OnClick = (x, y) =>
            {
                Sys.Power.Shutdown();
            };

            Cosmos.HAL.Global.PIT.Wait(10000);
        }

        protected override void Run()
        {
            //c.Clear(Color.Black);
            c.DrawImage(0, 0, Assets.Wallpaper, false);
            if (s != null)
            {
                s.Draw(c);
                s.Update(c);
            }
            else
            {
                b.OnDraw(c);
                b.OnUpdate();
            }
            c.DrawFilledRectangle((int)Sys.MouseManager.X, (int)Sys.MouseManager.Y, 8, 12, 0, Color.White);
            c.DrawString(10, 10, $"{c.GetFPS()} FPS", Font.Fallback, Color.White);
            c.Update();
        }
    }
}
