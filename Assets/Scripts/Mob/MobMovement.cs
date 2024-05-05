using UnityEngine;
using UnityEngine.AI;

public enum MobMovementState
{
    Idle = 0,
    Running = 1
}

public abstract class MobMovement : MonoBehaviour
{
    public Animator Anim;

    public GameObject player;
    public Player playerEntity;
    public NavMeshAgent nav;

    protected MobMovementState state = MobMovementState.Idle;

    private bool _useDefaultRotation = true;
    
    private float _preciseUpdateOffset = 5f;

    private float _lastUpdate = 0f;

    private float _lastUpdateOffset = 1f;

    protected bool AlwaysPreciseMovement => false;

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

    protected void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
        playerEntity = player.GetComponent<Player>();
    }

    protected void Update()
    {
        var destination = player.transform.position;
        Vector3 distance = destination - transform.position;
        
        if (!playerEntity.IsDead && distance.magnitude > nav.stoppingDistance)
        {
            state = MobMovementState.Running;

            _lastUpdate += Time.deltaTime;

            if (distance.magnitude > _preciseUpdateOffset && _lastUpdate <= _lastUpdateOffset && !AlwaysPreciseMovement)
            {
                Animating();
                return;
            }
            
            if (!UseDefaultRotation)
            {
                
                distance.y = 0; // This allows the object to only rotate on its y axis
            
                var rotation = Quaternion.LookRotation(distance);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 120 * Time.deltaTime);
            }
            
            _lastUpdate = 0f;

            // movement depend on range
            if (AlwaysPreciseMovement || distance.magnitude < _preciseUpdateOffset)
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
            distance.y = 0; // This allows the object to only rotate on its y axis
            
            var rotation = Quaternion.LookRotation(distance);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 120 * Time.deltaTime);
            state = MobMovementState.Idle;
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