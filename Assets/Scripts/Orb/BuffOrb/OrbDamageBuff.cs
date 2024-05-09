public class OrbBuffDamage : BuffOrb
{
    public override string OrbName { get; } = "BuffDamage";
    protected override void OrbEffects(Player player)
    {
        if (player.gameManager.buffDamageTaken < 15)
        {
            player.gameManager.buffDamageTaken++;
        }
    }
}