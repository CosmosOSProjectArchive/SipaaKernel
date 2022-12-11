using Cosmos.Core;
using SipaaKernel.Core;
using SipaaKernel.System.Shard2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel.System.Commands
{
    public class SysInfo : Command
    {
        public override string Name => "sysinfo";

        public override string Description => "Get infos about your computer & operating system";

        public override string Usage => "sysinfo";

        public override CommandResult Execute(Console Console, List<string> args)
        {
            Console.WriteLine($"Operating System : {OSInfo.OSName} version {OSInfo.OSVersion} (build {OSInfo.OSBuild})");
            Console.WriteLine($"Processor : {CPU.GetCPUBrandString}");
            Console.WriteLine($"Installed RAM : {CPU.GetAmountOfRAM}mb");
            return CommandResult.Sucess;
        }
    }
}
