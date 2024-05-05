using System.Threading.Tasks;

public abstract class MobRanged: Mob, IWeaponAnimationHandler
{
        protected RangedMobWeapon weapon;
        private bool isReadyToAttack = true;
        
        protected abstract string AttackAnimationMovement { get; }

        protected new void Awake()
        {
                base.Awake();
                // we just assume that mob only have one weapon
                weapon = GetComponentInChildren<RangedMobWeapon>();
        }
        
        private void FixedUpdate()
        {
                if (PlayerInRange && !movement.playerEntity.IsDead && !IsDead && isReadyToAttack)
                {
                        Attack();
                }
        }

        void Attack()
        {
                movement.Anim.Play(AttackAnimationMovement);
        }

        public override void OnStartAttackAnim()
        {
                base.OnStartAttackAnim();
                isReadyToAttack = false;
        }

        public override void OnEndAttackAnim()
        {
                base.OnEndAttackAnim();
                Task.Delay((int)(TimeBetweenAttack * 1000)).ContinueWith(t =>
                {
                        isReadyToAttack = true;
                });
        }

        public void OnStartAttackTrigger()
        {
                weapon.IsActive = false;
        }

        public void OnEndAttackTrigger()
        {
                weapon.IsActive = true;
        }
}