public interface IUnit
{
    public void Selected();

    // Destroy unit
    public void Destroy();

    // Move unit
    public void Move(UnityEngine.Vector3 destination);
}
