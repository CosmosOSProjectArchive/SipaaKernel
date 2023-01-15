using System.Collections.Generic;

namespace SipaaKernel.System.Shard2
{
    public class CommandRunner
    {
        public static List<Command> Commands = new List<Command>();

        public static CommandResult FindAndRunCommand(object c, string commandLine)
        {
            // Get arguments
            string[] args = commandLine.Split(' ');
            List<string> argsList = new List<string>();

            for (int i = 1; i < args.Length; i++)
            {
                argsList.Add(args[i]);
            }
            // Find the command and run it
            Command cmdToRun = null;
            bool finded = false;

            foreach (Command cmd in Commands)
            {
                if (cmd.Name.ToLower() == args[0].ToLower())
                {
                    finded = true;
                    cmdToRun = cmd;
                    break;
                }
            }

            if (finded)
            {
                return cmdToRun.Execute(argsList);
            }
            else
            {
                return CommandResult.NotFinded;
            }
        }
    }
}
