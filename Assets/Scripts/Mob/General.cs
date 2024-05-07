public class General: MobMeele
{
        public override int ID => 2;
        
        protected override string AttackAnimationMovement => "AttackMovement";

        protected override int MaxHealth => 150;
        protected override int InitialHealth => 150;

        protected override float TimeBetweenAttack => 1f;

        protected override void OnDamaged(int prevHealth, int currentHealth)
        {

        }
}