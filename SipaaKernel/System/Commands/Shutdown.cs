using SipaaKernel.System.Shard2;
using System;
using System.Collections.Generic;

namespace SipaaKernel.System.Commands
{
    internal class Shutdown : Command
    {
        public override string Name => "shutdown";

        public override string Description => "Shutdown SipaaKernel";

        public override string Usage => "shutdown";

        public override CommandResult Execute(List<string> args)
        {
            Console.Clear();
            Console.WriteLine("Goodbye!");
            Cosmos.HAL.Global.PIT.Wait(3000);
            Cosmos.System.Power.Shutdown();
            return CommandResult.Sucess;
        }
    }
}
