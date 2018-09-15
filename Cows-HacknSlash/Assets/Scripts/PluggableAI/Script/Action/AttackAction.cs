using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Attack")]
public class AttackAction : Action
{
    public override void Act(StateController controller)
    {
        Attack(controller);
    }

    // Method -> This will check if enemy is in specified range to the player,
    // If in range enemy will start attacking according to attack rate.
    private void Attack(StateController controller)
    {
        RaycastHit hit;

        Debug.DrawRay(controller.Eyes.position, controller.Eyes.forward.normalized * controller.EnemyStats.AttackRange, Color.red);

        if (Physics.SphereCast(controller.Eyes.position, controller.EnemyStats.lookSphereCastRadius, 
            controller.Eyes.forward, out hit, controller.EnemyStats.AttackRange) &&
            hit.collider.CompareTag("Player"))
        {
            // Check if Its Time to lunch next Attack (Note you can adjust AttackRate From EnemyStats (MagicCaster, FootSoldier)).
            if (controller.CheckIfCountDownElapsed(controller.EnemyStats.AttackRate))
            {
                controller.EnemyAttack.FollowTarget(controller.ChaseTarget.transform.position);
                controller.EnemyAttack.Attack(controller.EnemyStats.AttackRate);
            }
        }
        
    }
}
