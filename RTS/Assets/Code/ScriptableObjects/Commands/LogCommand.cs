using Code.Tools.DeveloperConsole;
using UnityEngine;

namespace Code.ScriptableObjects.Commands
 {
     [CreateAssetMenu(fileName = "Log Command", menuName = "ScriptableObjects/Developer Console/Log Command")]
     public class LogCommand : ConsoleCommand
     {
         public override bool Process(string[] arguments)
         {
             var logText = string.Join(" ", arguments);
             Debug.Log(logText);
             
             return CommandResponse.Instance.Succeeded(CommandWord);
         }
     }
 }