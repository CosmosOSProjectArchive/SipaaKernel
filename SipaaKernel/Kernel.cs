using Cosmos.Core;
using Cosmos.System.Plugs.System.System;
using SipaaGL;
using SipaaGL.Extentions;
using SipaaKernel.Core;
using SipaaKernel.System.Commands;
using SipaaKernel.System.CoreApps;
using SipaaKernel.System.Shard2;
using SipaaKernel.UI;
using System;
using System.IO;
using Sys = Cosmos.System;

namespace SipaaKernel
{
    public class Kernel : Sys.Kernel
    {
        public static VBECanvas c;
        public static bool isInGui = false;
        static SKDE skde;

        /// <summary>
        /// Show bugcheck screen (also called kernel panic)
        /// </summary>
        public static void KernelPanic(uint error)
        {
            c.Clear(Color.Black);
            c.DrawImage((int)c.Width / 2 - (int)Assets.KernelPanicBitmap.Width / 2, (int)c.Height / 2 - (int)Assets.KernelPanicBitmap.Height / 2, Assets.KernelPanicBitmap, false);
            c.DrawStringBF(10, 10,
                $"{OSInfo.OSName} {OSInfo.OSVersion} (build {OSInfo.OSBuild})\n" +
                $"{CPU.GetCPUBrandString()} with {CPU.GetAmountOfRAM()}mb memory.\n" +
                $"Kernel panic error code : {File.ReadAllText(@"0:\SKPANIC.DAT")}", BitFont.Fallback, Color.White);
            c.Update();
            global::System.Console.ReadKey();
            Sys.Power.Reboot();
        }

        protected override void BeforeRun()
        {
            // Bootscreen initialization
            Console.Clear();
            c = VBEConsole.Canvas;

            Sys.MouseManager.ScreenWidth = VBE.getModeInfo().width;
            Sys.MouseManager.ScreenHeight = VBE.getModeInfo().height;
            c.DrawImage((int)c.Width / 2 - (int)Assets.BootBitmap.Width / 2, (int)c.Height / 2 - (int)Assets.BootBitmap.Height / 2, Assets.BootBitmap, false);
            c.Update();

            Console.WriteLine($"SipaaKernel V5 Public Beta (version {OSInfo.OSVersion}, build {OSInfo.OSName}\n{CPU.GetAmountOfRAM()}mb of memory on a {CPU.GetCPUBrandString()}");
            // Start SipaaKernel core
            Core.Global.Boot();

            // GUI initialization
            skde = new();
            skde.AddAppToLauncher(new SiPaintApp());

            // Wait
            Cosmos.HAL.Global.PIT.Wait(5000);

            Console.Clear();

            // Console & console commands initialization
            Console.WriteLine($"SipaaKernel V5 Public Beta (Console Mode Only) (Version {OSInfo.OSVersion}, Build {OSInfo.OSBuild})");
            Console.WriteLine("Happy new year, SipaaKernel user!");

            VBEConsole.Init();

            CommandRunner.Commands.Add(new SysInfo());
            CommandRunner.Commands.Add(new Shutdown());
            CommandRunner.Commands.Add(new StartSKDE());

        }

        protected override void Run()
        {
            try
            {
                if (isInGui)
                {
                    c.DrawImage(0, 0, Assets.Wallpaper, false);
                    c.DrawStringBF(10, (int)c.Height - 112, $"{c.GetFPS()} FPS (This build has been compiled using SipaaGL)\nSipaaKernel Public Beta (Build " + OSInfo.OSBuild + ")", BitFont.Fallback, Color.White);

                    foreach (var w in WindowManager.Windows)
                    {
                        w.Draw(c);
                        w.Update();
                    }

                    skde.Draw(c);
                    skde.Update();

                    c.DrawFilledRectangle((int)Sys.MouseManager.X, (int)Sys.MouseManager.Y, 8, 12, 0, Color.White);
                    c.Update();

                }
                else
                {
                    Console.Write("shard@skuser:> ");
                    var input = Console.ReadLine();
                    var cmdresult = System.Shard2.CommandRunner.FindAndRunCommand(null, input);
                    if (cmdresult == CommandResult.NotFinded)
                    {
                        Console.WriteLine("The command than you typed can't be found.");
                    }
                    else if (cmdresult == CommandResult.Error)
                    {
                        Console.WriteLine("The command you runned ran into an error.");
                    }
                    else if (cmdresult == CommandResult.InvalidArgs)
                    {
                        Console.WriteLine("The command has been found but the arguments is invalid.");
                    }
                    else if (cmdresult == CommandResult.Fatal)
                    {
                        Console.WriteLine("The command you runned ran into an fatal error.");
                        Console.WriteLine("The system needs to be rebooted.");
                        Console.WriteLine("Press any key to reboot.");
                        Console.ReadKey();
                        Cosmos.System.Power.Reboot();
                    }
                    else if (cmdresult == CommandResult.NeedsUpdateMethodExit)
                    {
                        return;
                    }
                }
            }
            catch (global::System.Exception e)
            {
                KernelPanic(0x192833);
            }
        }
    }
}
