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
using SipaaKernel.UI;

namespace SipaaKernel
{
    public class Kernel : Sys.Kernel
    {
        static VBECanvas c;
        static SKDE skde;

        /// <summary>
        /// Show bugcheck screen (also called kernel panic)
        /// </summary>
        public static void KernelPanic(uint error)
        {
            c.Clear(Color.Black);
            c.DrawImage((int)c.Width / 2 - (int)Assets.KernelPanicBitmap.Width / 2, (int)c.Height / 2 - (int)Assets.KernelPanicBitmap.Height / 2, Assets.KernelPanicBitmap, false);
            c.DrawString(10, 10,
                $"{OSInfo.OSName} {OSInfo.OSVersion} (build {OSInfo.OSBuild})\n" +
                $"{CPU.GetCPUBrandString()} with {CPU.GetAmountOfRAM()}mb memory.\n" +
                $"Kernel panic error code : {File.ReadAllText(@"0:\SKPANIC.DAT")}", Font.Fallback, Color.White);
            c.Update();
            Console.ReadKey();
            Sys.Power.Reboot();
        }

        protected override void BeforeRun()
        {
            // Init graphics
            c = new();

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
            skde = new();
            skde.AddAppToLauncher(new TestApp());
            Cosmos.HAL.Global.PIT.Wait(10000);
        }

        protected override void Run()
        {
            try
            {

                c.DrawImage(0, 0, Assets.Wallpaper, false);

                foreach (var w in WindowManager.Windows)
                {
                    w.Draw(c);
                    w.Update();
                }

                skde.Draw(c);
                skde.Update();

                c.DrawString(10, (int)c.Height - 64, $"{c.GetFPS()} FPS", Font.Fallback, Color.White);
                c.DrawFilledRectangle((int)Sys.MouseManager.X, (int)Sys.MouseManager.Y, 8, 12, 0, Color.White);
                c.Update();
            }catch (Exception e)
            {
                KernelPanic(0x192833);
            }
        }
    }
}
