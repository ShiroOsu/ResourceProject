 using Code.Camera;
 using Code.Managers;
 using Code.Tools.DeveloperConsole;
 using Code.Tools.HelperClasses;
 using UnityEngine;

 namespace Code.ScriptableObjects.Commands
 {
     [CreateAssetMenu(fileName = "Camera Command", menuName = "ScriptableObjects/Developer Console/Camera Command")]
     public class CameraCommand : ConsoleCommand
     {
         private CameraControls m_CameraControls;
         private bool m_Rotate;
         private bool m_MouseControl;
         
         public override bool Process(string[] arguments)
         {
             if (arguments.Length != 1)
             {
                 return CommandResponse.Instance.Failed(CommandWord, "Given arguments does not match command!");
             }

             var str = arguments[0];
             if (!m_CameraControls)
             {
                 m_CameraControls = DataManager.Instance.cameraControls;
             }

             if (str.StringEquals("rotate"))
             {
                 m_CameraControls.CanRotate = m_Rotate;
                 m_Rotate = !m_Rotate;
                 return CommandResponse.Instance.Succeeded(CommandWord, "Can rotate: " + !m_Rotate);
             }
             
             if (str.StringEquals("reset"))
             {
                 m_CameraControls.CameraReset();
                 return CommandResponse.Instance.Succeeded(CommandWord, "Camera rotation reset.");
             }
             
             if (str.StringEquals("MouseControl"))
             {
                 m_CameraControls.CanMoveCameraWithMouse = m_MouseControl;
                 m_MouseControl = !m_MouseControl;
                 return CommandResponse.Instance.Succeeded(CommandWord, "Can move camera with mouse: " + !m_MouseControl);
             }

             return CommandResponse.Instance.Failed(CommandWord, "No camera command matching for the argument: " + str);
         }
     }
 }