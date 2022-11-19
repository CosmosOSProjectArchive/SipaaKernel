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
using System.IO;

namespace SipaaKernel
{
    public class Kernel : Sys.Kernel
    {
        VBECanvas c;
        Button b, b2;
        Setup s;

        /// <summary>
        /// Create a file to describe a kernel panic and reboot.
        /// </summary>
        public static void KernelPanic(uint error)
        {
            File.Create(@"0:\SKPANIC.DAT");
            File.WriteAllText(@"0:\SKPANIC.DAT", $"{error}");
            Sys.Power.Reboot();
        }

        protected override void BeforeRun()
        {
            // Init graphics
            c = new();

            // Verify if a kernel panic happened at the last session.
            if (File.Exists(@"0:\SKPANIC.DAT"))
            {
                c.Clear(Color.Black);
                c.DrawImage((int)c.Width / 2 - (int)Assets.KernelPanicBitmap.Width / 2, (int)c.Height / 2 - (int)Assets.KernelPanicBitmap.Height / 2, Assets.KernelPanicBitmap, false);
                c.DrawString(10, 10,
                    $"{OSInfo.OSName} {OSInfo.OSVersion} (build {OSInfo.OSBuild})\n" +
                    $"{CPU.GetCPUBrandString()} with {CPU.GetAmountOfRAM()}mb memory.\n" +
                    $"Kernel panic error code : {File.ReadAllText(@"0:\SKPANIC.DAT")}", Font.Fallback, Color.White);
                c.Update();
                Console.ReadKey();
        
            }

            // Init boot screen
            Sys.MouseManager.ScreenWidth = VBE.getModeInfo().width;
            Sys.MouseManager.ScreenHeight = VBE.getModeInfo().height;
            c.DrawImage((int)c.Width / 2 - (int)Assets.BootBitmap.Width / 2, (int)c.Height / 2 - (int)Assets.BootBitmap.Height / 2, Assets.BootBitmap, false);
            c.Update();

            // Run default SipaaKernel boot routines (audio, network & file system)
            Core.Global.Boot(false);

            // Check if SipaaKernel needs to be installed
            /**if (!SipaaKernelInstallationManager.IsInstalled)
            {
                s = new();
            }**/

            // Resize the wallpaper & Init a button
            Assets.Wallpaper = Assets.Wallpaper.Scale(VBE.getModeInfo().width, VBE.getModeInfo().height);
            b = new();
            b.Width = 150;
            b.Height = 40;
            b.X = 40;
            b.Y = 40;
            b.Text = "Shutdown";
            b.OnClick = (x, y) =>
            {
                Sys.Power.Shutdown();
            };

            b2 = new();
            b2.Width = 150;
            b2.Height = 40;
            b2.X = 40;
            b2.Y = 90;
            b2.Text = "KPanic";
            b2.OnClick = (x, y) =>
            {
                KernelPanic(0x00);
            };

            Cosmos.HAL.Global.PIT.Wait(10000);
        }

        protected override void Run()
        {
            try
            {
                //c.Clear(Color.Black);
                c.DrawImage(0, 0, Assets.Wallpaper, false);
                b.OnDraw(c);
                b.OnUpdate();
                b2.OnDraw(c);
                b2.OnUpdate();
                c.DrawString(10, (int)c.Height - 34, $"{c.GetFPS()} FPS", Font.Fallback, Color.White);
                c.DrawFilledRectangle((int)Sys.MouseManager.X, (int)Sys.MouseManager.Y, 8, 12, 0, Color.White);
                c.Update();
            }catch (Exception e)
            {
                KernelPanic(0x192833);
            }
        }
    }
}
