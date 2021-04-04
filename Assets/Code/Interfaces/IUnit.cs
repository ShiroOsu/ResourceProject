public interface IUnit
{
    public void Selected();

    // Destroy unit
    public void Destroy();

    // Spawn unit
    public void Spawn();

    // Move unit
    public void Move(UnityEngine.Vector3 destination);
}
