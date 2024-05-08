using UnityEngine;
using UnityEngine.AI;

public class PetMobMovement : PetMovement<Mob>
{
    [SerializeField] private bool IsLeft = true;
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
        if (Vector3.Distance(target.transform.position, transform.position) < 5f &&  Vector3.Distance(owner.transform.position, transform.position) < 3f)
        {
            state = PetMovementState.Follow;
            var destination = transform.position + new Vector3(IsLeft ? 1 : -1, 0, IsLeft ? 1 : -1);
            
            _lastUpdate += Time.deltaTime;
            
            _lastUpdate = 0f;
            
            nav.SetDestination(destination);
        }
        else if (!ownerEntity.IsDead && (Vector3.Distance(owner.transform.position, transform.position) > _distanceToOwner))
        {
            // state = PetMovementState.Running;
            state = PetMovementState.Follow;

            var destination = owner.transform.position + new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2));

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