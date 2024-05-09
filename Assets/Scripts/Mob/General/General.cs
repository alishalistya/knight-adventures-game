public class General: MobMeele
{
        public override int ID => 2;
        
        protected override string AttackAnimationMovement => "AttackMovement";

        protected int baseHealth = 120;
        protected int initialHealth;

        protected override int MaxHealth => initialHealth;
        protected override int InitialHealth => initialHealth;

        protected override float TimeBetweenAttack => 3f;

        protected new void Awake()
        {
                base.Awake();
                initialHealth = (int)(baseHealth * _difficultyMultiplier);
        }
}