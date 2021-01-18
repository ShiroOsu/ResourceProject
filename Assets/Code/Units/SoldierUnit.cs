using UnityEngine;

public class SoldierUnit : MonoBehaviour, IUnit
{
    public void Destroy()
    {
        Debug.Log(transform.name + " Destroy");
    }

    public void Spawn()
    {
        Debug.Log(transform.name + " Spawn");
    }
}