using System.Numerics;

namespace Code.SaveSystem.Data
{
    [System.Serializable]
    public struct SoldierData
    {
        public Vector3 position;
        public Quaternion rotation;
        public float health;
    }
}