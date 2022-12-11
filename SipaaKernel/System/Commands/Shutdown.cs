using SipaaKernel.System.Shard2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel.System.Commands
{
    internal class Shutdown : Command
    {
        public override string Name => "shutdown";

        public override string Description => "Shutdown SipaaKernel";

        public override string Usage => "shutdown";

        public override CommandResult Execute(Console c, List<string> args)
        {
            c.Clear();
            c.WriteLine("Goodbye!");
            Cosmos.HAL.Global.PIT.Wait(3000);
            Cosmos.System.Power.Shutdown();
            return CommandResult.Sucess;
        }
    }
}
