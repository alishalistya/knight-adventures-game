using UnityEngine;
using UnityEngine.AI;

enum KerocoMovementState
{
    Idle = 0,
    Running = 1
}

public class KerocoMovement : MonoBehaviour
{
    public Animator Anim;

    protected Transform player;
    protected NavMeshAgent nav;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
    }

    void Update()
    {
        nav.SetDestination(player.position);
        Animating();
    }

    void Animating()
    {
        /* todo animation follow agent
         * match speed with agent speed
         * https://docs.unity3d.com/Packages/com.unity.ai.navigation@2.0/manual/MixingComponents.html
         */
        Anim.SetInteger("MovementState", (int)KerocoMovementState.Running);
    }
}