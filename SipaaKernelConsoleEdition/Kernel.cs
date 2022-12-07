//using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
//using Cosmos.System.Graphics;
using PrismGL2D.Extentions;
using System.Drawing;
using Cosmos.System.Graphics.Fonts;
using SipaaKernelConsoleEdition.System;
using IL2CPU.API.Attribs;
using SipaaKernelConsoleEdition.System.Commands;
using SipaaKernelConsoleEdition.System.Shard2;

namespace SipaaKernelConsoleEdition
{
    public class Kernel : Sys.Kernel
    {
        public static VBECanvas canvas;
        public static Console Console;
        public static Font font;

        [ManifestResourceStream(ResourceName = "SipaaKernelConsoleEdition.consolefont.psf")]
        public static byte[] ConsoleFont;

        protected override void BeforeRun()
        {
            canvas = new();
            font = PCScreenFont.LoadFont(ConsoleFont);
            Console = new((uint)canvas.Width, (uint)canvas.Height);
            CommandRunner.Commands.Add(new HelloWorldCommand());
            Console.WriteLine("SipaaKernel booted successfully. Type a line of text to get it echoed back.");
            Console.BeforeCommand();
        }

        protected override void Run()
        {
            Console.Update();
            canvas.Update();
        }
    }
}
