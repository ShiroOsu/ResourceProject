using Code.Tools.DeveloperConsole;
using UnityEngine;

namespace Code.ScriptableObjects.Commands
 {
     [CreateAssetMenu(fileName = "Help Command", menuName = "ScriptableObjects/Developer Console/Help Command")]
     public class HelpCommand : ConsoleCommand
     {
         public override bool Process(string[] arguments)
         {
             var commands = DeveloperConsoleBehaviour.Instance.commands;
             Debug.Log("Available Commands: ");
             foreach (var command in commands)
             {
                 Debug.Log(" - " + command.CommandWord + ": " + command.ToolTip);
             }
             Debug.Log(" ----------------------------------------------------------------------------------- ");
             return CommandResponse.Instance.Succeeded(CommandWord, "Listed current available commands");
         }
     }
 }