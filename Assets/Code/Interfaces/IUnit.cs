public interface IUnit
{
    public void Unselect();

    public void Selected();

    public void Destroy();

    public void Move(UnityEngine.Vector3 destination);
}
