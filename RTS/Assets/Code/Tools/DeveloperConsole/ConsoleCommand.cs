using Code.Interfaces;
using UnityEngine;

namespace Code.Tools.DeveloperConsole
{
    public abstract class ConsoleCommand : ScriptableObject, IConsoleCommand
    {
        [SerializeField] private string commandWord = "";
        [Multiline(5)][SerializeField] private string tooltip = "";
        public string CommandWord => commandWord;
        public string ToolTip => tooltip;

        public abstract bool Process(string[] arguments);
    }
}
