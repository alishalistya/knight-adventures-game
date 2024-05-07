using UnityEngine;
using UnityEngine.AI;

public class PetMobMovement : PetMovement<Mob>
{
    public GameObject target;
    
    protected void Awake()
    {
        // base.Awake();
        owner = transform.parent.gameObject;
        nav = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
        ownerEntity = owner.GetComponent<Entity>();
        target = GameObject.FindGameObjectWithTag("Player");
    }
    
    protected void Update()
    {
        if (Vector3.Distance(target.transform.position, transform.position) < 5f)
        {
            state = PetMovementState.Follow;
            var destination = new Vector3(-1, 0, -1);
            
            _lastUpdate += Time.deltaTime;
            
            _lastUpdate = 0f;
            
            nav.SetDestination(destination);
        }
        else if (!ownerEntity.IsDead && (Vector3.Distance(owner.transform.position, transform.position) > _distanceToOwner))
        {
            // state = PetMovementState.Running;
            state = PetMovementState.Follow;

            var destination = owner.transform.position;

            _lastUpdate += Time.deltaTime;
            
            _lastUpdate = 0f;
            nav.SetDestination(destination);
        }
        else
        {
            state = PetMovementState.Idle;
            transform.rotation = owner.transform.rotation;
        }
        
        Animating();
    }
}