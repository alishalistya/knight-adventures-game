public class PetBuffer: BasePetMob
{
    // TODO: Implement this class
    protected override int MaxHealth => 50;
    protected override int InitialHealth => 50;
    protected override void OnDamaged(int prevHealth, int currentHealth)
    {
    }

    protected override float TimeBetweenAbility => 10f;
    protected override int AbilityEffect => 20;
}