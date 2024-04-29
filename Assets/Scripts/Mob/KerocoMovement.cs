using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

enum KerocoMovementState
{
    Idle = 0,
    Running = 1
}

public class KerocoMovement : MobMovement
{
    protected void Awake()
    {
        base.Awake();
        runningStateNumber = (int)KerocoMovementState.Running;
    }
}