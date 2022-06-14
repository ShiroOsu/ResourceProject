using Code.Tools.Debugging;
using Code.Tools.HelperClasses;

namespace Code.Tools.DeveloperConsole
{
    public class CommandResponse : Singleton<CommandResponse>
    {
        public bool Failed(string commandWord, string errorTxt)
        {
            Log.Error(commandWord + " Failed!", errorTxt);
            return false;
        }

        public bool Succeeded(string commandWord, string txt = "")
        {
            Log.Print(commandWord + "Command Succeeded!", txt);
            return true;
        }
    }
}
