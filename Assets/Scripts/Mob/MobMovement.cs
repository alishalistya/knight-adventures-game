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
            nav.SetDestination(player.transform.position);
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