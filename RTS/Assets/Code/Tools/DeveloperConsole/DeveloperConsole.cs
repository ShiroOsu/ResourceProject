using System;
using System.Collections.Generic;
using System.Linq;
using Code.Interfaces;

namespace Code.Tools.DeveloperConsole
{
    public class DeveloperConsole
    {
        private readonly string m_Prefix;
        private readonly IEnumerable<IConsoleCommand> m_Commands;
    
        public DeveloperConsole(string prefix, IEnumerable<IConsoleCommand> commands)
        {
            m_Prefix = prefix;
            m_Commands = commands;
        }

        public void ProcessCommand(string inputValue)
        {
            if (!inputValue.StartsWith(m_Prefix))
                return;

            inputValue = inputValue.Remove(0, m_Prefix.Length);
            var inputSplit = inputValue.Split(' ');
            var commandInput = inputSplit[0];
            var arguments = inputSplit.Skip(1).ToArray();
            
            ProcessCommand(commandInput, arguments);
        }

        private void ProcessCommand(string commandInput, string[] arguments)
        {
            foreach (var command in m_Commands)
            {
                if (!commandInput.Equals(command.CommandWord, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (command.Process(arguments))
                {
                    return;
                }
            }
        }
    }
}