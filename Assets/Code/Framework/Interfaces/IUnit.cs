public interface IUnit
{
    public void ShouldSelect(bool select);
   
    public void Destroy();

    public void Move(UnityEngine.Vector3 destination);
}
