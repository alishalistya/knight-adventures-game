using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public enum PetMovementState
{
    Idle = 0,
    Follow = 1,
}

public abstract class PetMovement<TEntity> : MonoBehaviour where TEntity : Entity
{
    public Animator Anim;
    // public GameObject target;  
    // public Entity targetEntity;
    public GameObject owner;
    public Entity ownerEntity;
    public NavMeshAgent nav;

    protected PetMovementState state = PetMovementState.Idle;

    protected bool _useDefaultRotation = true;
    
    protected float _preciseUpdateOffset = 5f;

    protected float _lastUpdate = 0f;

    protected float _lastUpdateOffset = 1f;
    
    protected float _distanceToOwner = 2f;

    public bool UseDefaultRotation
    {
        get => _useDefaultRotation;
        set
        {
            if (value)
            {
                nav.angularSpeed = 120;
            }
            else
            {
                nav.angularSpeed = 0;
            }

            _useDefaultRotation = value;
        }
    }

    // protected void Awake()
    // {
    //     owner = GameObject.FindGameObjectWithTag(typeof(TEntity).Name);
    //     nav = GetComponent<NavMeshAgent>();
    //     Anim = GetComponent<Animator>();
    //     // ownerEntity = owner.GetComponent<TEntity>();
    //     ownerEntity = owner.GetComponent<Entity>();
    // }
    
    protected void Update()
    {
        if (!ownerEntity.IsDead && (Vector3.Distance(owner.transform.position, transform.position) > _distanceToOwner))
        {
            // state = PetMovementState.Running;
            state = PetMovementState.Follow;

            var destination = owner.transform.position;
            Vector3 dir = destination - transform.position;

            _lastUpdate += Time.deltaTime;

            if (dir.magnitude > _preciseUpdateOffset && _lastUpdate <= _lastUpdateOffset)
            {
                Animating();
                return;
            }
            
            if (!UseDefaultRotation)
            {
                
                dir.y = 0; // This allows the object to only rotate on its y axis
            
                var rotation = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 120 * Time.deltaTime);
            }
            
            _lastUpdate = 0f;

            // movement depend on range
            if (dir.magnitude < _preciseUpdateOffset)
            {
                // use normal navigation
                nav.SetDestination(destination);
            }
            else
            {
                // add some randomness
                var random = RandomNavSphere(transform.position, 10, -1);
                var avg = (random + destination) / 2;
                nav.SetDestination(avg);
            }
        }
        else
        {
            state = PetMovementState.Idle;
            transform.rotation = owner.transform.rotation;
        }
        
        Animating();
    }
    
    protected Vector3 RandomNavSphere(Vector3 origin, float dist,int layermask)
    {
        var randDirection = Random.insideUnitSphere * dist;
   
        randDirection += origin;

        NavMeshHit navhit;
   
        NavMesh.SamplePosition(randDirection, out navhit, dist, layermask);
   
        return navhit.position;
    }

    protected void Animating()
    {
        /* todo animation follow agent
         * match speed with agent speed
         * https://docs.unity3d.com/Packages/com.unity.ai.navigation@2.0/manual/MixingComponents.html
         */
        Anim.SetInteger("MovementState", (int)state);
    }
}