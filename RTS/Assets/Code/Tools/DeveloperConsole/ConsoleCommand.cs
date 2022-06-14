using Code.Interfaces;
using UnityEngine;

namespace Code.Tools.DeveloperConsole
{
    public abstract class ConsoleCommand : ScriptableObject, IConsoleCommand
    {
        [SerializeField] private string commandWord = "";
        [SerializeField] private string commandTooltip = "";
        public string CommandWord => commandWord;
        public string ToolTip => commandTooltip;
        
        public abstract bool Process(string[] arguments);
    }
}
