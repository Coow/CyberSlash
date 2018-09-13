using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }

    // Method -> It check does the ray hit the user.
    // if hit chase him.
    // else remain in state.
    private bool Look(StateController controller)
    {
        RaycastHit hit;

        Debug.DrawRay(controller.Eyes.position, controller.Eyes.forward.normalized * controller.EnemyStats.lookRange, Color.green);

        if (Physics.SphereCast(controller.Eyes.position, controller.EnemyStats.lookSphereCastRadius, 
            controller.Eyes.forward, out hit, controller.EnemyStats.lookRange) &&
            hit.collider.CompareTag("Player"))
        {
            controller.ChaseTarget = hit.transform;
            // Changing it to 1 give it good distance, to let Enemy Start Attacking.
            controller.EnemyStats.lookSphereCastRadius = 1; 
            return true;
        }
        else
        {
            return false;
        } 
    }
}
