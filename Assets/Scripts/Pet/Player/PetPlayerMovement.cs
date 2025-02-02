using UnityEngine;
using UnityEngine.AI;

public class PetPlayerMovement : PetMovement<Player>
{
    protected void Awake()
    {
        owner = GameObject.FindGameObjectWithTag(typeof(Player).Name);
        nav = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
        ownerEntity = owner.GetComponent<Entity>();
    }
}