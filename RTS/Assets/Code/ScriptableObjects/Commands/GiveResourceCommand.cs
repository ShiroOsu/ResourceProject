using System;
using Code.Resources;
using Code.Tools.DeveloperConsole;
using Code.Tools.Enums;
using Code.Tools.HelperClasses;
using UnityEngine;

namespace Code.ScriptableObjects.Commands
 {
     [CreateAssetMenu(fileName = "GiveResource Command", menuName = "ScriptableObjects/Developer Console/GiveResource Command")]
     public class GiveResourceCommand : ConsoleCommand
     {
         public override bool Process(string[] arguments)
         {
             if (arguments.Length != 2)
             {
                 return CommandResponse.Instance.Failed(CommandWord, "Number of arguments does not match command!"
                                                                     + " Expected input: /Give ResourceType Integer");
             }

             if (!int.TryParse(arguments[1], out var value))
             {
                 return CommandResponse.Instance.Failed(CommandWord, 
                     $"Argument value type is invalid! Expected value type: Integer. Given type: {value.GetType()}");
             }

             var resourceString = arguments[0];
             
             ResourceType? type = null;
             foreach (var t in Extensions.GetValues<ResourceType>())
             {
                 if (t.ToString().Equals(resourceString, StringComparison.OrdinalIgnoreCase))
                 {
                     type = t;
                 }
             }
             
             switch (type)
             {
                 case ResourceType.Gold:
                     PlayerResources.Instance.AddResource(value);
                     return CommandResponse.Instance.Succeeded(CommandWord, $"Player was given {value} gold");
                 case ResourceType.Stone:
                     PlayerResources.Instance.AddResource(0, value);
                     return CommandResponse.Instance.Succeeded(CommandWord, $"Player was given {value} stone");
                 case ResourceType.Wood:
                     PlayerResources.Instance.AddResource(0, 0, value);
                     return CommandResponse.Instance.Succeeded(CommandWord, $"Player was given {value} wood");
                 case ResourceType.Food:
                     PlayerResources.Instance.AddResource(0, 0, 0, value);
                     return CommandResponse.Instance.Succeeded(CommandWord, $"Player was given {value} food");
                 case ResourceType.Units:
                     return CommandResponse.Instance.Failed(CommandWord, "Can not be given units!");
                 default:
                     return CommandResponse.Instance.Failed(CommandWord, $"The given resource type is not a valid!");
             }
         }
     }
 }