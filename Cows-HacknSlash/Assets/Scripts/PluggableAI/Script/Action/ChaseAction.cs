using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Chase")]
public class ChaseAction : Action
{
    public override void Act(StateController controller)
    {
        Chase(controller);
    }

    // Method -> This will let the enemy to start shasing player.
    private void Chase(StateController controller)
    {
        controller.Agent.destination = controller.ChaseTarget.position;
        controller.Agent.isStopped = false;
    }
}
