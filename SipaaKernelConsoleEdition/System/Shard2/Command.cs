using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelConsoleEdition.System.Shard2
{
    public abstract class Command
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string Usage { get; }

        public abstract CommandResult Execute(Console c, List<string> args);
    }
}
