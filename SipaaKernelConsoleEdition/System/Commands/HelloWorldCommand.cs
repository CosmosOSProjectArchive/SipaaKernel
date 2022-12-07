using SipaaKernelConsoleEdition.System.Shard2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelConsoleEdition.System.Commands
{
    internal class HelloWorldCommand : Command
    {
        public override string Name => "HelloWorld";

        public override string Description => "Prints 'Hello World' and every arguments to the console.";

        public override string Usage => "HelloWorld [any args you want]";

        public override CommandResult Execute(Console c, List<string> args)
        {
            c.WriteLine("Hello World!");
            c.WriteLine("Arguments : ");
            foreach (string arg in args)
            {
                c.WriteLine(arg);
            }
            return CommandResult.Sucess;
        }
    }
}
