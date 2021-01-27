public interface IUnit
{
    // Destroy unit
    public void Destroy();

    // Spawn unit
    public void Spawn();

    public void Move(UnityEngine.Vector3 destination);
}
