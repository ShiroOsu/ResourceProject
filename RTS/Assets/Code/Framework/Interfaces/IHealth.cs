namespace Code.Framework.Interfaces
{
    public interface IHealth
    {
        public void TakeDamage(float amount);
        public void ModifyHealth(float amount);
        public void RegenHealth(/*float amount*/);
    }
}
