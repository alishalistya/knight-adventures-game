using UnityEngine;
using UnityEngine.AI;

public class MobMovement : MonoBehaviour
{
    public Animator Anim;

    public GameObject player;
    public NavMeshAgent nav;

    protected int runningStateNumber;

    protected void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
    }

    protected void Update()
    {
        nav.SetDestination(player.transform.position);
        Animating();
    }

    protected void Animating()
    {
        /* todo animation follow agent
         * match speed with agent speed
         * https://docs.unity3d.com/Packages/com.unity.ai.navigation@2.0/manual/MixingComponents.html
         */
        Anim.SetInteger("MovementState", runningStateNumber);
    }
}