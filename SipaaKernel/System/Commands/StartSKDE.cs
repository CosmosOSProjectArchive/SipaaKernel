using SipaaKernel.System.Shard2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel.System.Commands
{
    public class StartSKDE : Command
    {
        public override string Name => "starts";

        public override string Description =>"Starts the SKDE desktop";

        public override string Usage => "startx";

        public override CommandResult Execute(Console c, List<string> args)
        {
            Kernel.isInGui = true;

            return CommandResult.NeedsUpdateMethodExit;
        }
    }
}
