using UnityEngine;
using UnityEngine.AI;

public class PetMobMovement : PetMovement<Mob>
{
    protected void Awake()
    {
        // base.Awake();
        owner = transform.parent.gameObject;
        nav = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
        ownerEntity = owner.GetComponent<Entity>();
        Debug.Log(owner.name);
    }
}