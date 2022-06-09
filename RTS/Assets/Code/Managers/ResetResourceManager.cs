using System.Collections.Generic;
using Code.ScriptableObjects;
using Code.Tools.HelperClasses;

namespace Code.Managers
{
    public class ResetResourceManager : Singleton<ResetResourceManager>
    {
        public List<ResourceData> listOfResourcesToReset;

        public void Awake()
        {
            foreach (var resource in listOfResourcesToReset)
            {
                resource.resourcesLeft = ResourceData.C_MaxAmount;
            }
        }
    }
}