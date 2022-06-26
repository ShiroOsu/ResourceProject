 using Code.Tools.DeveloperConsole;
 using UnityEngine;

 namespace Code.ScriptableObjects.Commands
 {
     [CreateAssetMenu(fileName = "Dev Command", menuName = "ScriptableObjects/Developer Console/Dev Command")]
     public class DevCommand : ConsoleCommand
     {
         public override bool Process(string[] arguments)
         {
             return CommandResponse.Instance.Succeeded(CommandWord);
         }
     }
 }