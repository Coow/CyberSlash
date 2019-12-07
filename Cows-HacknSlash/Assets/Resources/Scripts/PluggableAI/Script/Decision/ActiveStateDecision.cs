using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/ActiveState")]
public class ActiveStateDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        // Check if user is active.
        // We can also check is user is destory.
        bool chaseTargetIsActive = controller.ChaseTarget.gameObject.activeSelf;
        return chaseTargetIsActive;
    }
}
