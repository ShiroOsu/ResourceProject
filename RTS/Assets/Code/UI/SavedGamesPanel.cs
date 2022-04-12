using System;
using UnityEngine;

namespace Code.UI
{
    public class SavedGamesPanel : MonoBehaviour
    {
        public void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}
