using System.Collections.Generic;

namespace SipaaKernel.System.Shard2
{
    public abstract class Command
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string Usage { get; }

        public abstract CommandResult Execute(List<string> args);
    }
}
