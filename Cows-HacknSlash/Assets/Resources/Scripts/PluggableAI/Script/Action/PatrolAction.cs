using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : Action
{
    public override void Act(StateController controller)
    {
        Patrol(controller);
    }

    private void Patrol(StateController controller)
    {
        // The first thing to do is to check the wayPoint it's headed toward.
        controller.Agent.destination = controller.WayPointsList[controller.NextWayPoint].position;

        // Start movement of agent.
        controller.Agent.isStopped = false;

        // Check if agent arrive to current destination.
        if (controller.Agent.remainingDistance <= controller.Agent.stoppingDistance &&
            !controller.Agent.pathPending)
        {
            // move agent to the next way point.
            controller.NextWayPoint = (controller.NextWayPoint + 1) % controller.WayPointsList.Count;
        }
    }
}
