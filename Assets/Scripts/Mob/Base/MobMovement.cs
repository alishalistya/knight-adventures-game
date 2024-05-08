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

    private MobMovementState _state = MobMovementState.Idle;
    
    [SerializeField] float triggerRange = 0f;

    public bool isTriggered = false;

    protected MobMovementState state 
    {
        get => _state;
        set  {
            _state = value;
            Animating();
        }
    }

    private bool _useDefaultRotation = true;
    
    private float _preciseUpdateOffset = 10f;

    private float _lastUpdate = 0f;

    private float _lastUpdateOffset = 2f;

    protected virtual bool AlwaysPreciseMovement => false;

    protected virtual bool UseRandomMovement => false;

    private float _randomMovementMinimalOffset;

    protected void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
        playerEntity = player.GetComponent<Player>();
        nav.isStopped = true;
        _randomMovementMinimalOffset = 0.5f * nav.stoppingDistance;
    }

    protected void Update()
    {
        var destination = player.transform.position;
        Vector3 distance = destination - transform.position;

        if (distance.magnitude <= triggerRange || triggerRange == 0f)
        {
            nav.isStopped = false;
            isTriggered = true;
        }

        if (!isTriggered)
        {
            return;
        }

        if (!playerEntity.IsDead && UseRandomMovement && distance.magnitude < _randomMovementMinimalOffset)
        {
            state = MobMovementState.Running;

            _lastUpdate += Time.deltaTime;
            

            if (_lastUpdate <= _lastUpdateOffset)
            {
                return;
            }
            
            _lastUpdate = 0f;
            
            var random = RandomNavSphere(player.transform.position, nav.stoppingDistance*2f, -1);
            
            random.y = 0; // This allows the object to only rotate on its y axis
            var rotation = Quaternion.LookRotation(random);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 120 * Time.deltaTime);
            
            nav.SetDestination(random);
        }
        else if (!playerEntity.IsDead && distance.magnitude > nav.stoppingDistance)
        {
            state = MobMovementState.Running;

            _lastUpdate += Time.deltaTime;
            
            distance.y = 0; // This allows the object to only rotate on its y axis
            var rotation = Quaternion.LookRotation(distance);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 120 * Time.deltaTime);

            if (distance.magnitude > _preciseUpdateOffset && _lastUpdate <= _lastUpdateOffset && !AlwaysPreciseMovement)
            {
                return;
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
                var random = RandomNavSphere(destination, 20, -1);
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