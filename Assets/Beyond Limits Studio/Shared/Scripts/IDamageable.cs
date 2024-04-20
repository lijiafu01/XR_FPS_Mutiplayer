namespace BeyondLimitsStudios
{
    public interface IDamageable
    {
        public void ApplyDamage(DamageData damageData);
    }

    public struct DamageData
    {
        public float Damage;

        public DamageData(float Damage)
        {
            this.Damage = Damage;
        }
    }
}