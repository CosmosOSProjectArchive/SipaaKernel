using Cosmos.System;
using Cosmos.System.Plugs.System.System;
using SipaaKernel.System.Shard2;
using System.Collections.Generic;

namespace SipaaKernel.System.Commands
{
    public class StartSKDE : Command
    {
        public override string Name => "starts";

        public override string Description => "Starts the SKDE desktop";

        public override string Usage => "startx";

        public override CommandResult Execute(List<string> args)
        {
            Kernel.isInGui = true;
            VBEConsole.IsCursorEnabled = false;
            return CommandResult.NeedsUpdateMethodExit;
        }
    }
}
