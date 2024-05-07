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
    
    protected float _lastUpdate = 0f;

    protected float _lastUpdateOffset = 1f;
    
    public float _distanceToOwner = 2f;
    
    protected void Update()
    {
        if (!ownerEntity.IsDead 
            && 
            (Vector3.Distance(owner.transform.position, transform.position) > _distanceToOwner))
        {
            state = PetMovementState.Follow;

            var destination = owner.transform.position;

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
    
    protected void Animating()
    {
        Anim.SetInteger("MovementState", (int)state);
    }
}