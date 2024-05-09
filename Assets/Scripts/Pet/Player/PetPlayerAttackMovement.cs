using UnityEngine;

public class PetPlayerAttackMovement : PetPlayerMovement
{
    // TODO: implement change target when player hit an enemy
    // target and targetEntity is same as in PetPlayerAttackMovement
    public GameObject target;
    public Entity targetEntity;
    

    private void setTarget(bool IsNull)
    {
        if (!IsNull)
        {
            target = GameObject.FindGameObjectWithTag("Enemy");
            targetEntity = target is not null ? target.GetComponent<Entity>() : null;
        }
        else
        {
            target = null;
            targetEntity = null;
        }
    }
    
    protected void FixedUpdate()
    {
        if (target is null)
        {
            setTarget(false);
        }
        if ((!ownerEntity.IsDead
             && Vector3.Distance(owner.transform.position, transform.position) > _distanceToOwner)
             || target is null
            )
        {
            state = PetMovementState.Follow;
            setTarget(true);

            var _random = Random.Range(0, 2);
            var destination = owner.transform.position + new Vector3(_random % 2 == 0 ? 1 : -1, 0, _random % 2 == 1 ? 1 : -1);

            _lastUpdate += Time.deltaTime;
            
            _lastUpdate = 0f;

            nav.SetDestination(destination);
        }
        else if (target is not null) /* chase target */
        {
            targetEntity = target.GetComponent<Entity>();
            if (targetEntity.IsDead)
            {
                setTarget(true);
            }
            else
            {
                state = PetMovementState.Follow;
                var destination = target.transform.position;

                _lastUpdate += Time.deltaTime;
                
                _lastUpdate = 0f;

                nav.SetDestination(destination);
            }
        }
        else
        {
            setTarget(true);
            state = PetMovementState.Idle;
            transform.rotation = owner.transform.rotation;
        }
        
        Animating();
    }

}