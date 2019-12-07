using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/TurnToward")]
public class TurnTowardAction : Action
{
    public override void Act(StateController controller)
    {
        TurnToward(controller);
    }

    // Method -> If enemy remaining distance is less than Stoppoing Distance 
    // Manually Rotate Enemy AI agent toward chase target face so it always face enemy.
    private void TurnToward(StateController controller)
    {
        var distance = Vector3.Distance(controller.ChaseTarget.position, controller.transform.position);

        if (distance <= controller.Agent.stoppingDistance)
        {

            RotateTowardPlayer(controller);
        }

    }

    // Method -> Just to let the enemy to face Chase Target.
    private void RotateTowardPlayer(StateController controller)
    {
        var direction = (controller.ChaseTarget.position - controller.transform.position).normalized;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, lookRotation, 
        controller.EnemyStats.TurnSpeed * Time.deltaTime);
    }
}
