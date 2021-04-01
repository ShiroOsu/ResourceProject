using UnityEngine;

public class Castle : MonoBehaviour, IStructure
{
    public void Build()
    {
        Debug.Log("Build " + transform.name);
    }

    public void Destroy()
    {
        Debug.Log("Destroy " + transform.name);
    }

    public void Selected()
    {
        Debug.Log(transform.name + " selected");
    }
}
