using System;

public abstract class BasePetPlayer : BasePet<Player>
{
  public override void TakeDamage(int amount)
  {
    if (PlayerCheats.IsCheat(StatusCheats.FULL_HP_PET))
    {
      return;
    }
    base.TakeDamage(amount);
  }
}