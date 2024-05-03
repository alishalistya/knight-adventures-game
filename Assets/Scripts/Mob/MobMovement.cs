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
        if (!playerEntity.IsDead)
        {
            state = MobMovementState.Running;

            var destination = player.transform.position;
            
            if (!UseDefaultRotation)
            {
                Vector3 dir = destination - transform.position;
                dir.y = 0; //This allows the object to only rotate on its y axis
            
                var rotation = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 120 * Time.deltaTime);
            }
            
            nav.SetDestination(destination);
        }
        else
        {
            state = MobMovementState.Idle;
        }
        
        Animating();
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