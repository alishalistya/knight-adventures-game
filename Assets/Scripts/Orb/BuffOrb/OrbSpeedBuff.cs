using System.Threading.Tasks;

public class OrbSpeedBuff : BuffOrb
{
    public override string OrbName { get; } = "SpeedBuff";
    
    protected override void OrbEffects(Player player)
    {
        foreach (var buff in player.Movement.MovementSpeedMultiplierBuffs)
        {
            if (!buff.Name.Equals(OrbName)) continue;
            player.Movement.MovementSpeedMultiplierBuffs.Remove(buff);
            break;
        }
        
        var movementBuff = new MovementSpeedMultiplierBuff(OrbName, 0.2f);
        player.Movement.MovementSpeedMultiplierBuffs.Add(movementBuff);
        
        Task.Delay((int)(15 * 1000)).ContinueWith(t =>
        {
            movementBuff.IsActive = false;
            player.Movement.MovementSpeedMultiplierBuffs.Remove(movementBuff);
        });
    }
}