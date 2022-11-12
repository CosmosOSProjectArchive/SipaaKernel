using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using SipaaKernel.Core;
namespace SipaaKernel
{
    public class Kernel : Sys.Kernel
    {

        protected override void BeforeRun()
        {
            Global.Boot(true);
            Console.Clear();
            Console.WriteLine("SipaaKernel booted successfully. Type a line of text to get it echoed back.");
        }

        protected override void Run()
        {
            Console.Write("Input: ");
            var input = Console.ReadLine();
            Console.Write("Text typed: ");
            Console.WriteLine(input);
        }
    }
}
