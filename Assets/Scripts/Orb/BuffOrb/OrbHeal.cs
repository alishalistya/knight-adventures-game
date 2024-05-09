public class OrbHeal : BuffOrb
{
    public override string OrbName { get; } = "Heal";
    protected override void OrbEffects(Player player)
    {
        player.Heal((int)(player.Health.MaxHealth.value*0.2f));
    }
}